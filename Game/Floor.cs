/**
 * @brief 게임의 바닥 오브젝트입니다.
 */
class Floor : GameObject
{
    /**
     * @brief 게임의 바닥 오브젝트 속성에 대한 Getter/Setter 입니다.
     */
    public float Speed
    {
        get => speed_;
        set => speed_ = value;
    }

    public bool Movable
    {
        get => bIsMove_;
        set => bIsMove_ = value;
    }

    public RigidBody Body
    {
        get => rigidBody_;
    }


    /**
     * @brief 바닥의 바디를 생성합니다.
     * 
     * @param center 바닥 바디의 중심 좌표입니다.
     * @param width 바닥 바디의 가로 크기입니다.
     * @param height 바닥 바디의 세로 크기입니다.
     */
    public void CreateBody(Vector2<float> center, float width, float height)
    {
        rigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief 게임의 바닥 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Tick(float deltaSeconds)
    {
        if (bIsMove_)
        {
            moveLength_ += (deltaSeconds * speed_);

            if (moveLength_ > rigidBody_.Width)
            {
                moveLength_ -= rigidBody_.Width;
            }
        }

        float factor = moveLength_ / rigidBody_.Width;

        Texture floorTexture = ContentManager.Get().GetTexture("Floor");

        RenderManager.Get().DrawHorizonScrollingTexture(
            ref floorTexture,
            rigidBody_.Center,
            rigidBody_.Width,
            rigidBody_.Height,
            factor
        );
    }


    /**
     * @brief 게임의 바닥이 현재 이동 중인지 확인합니다.
     */
    private bool bIsMove_ = false;


    /**
     * @brief 게임의 바닥이 이동한 거리입니다.
     */
    private float moveLength_ = 0.0f;


    /**
     * @brief 게임의 바닥 이동 속도입니다.
     */
    private float speed_ = 0.0f;


    /**
     * @brief 게임 바닥 오브젝트의 강체입니다.
     */
    private RigidBody rigidBody_;
}