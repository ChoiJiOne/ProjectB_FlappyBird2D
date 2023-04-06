using System.Collections.Generic;


/**
 * @brief 게임의 준비 씬 노드입니다.
 */
class ReadySceneNode : SceneNode
{
    /**
     * @brief 게임의 준비 씬 노드에 입장합니다.
     */
    public override void Entry()
    {
        gameObjectSignatures_ = new List<string>();

        gameObjectSignatures_.Add("GetReadySlate");
        gameObjectSignatures_.Add("PressButtonExampleSlate");

        Floor floor = new Floor();
        Bird bird = new Bird();
        PipeDetector pipeDetector = new PipeDetector();
        Button pressButton = new Button();
        SlideSlate getReadySlate = new SlideSlate();
        Slate pressButtonExampleSlate = new Slate();

        floor.UpdateOrder = 2;
        floor.Active = true;
        floor.Speed = 300.0f;
        floor.Movable = true;
        floor.CreateBody(new Vector2<float>(500.0f, 750.0f), 1000.0f, 100.0f);

        bird.UpdateOrder = 3;
        bird.Active = true;
        bird.Movable = true;
        bird.CreateBody(new Vector2<float>(300.0f, 350.0f), 70.0f, 50.0f);

        pipeDetector.Active = true;
        pipeDetector.UpdateOrder = 4;
        pipeDetector.CanGeneratePipe = false;
        pipeDetector.MaxPipeCount = 7;
        pipeDetector.RespawnPosition = new Vector2<float>(1200.0f, 300.0f);
        pipeDetector.PipeToPipeGapLength = 300.0f;
        pipeDetector.PipeTopAndBottomGapLength = 200.0f;
        pipeDetector.TotalPipeWidth = 100.0f;
        pipeDetector.TotalPipeHeight = 700.0f;
        pipeDetector.MinPipeHeight = 100.0f;
        pipeDetector.PipeSpeed = 300.0f;

        pressButton.UpdateOrder = 6;
        pressButton.Active = true;
        pressButton.UITexture = "PauseButton";
        pressButton.EventAction = () =>
        {
            pressButton.UITexture = (pressButton.UITexture == "PauseButton") ? "ContinueButton" : "PauseButton";

            floor.Movable = !floor.Movable;
            bird.Movable = !bird.Movable;
            getReadySlate.Movable = !getReadySlate.Movable;
        };
        pressButton.ReduceRatio = 0.95f;
        pressButton.CreateUIBody(new Vector2<float>(50.0f, 50.0f), 52.0f, 56.0f);

        getReadySlate.UpdateOrder = 6;
        getReadySlate.Active = true;
        getReadySlate.UITexture = "GetReady";
        getReadySlate.Movable = true;
        getReadySlate.MaxWaitTimeForMove = 1.0f;
        getReadySlate.MoveLength = 20.0f;
        getReadySlate.CreateUIBody(new Vector2<float>(500.0f, 200.0f), 400.0f, 100.0f);

        pressButtonExampleSlate.UpdateOrder = 6;
        pressButtonExampleSlate.Active = true;
        pressButtonExampleSlate.UITexture = "PressButtonExample";
        pressButtonExampleSlate.CreateUIBody(new Vector2<float>(500.0f, 400.0f), 230.0f, 200.0f);

        WorldManager.Get().AddGameObject("Floor", floor);
        WorldManager.Get().AddGameObject("Bird", bird);
        WorldManager.Get().AddGameObject("PipeDetector", pipeDetector);
        WorldManager.Get().AddGameObject("PressButton", pressButton);
        WorldManager.Get().AddGameObject("GetReadySlate", getReadySlate);
        WorldManager.Get().AddGameObject("PressButtonExampleSlate", pressButtonExampleSlate);
    }


    /**
     * @brief 게임의 준비 씬 노드로 부터 나갑니다.
     */
    public override void Leave()
    {
        foreach (string gameObjectSignature in gameObjectSignatures_)
        {
            WorldManager.Get().RemoveGameObject(gameObjectSignature);
        }

        DetectSwitch = false;
    }


    /**
     * @brief 준비 씬 노드 내의 게임 오브젝트 시그니처입니다.
     */
    private List<string> gameObjectSignatures_ = null;
}