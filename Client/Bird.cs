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
     * @brief 게임의 플레이어가 조종하는 새 오브젝트 속성에 대한 Getter/Setter 입니다.
     */
    public Texture Texture
    {
        get => texture_;
        set => texture_ = value;
    }

    public RigidBody RigidBody
    {
        get => rigidBody_;
        set => rigidBody_ = value;
    }


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public void Update(float deltaSeconds)
    {
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

                if(InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
                {
                    currentState_ = EState.JUMP;
                    rotate_ = MinRotate;
                }
                break;

            case EState.JUMP:
                center = rigidBody_.Center;
                center.y -= (deltaSeconds * moveSpeed_);
                rigidBody_.Center = center;

                jumpMoveLength_ += (deltaSeconds * moveSpeed_);

                if(jumpMoveLength_ > jumpLength_)
                {
                    currentState_ = EState.FALL;
                    jumpMoveLength_ = 0.0f;
                }
                break;

            case EState.FALL:
                rotate_ += (deltaSeconds * rotateSpeed_);

                if (rotate_ > MaxRotate) rotate_ = MaxRotate;

                center = rigidBody_.Center;
                center.y += (deltaSeconds * moveSpeed_);
                rigidBody_.Center = center;

                if (InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
                {
                    currentState_ = EState.JUMP;
                    rotate_ = MinRotate;
                }
                break;
        }

        Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
        if(floor.RigidBody.IsCollision(ref rigidBody_))
        {
            currentState_ = EState.DONE;
            floor.Movable = false;
        }
    }
    

    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트를 화면에 그립니다.
     */
    public void Render()
    {
        RenderManager.Get().DrawTexture(ref texture_, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height, rotate_);
    }


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        texture_.Release();
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
    public float waitMoveLength_ = 10.0f;


    /**
     * @brief 새 오브젝트가 점프 시 움직이는 거리입니다.
     */
    public float jumpLength_ = 70.0f;


    /**
     * @brief 새 오브젝트의 이동 속도입니다.
     */
    public float moveSpeed_ = 300.0f;


    /**
     * @brief 새 오브젝트가 점프 시 움직인 거리입니다.
     */
    public float jumpMoveLength_ = 0.0f;

    
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


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트의 텍스처입니다.
     */
    private Texture texture_;
}