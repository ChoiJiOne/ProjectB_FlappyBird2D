/**
 * @brief 게임 내의 오브젝트를 나타내는 인터페이스입니다.
 */
abstract class GameObject
{
    /**
     * @brief 게임 오브젝트 속성의 Getter/Setter입니다.
     */
    public bool Active
    {
        get => bIsActive_;
        set => bIsActive_ = value;
    }

    public int UpdateOrder
    {
        get => updateOrder_;
        set => updateOrder_ = value;
    }


    /**
     * @brief 게임 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public abstract void Tick(float deltaSeconds);


    /**
     * @brief 게임 오브젝트의 활성화 여부입니다.
     * 
     * @note 비활성화 되어 있다면, 업데이트와 렌더링을 수행하지 않습니다.
     */
    private bool bIsActive_ = false;


    /**
     * @brief 게임 오브젝트의 업데이트 순위입니다.
     */
    private int updateOrder_ = 0;
}