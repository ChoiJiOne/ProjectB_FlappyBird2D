using System;
using System.Collections.Generic;


/**
 * @brief 게임 월드의 게임 오브젝트를 관리하는 싱글턴 클래스입니다.
 */
class WorldManager
{
    /**
     * @brief 게임 오브젝트를 관리하는 월드 매니저의 인스턴스를 얻습니다.
     * 
     * @return 게임 오브젝트를 관리하는 월드 매니저의 인스턴스를 반환합니다.
     */
    public static WorldManager Get()
    {
        if(worldManager_ == null)
        {
            worldManager_ = new WorldManager();
        }

        return worldManager_;
    }


    /**
     * @brief 월드 매니저를 초기화합니다.
     */
    public void Setup()
    {
        if (bIsSetup_) return;

        bIsSetup_ = true;
    }


    /**
     * @brief 월드 매니저의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        if (!bIsSetup_) return;

        foreach (KeyValuePair<string, IGameObject> objectKeyValue in gameObjects_)
        {
            IGameObject gameObject = objectKeyValue.Value;
            gameObject.Cleanup();
        }

        bIsSetup_ = false;
    }


    /**
     * @brief 시그니처 값에 대응하는 게임 오브젝트가 있는지 확인합니다.
     * 
     * @param signature 게임 오브젝트가 있는지 확인할 시그니처 값입니다.
     * 
     * @return 시그니처 값에 대응하는 게임 오브젝트가 있다면 true, 그렇지 않다면 false를 반환합니다.
     */
    public bool IsValid(string signature)
    {
        return gameObjects_.ContainsKey(signature);
    }


    /**
     * @brief 월드에 게임 오브젝트를 추가합니다.
     * 
     * @param signature 게임 오브젝트의 시그니처입니다.
     * @param gameObject 추가할 게임 오브젝트입니다.
     * 
     * @throws 시그니처 값이 이미 존재하면 예외를 던집니다.
     */
    public void AddGameObject(string signature, IGameObject gameObject)
    {
        if (IsValid(signature))
        {
            throw new Exception("collision game object signature...");
        }

        gameObjects_.Add(signature, gameObject);
    }


    /**
     * @brief 월드에 게임 오브젝트를 삭제합니다.
     * 
     * @note 시그니처 값에 대응하는 게임 오브젝트가 없으면 아무 동작도 수행하지 않습니다.
     * 
     * @param signature 게임 오브젝트의 시그니처입니다.
     */
    public void RemoveGameObject(string signature)
    {
        if (!IsValid(signature)) return;

        gameObjects_.Remove(signature);
    }


    /**
     * @brief 생성자는 외부에서 호출할 수 없도록 감춤니다.
     */
    private WorldManager() { }


    /**
     * @brief 월드 매니저가 초기화된 적이 있는지 확인합니다.
     */
    private bool bIsSetup_ = false;


    /**
     * @brief 월드 매니저가 관리하는 게임 오브젝트입니다.
     */
    private Dictionary<string, IGameObject> gameObjects_ = new Dictionary<string, IGameObject>();


    /**
     * @brief 게임 오브젝트를 관리하는 월드 매니저의 인스턴스입니다.
     */
    private static WorldManager worldManager_;
}