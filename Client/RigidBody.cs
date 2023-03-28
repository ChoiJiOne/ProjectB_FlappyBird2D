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
     * @param Center 오브젝트 강체의 중심 좌표입니다.
     * @param Width 오브젝트 강체의 가로 크기입니다.
     * @param Height 오브젝트 강체의 세로 크기입니다.
     */
    public RigidBody(Vector2<float> Center, float Width, float Height)
    {
        Center_ = Center;
        Width_ = Width;
        Height_ = Height;
    }


    /**
     * @brief 오브젝트 강체 속성에 대한 Getter/Setter 입니다.
     */
    public Vector2<float> Center
    {
        get => Center_;
        set => Center_ = value;
    }

    public float Width
    {
        get => Width_;
        set => Width_ = value;
    }

    public float Height
    {
        get => Height_;
        set => Height_ = value;
    }


    /**
     * @brief 강체의 중심 좌표입니다.
     */
    private Vector2<float> Center_;


    /**
     * @brief 강체의 가로 크기입니다.
     */
    private float Width_ = 0.0f;


    /**
     * @brief 강체의 세로 크기입니다.
     */
    private float Height_ = 0.0f;
}
