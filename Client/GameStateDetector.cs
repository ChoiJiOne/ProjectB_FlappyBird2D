using System.Collections.Generic;


/**
 * @brief 현재 게임 상태를 추적하는 게임 오브젝트입니다.
 */
class GameStateDetector : GameObject
{
    /**
     * @brief 현재 게임 상태를 나타내는 열거형입니다.
     */
    public enum EGameState
    {
        READY = 0x01,
        PLAY = 0x02,
        DONE = 0x03,
    }


    /**
     * @brief 현재 게임 상태를 추적하는 게임 오브젝트의 Setter/Getter입니다.
     */
    public EGameState GameState
    {
        get => gameState_;
    }


    /**
     * @brief 현재 게임 상태를 추적하는 게임 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Update(float deltaSeconds)
    {
        switch(gameState_)
        {
            case EGameState.READY:
                UpdateReadyState(deltaSeconds);
                break;

            case EGameState.PLAY:
                UpdatePlayState(deltaSeconds);
                break;
        }
    }


    /**
     * @brief 현재 게임 상태를 추적하는 게임 오브젝트를 화면에 그립니다.
     * 
     * @note 게임 상태를 추적하는 게임 오브젝트는 화면에 렌더링을 수행하지 않습니다.
     */
    public override void Render() {}


    /**
     * @brief 대기 중인 상태의 업데이트를 수행합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void UpdateReadyState(float deltaSeconds)
    {
        if (gameState_ != EGameState.READY) return;

        bool bIsDetectSpace = InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED;

        if (bIsDetectSpace)
        {
            gameState_ = EGameState.PLAY;

            PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
            pipeDetector.CanGeneratePipe = true;
        }
    }


    /**
     * @brief 플레이 중인 상태의 업데이트를 수행합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    private void UpdatePlayState(float deltaSeconds)
    {
        if (gameState_ != EGameState.PLAY) return;

        Bird bird = WorldManager.Get().GetGameObject("Bird") as Bird;
        if (bird.State == Bird.EState.DONE)
        {
            Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
            floor.Movable = false;

            PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
            pipeDetector.CanGeneratePipe = false;

            List<Pipe> pipes = pipeDetector.DetectPipes;
            foreach (Pipe pipe in pipes)
            {
                pipe.Movable = false;
            }

            gameState_ = EGameState.DONE;
        }
    }


    /**
     * @brief 현재 게임 상태입니다.
     */
    EGameState gameState_ = EGameState.READY;
}