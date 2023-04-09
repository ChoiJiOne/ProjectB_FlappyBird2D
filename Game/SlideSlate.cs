/**
 * @brief 움직이는 슬레이트 UI입니다.
 */
class SlideSlate : Slate
{
    /**
     * @brief 움직이는 슬레이트 UI 속성에 대한 Getter/Setter입니다.
     */
    public bool Movable
    {
        get => bCanMove_;
        set => bCanMove_ = value;
    }

    public float MaxWaitTimeForMove
    {
        set => maxWaitTimeForMove_ = value;
    }

    public float MoveLength
    {
        set => moveLength_ = value;
    }


    /**
     * @brief 움직이는 슬레이트 UI를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Tick(float deltaSeconds)
    {
        if (bCanMove_)
        {
            waitTimeForMove_ += deltaSeconds;
            if (waitTimeForMove_ > maxWaitTimeForMove_)
            {
                waitTimeForMove_ = 0.0f;
                moveDirection_ *= -1.0f;
            }

            Vector2<float> center = UIBody.Center;
            center.y += (moveDirection_ * deltaSeconds * moveLength_);
            UIBody.Center = center;
        }

        base.Tick(deltaSeconds);
    }


    /**
     * @brief 움직이는 슬레이트 오브젝트가 움직일 수 있는지 확인합니다.
     */
    private bool bCanMove_ = true;


    /**
     * @brief 움직이는 슬레이트 오브젝트가 움직이는 방향입니다.
     * 
     * @note +는 아래 방향, -는 위 방향입니다.
     */
    private float moveDirection_ = 1.0f;


    /**
     * @brief 움직이는 슬레이트 오브젝트가 움직이기 위해 누적된 시간입니다.
     */
    private float waitTimeForMove_ = 0.0f;


    /**
     * @brief 움직이는 슬레이트 오브젝트가 움직이기 위해 기다릴 수 있는 최대 시간입니다.
     */
    private float maxWaitTimeForMove_ = 0.0f;


    /**
     * @brief 움직이는 슬레이트 오브젝트가 움직이는 거리입니다.
     */
    private float moveLength_ = 0.0f;
}