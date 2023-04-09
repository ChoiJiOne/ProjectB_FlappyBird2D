using System;
using System.Collections.Generic;


/**
 * @brief 게임의 플레이어가 조종하는 새 오브젝트입니다.
 */
class Bird : GameObject
{
    /**
     * @brief 새 오브젝트의 상태를 정의합니다.
     */
    public enum EState
    {
        WAIT = 0x00, // 대기 상태입니다.
        JUMP = 0x01, // 점프 상태입니다.
        FALL = 0x02, // 떨어지는 상태입니다.
        DONE = 0x03, // 끝난 상태입니다.
    }


    /**
     * @brief 새 오브젝트의 날개입니다.
     */
    public enum EWing
    {
        UP = 0x00,     // 날개가 위에 있습니다.
        NORMAL = 0x01, // 날개를 접었습니다.
        DOWN = 0x02,   // 날개가 아래에 있습니다.
    }


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트 속성에 대한 Getter/Setter 입니다.
     */
    public RigidBody Body
    {
        get => rigidBody_;
    }

    public EState State
    {
        get => currentState_;
        set => currentState_ = value;
    }

    public bool Movable
    {
        get => bCanMove_;
        set => bCanMove_ = value;
    }

    public int PassPipe
    {
        get => countPassPipe_;
    }


    /**
     * @brief 새 오브젝트의 바디를 생성합니다.
     * 
     * @param center 새 오브젝트 바디의 중심 좌표입니다.
     * @param width 새 오브젝트 바디의 가로 크기입니다.
     * @param height 새 오브젝트 바디의 세로 크기입니다.
     */
    public void CreateBody(Vector2<float> center, float width, float height)
    {
        rigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Tick(float deltaSeconds)
    {
        if (bCanMove_)
        {
            switch (currentState_)
            {
                case EState.WAIT:
                    UpdateWaitState(deltaSeconds);
                    break;

                case EState.JUMP:
                    UpdateJumpState(deltaSeconds);
                    break;

                case EState.FALL:
                    UpdateFallState(deltaSeconds);
                    break;

                case EState.DONE:
                    UpdateDoneState(deltaSeconds);
                    break;
            }
        }

        string birdTextureSignature = "BirdWingNormal";

        if (currWingState_ != EWing.NORMAL)
        {
            birdTextureSignature = (currWingState_ == EWing.UP) ? "BirdWingUp" : "BirdWingDown";
        }

        Texture wingBird = ContentManager.Get().GetTexture(birdTextureSignature);
        RenderManager.Get().DrawTexture(ref wingBird, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height, rotate_);
    }
    

    /**
     * @brief 새 오브젝트가 바닥과 부딪히는지 확인합니다.
     * 
     * @note 새 오브젝트가 바닥과 부딪히면 상태를 DONE으로 변경합니다.
     */
    private void CheckCollisionFloor()
    {
        Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;

        if (floor.Body.IsCollision(ref rigidBody_))
        {
            currentState_ = EState.DONE;
            PlaySound("Hit");
        }

        if(currentState_ == EState.DONE)
        {
            StopRelatedGameObject();
        }
    }


    /**
     * @brief 새 오브젝트가 파이프와 부딪히는지 확인합니다.
     * 
     * @note 새 오브젝트가 파이프와 부딪히면 상태를 DONE으로 변경합니다.
     */
    private void CheckCollisionPipe()
    {
        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
        List<Pipe> pipes = pipeDetector.DetectPipes;

        foreach(Pipe pipe in pipes)
        {
            if(pipe.TopRigidBody.IsCollision(ref rigidBody_) || pipe.BottomRigidBody.IsCollision(ref rigidBody_))
            {
                currentState_ = EState.DONE;
                PlaySound("Hit");
            }
        }

        if (currentState_ == EState.DONE)
        {
            StopRelatedGameObject();
        }
    }


    /**
     * @brief 새 오브젝트가 통과한 파이프를 확인합니다.
     */
    private void CheckPassPipe()
    {
        float birdCenterX = rigidBody_.Center.x;

        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
        List<Pipe> pipes = pipeDetector.DetectPipes;
        float pipeWidth = 0.0f;
        float pipeCenterX = 0.0f;

        foreach (Pipe pipe in pipes)
        {
            pipeWidth = pipe.TopRigidBody.Width;
            pipeCenterX = pipe.TopRigidBody.Center.x;

            if(Math.Abs(birdCenterX - pipeCenterX) <= 1.0f && !pipe.PassBird)
            {
                pipe.PassBird = true;
                countPassPipe_++;

                Number birdScore = WorldManager.Get().GetGameObject("BirdScore") as Number;
                birdScore.NumberContext = countPassPipe_;

                PlaySound("Coin");
            }
        }
    }


    /**
     * @brief 새 오브젝트와 관련 있는 게임 오브젝트의 움직임을 중단합니다.
     */
    private void StopRelatedGameObject()
    {
        Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
        List<Pipe> pipes = pipeDetector.DetectPipes;

        floor.Movable = false;
        pipeDetector.CanGeneratePipe = false;

        foreach (Pipe pipe in pipes)
        {
            pipe.Movable = false;
        }
    }


    /**
     * @brief 새 오브젝트의 상태가 WAIT 상태일 때 업데이트를 수행합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void UpdateWaitState(float deltaSeconds)
    {
        if (currentState_ != EState.WAIT) return; // 현재 상태가 WAIT가 아니면 수행하지 않습니다.

        waitTime_ += deltaSeconds;
        if (waitTime_ > maxWaitTime_)
        {
            waitTime_ = 0.0f;
            waitMoveDirection_ *= -1.0f;
        }

        MovePosition(deltaSeconds);
        UpdateWingState(deltaSeconds);

        if (InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
        {
            currentState_ = EState.JUMP;
            rotate_ = MinRotate;
            bIsJump_ = true;

            Scene scene = WorldManager.Get().GetGameObject("Scene") as Scene;
            SceneNode currentSceneNode = scene.CurrSceneNode;
            currentSceneNode.DetectSwitch = true;

            PlaySound("Jump");
        }

        CheckCollisionPipe();
        CheckCollisionFloor();
    }


    /**
     * @brief 새 오브젝트의 상태가 JUMP 상태일 때 업데이트를 수행합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void UpdateJumpState(float deltaSeconds)
    {
        if (currentState_ != EState.JUMP) return;

        MovePosition(deltaSeconds);
        UpdateWingState(deltaSeconds);
        UpdateJumpMoveState(deltaSeconds);

        if (InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
        {
            jumpMoveUpLength_ = 0.0f;
            jumpMoveDownLength_ = 0.0f;
            bIsJump_ = true;

            PlaySound("Jump");
        }

        CheckPassPipe();
        CheckCollisionPipe();
        CheckCollisionFloor();
    }


    /**
     * @brief 새 오브젝트의 상태가 FALL 상태일 때 업데이트를 수행합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void UpdateFallState(float deltaSeconds)
    {
        if (currentState_ != EState.FALL) return; // 현재 상태가 FALL이 아니면 수행하지 않습니다.

        MovePosition(deltaSeconds);

        if (InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
        {
            currentState_ = EState.JUMP;
            rotate_ = MinRotate;
            bIsJump_ = true;

            PlaySound("Jump");
        }

        CheckPassPipe();
        CheckCollisionPipe();
        CheckCollisionFloor();
    }


    /**
     * @brief 새 오브젝트가 DONE 상태일 때 업데이트를 수행합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void UpdateDoneState(float deltaSeconds)
    {
        if (currentState_ != EState.DONE) return;

        MovePosition(deltaSeconds);

        Background background = WorldManager.Get().GetGameObject("Background") as Background;
        bIsLeaveInBackground_ = !background.Body.IsCollision(ref rigidBody_);

        if(bIsLeaveInBackground_)
        {
            Scene scene = WorldManager.Get().GetGameObject("Scene") as Scene;
            SceneNode currentSceneNode = scene.CurrSceneNode;

            if (currentSceneNode is PlaySceneNode)
            {
                currentSceneNode.DetectSwitch = true;
            }
        }
    }


    /**
     * @brief 새 오브젝트가 점프했을 때의 이동 상태를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void UpdateJumpMoveState(float deltaSeconds)
    {
        if (bIsJump_)
        {
            jumpMoveUpLength_ += (deltaSeconds * moveSpeed_);

            if (jumpMoveUpLength_ > jumpUpLength_)
            {
                bIsJump_ = false;
                jumpMoveUpLength_ = 0.0f;
            }
        }
        else
        {
            jumpMoveDownLength_ += (deltaSeconds * moveSpeed_);

            if (jumpMoveDownLength_ > jumpDownLength_)
            {
                currentState_ = EState.FALL;
                currWingState_ = EWing.UP;
                prevWingState_ = EWing.NORMAL;
                wingStateTime_ = 0.0f;
                jumpMoveDownLength_ = 0.0f;
            }
        }
    }


    /**
     * @brief 새 오브젝트를 이동시킵니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void MovePosition(float deltaSeconds)
    {
        Vector2<float> center = rigidBody_.Center;

        switch (currentState_)
        {
            case EState.WAIT:
                center.y += (waitMoveDirection_ * deltaSeconds * waitMoveLength_);
                rigidBody_.Center = center;
                break;

            case EState.JUMP:
                float jumpDirection = bIsJump_ ? -1.0f : 1.0f;
                center.y += (deltaSeconds * jumpDirection * moveSpeed_);
                center.y = Math.Max(center.y, 0);
                rigidBody_.Center = center;
                break;

            case EState.FALL:
                rotate_ += (deltaSeconds * rotateSpeed_);
                rotate_ = Math.Min(rotate_, MaxRotate);
                center.y += (deltaSeconds * moveSpeed_);
                rigidBody_.Center = center;
                break;

            case EState.DONE:
                if(!bIsLeaveInBackground_)
                {
                    center.y += (deltaSeconds * moveSpeed_);
                    rigidBody_.Center = center;
                    rotate_ += (deltaSeconds * rotateSpeed_);
                }
                break;
        }
    }


    /**
     * @brief 새 오브젝트의 날개 상태를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public void UpdateWingState(float deltaSeconds)
    {
        wingStateTime_ += deltaSeconds;

        if (wingStateTime_ > changeWingStateTime_)
        {
            wingStateTime_ = 0.0f;

            if (currWingState_ == EWing.NORMAL)
            {
                currWingState_ = GetCountWingState(prevWingState_);
                prevWingState_ = EWing.NORMAL;
            }
            else // currWingState_ == EWing.DOWN or currWingState_ == EWing.UP
            {
                prevWingState_ = currWingState_;
                currWingState_ = EWing.NORMAL;
            }
        }
    }


    /**
     * @brief 사운드를 플레이합니다.
     */
    private void PlaySound(string signature)
    {
        Sound sound = ContentManager.Get().GetSound(signature) as Sound;
        sound.Reset();
        sound.Play();
    }


    /**
     * @brief 날개 상태의 반대 상태를 얻습니다.
     * 
     * @note 예시는 다음과 같습니다.
     * UP => DOWN, DOWN => UP, NORMAL => NORMAL
     */
    public static EWing GetCountWingState(EWing wingState)
    {
        if(wingState == EWing.NORMAL)
        {
            return EWing.NORMAL;
        }

        return (wingState == EWing.UP ? EWing.DOWN : EWing.UP);
    }


    /**
     * @brief 새 오브젝트가 움직일 수 있는지 확인합니다.
     */
    private bool bCanMove_ = true;


    /**
     * @brief 새 오브젝트의 현재 상태입니다.
     */
    private EState currentState_ = EState.WAIT;


    /**
     * @brief 새 오브젝트가 대기 시 움직이는 방향입니다.
     * 
     * @note +는 아래 방향, -는 위 방향입니다.
     */
    private float waitMoveDirection_ = 1.0f;


    /**
     * @brief 새 오브젝트가 대기 상태에서 누적된 시간입니다.
     */
    private float waitTime_ = 0.0f;


    /**
     * @brief 새 오브젝트가 대기 상태에서 기다릴 수 있는 최대 시간입니다.
     */
    private float maxWaitTime_ = 1.0f;


    /**
     * @brief 새 오브젝트가 대기 상태에서 움직이는 거리입니다.
     */
    private float waitMoveLength_ = 20.0f;


    /**
     * @brief 새 오브젝트가 점프 시 움직이는 거리입니다.
     */
    private float jumpUpLength_ = 70.0f;


    /**
     * @brief 새 오브젝트가 점프 후 떨어지는 거리입니다.
     */
    private float jumpDownLength_ = 40.0f;


    /**
     * @brief 새 오브젝트의 이동 속도입니다.
     */
    private float moveSpeed_ = 350.0f;


    /**
     * @brief 새 오브젝트의 점프 방향입니다.
     * 
     * @note +는 아래 방향, -는 위 방향입니다.
     */
    private bool bIsJump_ = false;


    /**
     * @brief 새 오브젝트가 점프 시 움직인 거리입니다.
     */
    private float jumpMoveUpLength_ = 0.0f;


    /**
     * @brief 새 오브젝트가 점프 후 움직인 거리입니다.
     */
    private float jumpMoveDownLength_ = 0.0f;


    /**
     * @brief 이전의 새 오브젝트의 날개 상태입니다.
     */
    private EWing prevWingState_ = EWing.NORMAL;


    /**
     * @brief 현재 새 오브젝트의 날개 상태입니다.
     */
    private EWing currWingState_ = EWing.UP;


    /**
     * @brief 새 오브젝트의 날개 상태가 지속된 시간입니다.
     */
    private float wingStateTime_ = 0.0f;


    /**
     * @brief 새 오브젝트의 날개 상태를 변경하는 시간입니다.
     */
    private float changeWingStateTime_ = 0.1f;

    
    /**
     * @brief 새 오브젝트의 회전 각도입니다.
     */
    private float rotate_ = 0.0f;


    /**
     * @brief 새 오브젝트의 회전 속도입니다.
     */
    private float rotateSpeed_ = 200.0f;


    /**
     * @brief 새 오브젝트가 백그라운드 밖으로 나갔는지 확인합니다.
     */
    private bool bIsLeaveInBackground_ = false;


    /**
     * @brief 새 오브젝트가 통과한 파이프의 수입니다.
     */
    private int countPassPipe_ = 0;


    /**
     * @brief 새 오브젝트의 최소 회전 각도입니다.
     */
    private static readonly float MinRotate = -30.0f;


    /**
     * @brief 새 오브젝트의 최대 회전 각도입니다.
     */
    private static readonly float MaxRotate = 90.0f;


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트의 강체입니다.
     */
    private RigidBody rigidBody_;
}