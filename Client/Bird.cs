using System;
using SDL2;


/**
 * @brief 게임의 플레이어가 조종하는 새 오브젝트입니다.
 */
class Bird : IGameObject
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
    public RigidBody RigidBody
    {
        get => rigidBody_;
        set => rigidBody_ = value;
    }

    public EState State
    {
        get => currentState_;
        set => currentState_ = value;
    }


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public void Update(float deltaSeconds)
    {
        bool bIsPressSpace = (InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED);
        Vector2<float> center;

        switch(currentState_)
        {
            case EState.WAIT:
                waitTime_ += deltaSeconds;

                if (waitTime_ > maxWaitTime_)
                {
                    waitTime_ = 0.0f;
                    waitMoveDirection_ *= -1.0f;
                }

                center = rigidBody_.Center;
                center.y += (waitMoveDirection_ * deltaSeconds * waitMoveLength_);
                rigidBody_.Center = center;

                if(bIsPressSpace)
                {
                    currentState_ = EState.JUMP;
                    rotate_ = MinRotate;
                    bIsJump = true;
                }
                break;

            case EState.JUMP:
                float jumpDirection = bIsJump ? -1.0f : 1.0f;

                center = rigidBody_.Center;
                center.y += (deltaSeconds * jumpDirection * moveSpeed_);
                center.y = Math.Max(center.y, 0);
                rigidBody_.Center = center;

                wingStateTime_ += deltaSeconds;

                if(wingStateTime_ > changeWingStateTime_)
                {
                    wingStateTime_ = 0.0f;

                    if(currWingState_ == EWing.NORMAL)
                    {
                        if(prevWingState_ == EWing.NORMAL)
                        {
                            currWingState_ = EWing.UP;
                        }
                        else
                        {
                            currWingState_ = (prevWingState_ == EWing.UP) ? EWing.DOWN : EWing.UP;
                            prevWingState_ = EWing.NORMAL;
                        }
                    }
                    else // currWingState_ == EWing.DOWN or currWingState_ == EWing.UP
                    {
                        prevWingState_ = currWingState_;
                        currWingState_ = EWing.NORMAL;
                    }
                }

                if (bIsJump)
                {
                    jumpMoveUpLength_ += (deltaSeconds * moveSpeed_);

                    if(jumpMoveUpLength_ > jumpUpLength_)
                    {
                        bIsJump = false;
                        jumpMoveUpLength_ = 0.0f;
                    }
                }
                else
                {
                    jumpMoveDownLength_ += (deltaSeconds * moveSpeed_);

                    if(jumpMoveDownLength_ > jumpDownLength_)
                    {
                        currentState_ = EState.FALL;
                        currWingState_ = EWing.UP;
                        prevWingState_ = EWing.NORMAL;
                        wingStateTime_ = 0.0f;
                        jumpMoveDownLength_ = 0.0f;
                    }
                }

                if (bIsPressSpace)
                {
                    jumpMoveUpLength_ = 0.0f;
                    jumpMoveDownLength_ = 0.0f;
                    bIsJump = true;
                }
                break;

            case EState.FALL:
                rotate_ += (deltaSeconds * rotateSpeed_);
                rotate_ = Math.Min(rotate_, MaxRotate);

                center = rigidBody_.Center;
                center.y += (deltaSeconds * moveSpeed_);
                rigidBody_.Center = center;

                if (bIsPressSpace)
                {
                    currentState_ = EState.JUMP;
                    rotate_ = MinRotate;
                    bIsJump = true;
                }
                break;
        }

        CheckCollisionFloor();
    }
    

    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트를 화면에 그립니다.
     */
    public void Render()
    {
        if (currentState_ == EState.JUMP)
        {
            string signature = "BirdWingNormal";
            switch(currWingState_)
            {
                case EWing.UP:
                    signature = "BirdWingUp";
                    break;

                case EWing.DOWN:
                    signature = "BirdWingDown";
                    break;
            }

            Texture wingBird = ContentManager.Get().GetTexture(signature);
            RenderManager.Get().DrawTexture(ref wingBird, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height, rotate_);
        }
        else
        {
            Texture normalWingBird = ContentManager.Get().GetTexture("BirdWingNormal");
            RenderManager.Get().DrawTexture(ref normalWingBird, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height, rotate_);
        }
    }


    /**
     * @brief 새 오브젝트가 바닥과 부딪히는지 확인합니다.
     * 
     * @note 새 오브젝트가 바닥과 부딪히면 상태를 DONE으로 변경합니다.
     */
    private void CheckCollisionFloor()
    {
        Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;

        if (floor.RigidBody.IsCollision(ref rigidBody_))
        {
            currentState_ = EState.DONE;
            floor.Movable = false;
        }
    }


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
    private float waitMoveLength_ = 10.0f;


    /**
     * @brief 새 오브젝트가 점프 시 움직이는 거리입니다.
     */
    private float jumpUpLength_ = 70.0f;


    /**
     * @brief 새 오브젝트가 점프 후 떨어지는 거리입니다.
     */
    private float jumpDownLength_ = 50.0f;


    /**
     * @brief 새 오브젝트의 이동 속도입니다.
     */
    private float moveSpeed_ = 350.0f;


    /**
     * @brief 새 오브젝트의 점프 방향입니다.
     * 
     * @note +는 아래 방향, -는 위 방향입니다.
     */
    private bool bIsJump = false;


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