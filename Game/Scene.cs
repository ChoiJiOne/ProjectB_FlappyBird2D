using System.Collections.Generic;


/**
 * @brief 게임 내의 씬입니다.
 */
class Scene : GameObject
{
    /**
     * @brief 게임 내 씬의 속성에 대한 Getter/Setter입니다.
     */
    public bool DetectSwitch
    {
        get => bIsDetectSwitch_;
        set => bIsDetectSwitch_ = value;
    }

    public Scene NextScene
    {
        get => nextScene_;
        set => nextScene_ = value;
    }


    /**
     * @brief 씬에 진입합니다.
     * 
     * @note 이 메서드는 상속받는 하위 클래스에서 구현해야 합니다.
     */
    public virtual void Entry() 
    {
        Active = true;
    }


    /**
     * @brief 씬으로부터 나갑니다.
     * 
     * @note 이 메서드는 상속받는 하위 클래스에서 구현해야 합니다.
     */
    public virtual void Leave() 
    {
        NextScene.Entry();

        DetectSwitch = false;
        Active = false;
    }


    /**
     * @brief 씬 내의 게임 오브젝트를 삭제합니다.
     */
    public void CleanupGameObjects()
    {
        foreach (string gameObjectSignature in gameObjectSignatures_)
        {
            WorldManager.Get().RemoveGameObject(gameObjectSignature);
        }
    }


    /**
     * @brief 씬 오브젝트를 업데이트합니다.
     * 
     * @note 이 메서드는 상속받는 하위 클래스에서 구현해야 합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Tick(float deltaSeconds) 
    {
        if(bIsDetectSwitch_)
        {
            Leave();
        }
    }


    /**
     * @brief 씬의 전환을 확인합니다.
     */
    protected bool bIsDetectSwitch_ = false;


    /**
     * @brief 씬에 연결된 다음 씬입니다.
     */
    protected Scene nextScene_ = null;


    /**
     * @brief 씬 내의 게임 오브젝트 시그니처입니다.
     */
    protected List<string> gameObjectSignatures_ = null;
}