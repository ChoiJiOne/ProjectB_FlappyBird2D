using System.Collections.Generic;


/**
 * @brief 현재 게임 상태를 추적하는 게임 오브젝트입니다.
 */
class GameStateDetector : GameObject
{
    /**
     * @brief 현재 게임 씬의 상태를 나타내는 열거형입니다.
     */
    public enum EGameScene
    {
        START = 0x00,
        READY = 0x01,
        PLAY = 0x02,
        DONE = 0x03,
    }


    /**
     * @brief 현재 게임의 상태를 나타내는열거형입니다.
     */
    public enum EGameState
    {
        WAIT = 0x00,
        ENTRY = 0x01,
        PROCESSING = 0x02,
        LEAVE = 0x03,
    }


    /**
     * @brief 현재 게임 상태를 추적하는 게임 오브젝트의 Setter/Getter입니다.
     */
    public EGameScene GameScene
    {
        get => gameScene_;
    }

    public EGameState GameState
    {
        get => currGameState_;
        set => currGameState_ = value;
    }


    /**
     * @brief 현재 게임 상태를 추적하는 게임 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Tick(float deltaSeconds)
    {
        switch(gameScene_)
        {
            case EGameScene.START:
                UpdateStartScene(deltaSeconds);
                break;

            case EGameScene.READY:
                UpdateReadyScene(deltaSeconds);
                break;

            case EGameScene.PLAY:
                UpdatePlayScene(deltaSeconds);
                break;

            case EGameScene.DONE:
                UpdateDoneScene(deltaSeconds);
                break;
        }
    }


    /**
     * @brief 게임의 상태가 변경되었는지 확인합니다.
     * 
     * @return 게임의 상태가 변경되었다면 true, 그렇지 않으면 false를 반환합니다.
     */
    private bool IsSwitchGameState()
    {
        return prevGameState_ != currGameState_;
    }


    /**
     * @brief 게임의 시작씬에서 업데이트를 수행합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void UpdateStartScene(float deltaSeconds)
    {
        if (!IsSwitchGameState()) return;
    }


    /**
     * @brief 게임의 준비씬에서 업데이트를 수행합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void UpdateReadyScene(float deltaSeconds)
    {
        if (!IsSwitchGameState()) return;
    }


    /**
     * @brief 게임의 플레이씬에서 업데이트를 수행합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void UpdatePlayScene(float deltaSeconds)
    {
        if (!IsSwitchGameState()) return;
    }


    /**
     * @brief 게임의 종료씬에서 업데이트를 수행합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void UpdateDoneScene(float deltaSeconds)
    {
        if (!IsSwitchGameState()) return;
    }


    /**
     * @brief 현재 게임 씬입니다.
     */
    private EGameScene gameScene_ = EGameScene.START;


    /**
     * @brief 이전의 게임 상태입니다.
     */
    private EGameState prevGameState_ = EGameState.WAIT;


    /**
     * @brief 현재 게임 상태입니다.
     */
    private EGameState currGameState_ = EGameState.WAIT;
}