/**
 * @brief 오브젝트의 강체(RigidBody)입니다.
 * 
 * @note 이 강체는 AABB 기반입니다.
 */
class RigidBody
{
    /**
     * @brief 오브젝트 강체의 생성자입니다.
     * 
     * @param center 오브젝트 강체의 중심 좌표입니다.
     * @param width 오브젝트 강체의 가로 크기입니다.
     * @param height 오브젝트 강체의 세로 크기입니다.
     */
    public RigidBody(Vector2<float> center, float width, float height)
    {
        center_ = center;
        width_ = width;
        height_ = height;
    }


    /**
     * @brief 오브젝트 강체 속성에 대한 Getter/Setter 입니다.
     */
    public Vector2<float> Center
    {
        get => center_;
        set => center_ = value;
    }

    public float Width
    {
        get => width_;
        set => width_ = value;
    }

    public float Height
    {
        get => height_;
        set => height_ = value;
    }


    /**
     * @brief 강체의 중심 좌표입니다.
     */
    private Vector2<float> center_;


    /**
     * @brief 강체의 가로 크기입니다.
     */
    private float width_ = 0.0f;


    /**
     * @brief 강체의 세로 크기입니다.
     */
    private float height_ = 0.0f;
}
