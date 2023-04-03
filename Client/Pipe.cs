using System;


/**
 * @brief 게임의 파이프 오브젝트입니다.
 */
class Pipe : GameObject
{
    /**
     * @brief 파이프 오브젝트의 상태를 정의합니다.
     */
    public enum EState
    {
        WAIT = 0x00, // 백그라운드 밖에서 대기 중인 상태입니다.
        ENTRY = 0x01, // 백그라운드 안으로 들어온 상태입니다.
        LEAVE = 0x02, // 백그라운드 밖으로 나간 상태입니다.
    }


    /**
     * @brief 게임의 파이프 오브젝트 속성에 대한 Getter/Setter 입니다.
     */
    public bool Movable
    {
        get => bIsMove_;
        set => bIsMove_ = value;
    }

    public float Speed
    {
        get => speed_;
        set => speed_ = value;
    }

    public int SignatureNumber
    {
        get => signatureNumber_;
        set => signatureNumber_ = value;
    }

    public EState State
    {
        get => currentState_;
        set => currentState_ = value;
    }

    public RigidBody TopRigidBody
    {
        get => topRigidBody_;
        set => topRigidBody_ = value;
    }

    public RigidBody BottomRigidBody
    {
        get => bottomRigidBody_;
        set => bottomRigidBody_ = value;
    }


    /**
     * @brief 게임의 파이프 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Update(float deltaSeconds)
    {
        if (currentState_ == EState.LEAVE) return;

        if(bIsMove_)
        {
            Vector2<float> topCenter = topRigidBody_.Center;
            topCenter.x -= (deltaSeconds * speed_);
            topRigidBody_.Center = topCenter;

            Vector2<float> bottomCenter = bottomRigidBody_.Center;
            bottomCenter.x -= (deltaSeconds * speed_);
            bottomRigidBody_.Center = bottomCenter;
        }

        Background background = WorldManager.Get().GetGameObject("Background") as Background;
        switch(currentState_)
        {
            case EState.WAIT:
                CheckEntryFromBackground(background);
                break;

            case EState.ENTRY:
                CheckLeaveFromBackground(background);
                break;

            case EState.LEAVE:
                break;
        }
    }


    /**
     * @brief 게임의 파이프 오브젝트를 화면에 그립니다.
     */
    public override void Render()
    {
        Texture topPipeTexture = ContentManager.Get().GetTexture("PipeTop");
        
        RenderManager.Get().DrawTexture(
            ref topPipeTexture,
            topRigidBody_.Center,
            topRigidBody_.Width,
            topRigidBody_.Height
        );

        Texture bottomPipeTexture = ContentManager.Get().GetTexture("PipeBottom");

        RenderManager.Get().DrawTexture(
            ref bottomPipeTexture,
            bottomRigidBody_.Center,
            bottomRigidBody_.Width,
            bottomRigidBody_.Height
        );
    }


    /**
     * @brief 파이프가 백그라운드 밖으로 나갔는지 확인합니다.
     * 
     * @param background 백그라운드 오브젝트입니다.
     */
    private void CheckLeaveFromBackground(Background background)
    {
        if (currentState_ != EState.ENTRY) return;

        if (!background.Body.IsCollision(ref topRigidBody_) || !background.Body.IsCollision(ref bottomRigidBody_))
        {
            currentState_ = EState.LEAVE;
        }
    }


    /**
     * @brief 파이프가 백그라운드 내부로 들어왔는지 확인합니다.
     * 
     * @param background 백그라운드 오브젝트입니다.
     */
    private void CheckEntryFromBackground(Background background)
    {
        if (currentState_ != EState.WAIT) return;

        if (background.Body.IsCollision(ref topRigidBody_) && background.Body.IsCollision(ref bottomRigidBody_))
        {
            currentState_ = EState.ENTRY;
        }
    }

    
    /**
     * @brief 파이프가 이동할 수 있는지 확인합니다.
     */
    private bool bIsMove_ = false;


    /**
     * @brief 파이프의 이동 속도입니다.
     */
    private float speed_ = 0.0f;


    /**
     * @brief 파이프의 고유 넘버입니다.
     */
    private int signatureNumber_ = -1;


    /**
     * @brief 파이프의 상태입니다.
     */
    private EState currentState_ = EState.WAIT;


    /**
     * @brief 파이프의 상단 강체입니다.
     */
    private RigidBody topRigidBody_;


    /**
     * @brief 파이프의 하단 강체입니다.
     */
    private RigidBody bottomRigidBody_;
}