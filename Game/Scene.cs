using System.Collections.Generic;


/**
 * @brief 게임 내의 씬입니다.
 */
class Scene : GameObject
{
    /**
     * @brief 게임 내 씬의 속성에 대한 Getter/Setter입니다.
     */
    public SceneNode CurrSceneNode
    {
        get => currentSceneNode_;
        set => currentSceneNode_ = value;
    }


    /**
     * @brief 씬 노드를 추가합니다.
     * 
     * @param sceneNode 추가할 씬 노드입니다.
     */
    public void AddSceneNode(SceneNode sceneNode)
    {
        sceneNodes_.AddLast(sceneNode);
    }


    /**
     * @brief 씬 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Tick(float deltaSeconds)
    {
        if(currentSceneNode_.DetectSwitch)
        {
            currentSceneNode_.Leave();
            currentSceneNode_ = currentSceneNode_.NextSceneNode;
            currentSceneNode_.Entry();
        }
    }

    
    /**
     *@brief 씬이 현재 가리키는 씬 노드입니다.
     */
    private SceneNode currentSceneNode_ = null;


    /**
     * @brief 씬 내의 노드 목록입니다.
     */
    private LinkedList<SceneNode> sceneNodes_ = new LinkedList<SceneNode>();
}