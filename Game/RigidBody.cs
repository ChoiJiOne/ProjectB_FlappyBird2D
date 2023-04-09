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
     * @brief 다른 강체와 충돌하는지 검사합니다.
     * 
     * @note AABB 기반의 충돌 검출을 수행합니다.
     * 
     * @param rigidBody 검사를 수행할 강체입니다.
     * 
     * @return 충돌한다면 true, 그렇지 않다면 false를 반환합니다.
     */
    public bool IsCollision(ref RigidBody rigidBody)
    {
        Vector2<float> minPosition, maxPosition;
        GetBoundPosition(out minPosition, out maxPosition);

        Vector2<float> otherMinPosition, otherMaxPosition;
        rigidBody.GetBoundPosition(out otherMinPosition, out otherMaxPosition);

        if (maxPosition.x <= otherMinPosition.x || minPosition.x >= otherMaxPosition.x)
        {
            return false;
        }

        if (maxPosition.y <= otherMinPosition.y || minPosition.y >= otherMaxPosition.y)
        {
            return false;
        }

        return true;
    }


    /**
     * @brief 강체의 경계 영역(최대-최소) 좌표를 얻습니다.
     * 
     * @param minPosition[out] 강체의 경계 영역 중 최소 좌표입니다.
     * @param maxPosition[out] 강체의 경계 영역 중 최대 좌표입니다.
     */
    public void GetBoundPosition(out Vector2<float> minPosition, out Vector2<float> maxPosition)
    {
        minPosition.x = center_.x - width_ / 2.0f;
        minPosition.y = center_.y - height_ / 2.0f;

        maxPosition.x = center_.x + width_ / 2.0f;
        maxPosition.y = center_.y + height_ / 2.0f;
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
