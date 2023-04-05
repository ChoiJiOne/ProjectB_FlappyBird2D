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
        get => gameState_;
    }


    /**
     * @brief 현재 게임 상태를 추적하는 게임 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Tick(float deltaSeconds)
    {

    }


    /**
     * @brief 현재 게임 씬입니다.
     */
    private EGameScene gameScene_ = EGameScene.START;


    /**
     * @brief 현재 게임 상태입니다.
     */
    private EGameState gameState_ = EGameState.WAIT;
}