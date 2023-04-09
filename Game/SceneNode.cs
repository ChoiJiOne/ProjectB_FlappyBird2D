/**
 * @brief 게임 씬 내의 노드의 추상 클래스입니다.
 */
abstract class SceneNode
{
    /**
     * @brief 게임 씬 내의 노드의 추상 클래스 속성에 대한 Getter/Setter입니다.
     */
    public bool DetectSwitch
    {
        get => bIsDetectSwitch_;
        set => bIsDetectSwitch_ = value;
    }

    public SceneNode NextSceneNode
    {
        get => nextSceneNode_;
        set => nextSceneNode_ = value;
    }


    /**
     * @brief 씬 노드에 진입합니다.
     */
    public abstract void Entry();


    /**
     * @brief 씬 노드로 부터 나갑니다.
     */
    public abstract void Leave();


    /**
     * @brief 씬 노드의 전환을 확인합니다.
     */
    private bool bIsDetectSwitch_ = false;


    /**
     * @brief 이 노드에 연결된 다음 노드입니다.
     */
    private SceneNode nextSceneNode_ = null;
}