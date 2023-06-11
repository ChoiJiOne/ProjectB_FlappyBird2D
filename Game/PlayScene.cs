using System.Collections.Generic;


/**
 * @brief 게임의 플레이 씬 입니다.
 */
class PlayScene : Scene
{
    /**
     * @brief 게임의 플레이 씬에 입장합니다.
     */
    public override void Entry()
    {
        base.Entry();

        Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
        Bird bird = WorldManager.Get().GetGameObject("Bird") as Bird;
        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;

        pipeDetector.CanGeneratePipe = true;

        Button pressButton = WorldManager.Get().GetGameObject("PressButton") as Button;
        pressButton.EventAction = () =>
        {
            pressButton.UITexture = (pressButton.UITexture == "PauseButton") ? "ContinueButton" : "PauseButton";

            bird.Movable = !bird.Movable;
            if (bird.State != Bird.EState.DONE)
            {
                floor.Movable = !floor.Movable;
                pipeDetector.CanGeneratePipe = !pipeDetector.CanGeneratePipe;

                List<Pipe> pipes = pipeDetector.DetectPipes;
                foreach (Pipe pipe in pipes)
                {
                    pipe.Movable = !pipe.Movable;
                }
            }
        };

        Number birdScore = new Number();
        birdScore.UpdateOrder = 6;
        birdScore.Active = true;
        birdScore.Center = new Vector2<float>(500.0f, 50.0f);
        birdScore.NumberWidth = 25.0f;
        birdScore.NumberHeight = 50.0f;
        birdScore.NumberGapLength = 5.0f;

        WorldManager.Get().AddGameObject("BirdScore", birdScore);

        InputManager.Get().BindWindowEventAction(
            EWindowEvent.FOCUS_LOST,
            () =>
            {
                pressButton.UITexture = "ContinueButton";

                bird.Movable = !bird.Movable;
                if (bird.State != Bird.EState.DONE)
                {
                    floor.Movable = !floor.Movable;
                    pipeDetector.CanGeneratePipe = !pipeDetector.CanGeneratePipe;

                    List<Pipe> pipes = pipeDetector.DetectPipes;
                    foreach (Pipe pipe in pipes)
                    {
                        pipe.Movable = !pipe.Movable;
                    }
                }
            }
        );
    }


    /**
     * @brief 게임의 플레이 씬으로부터 나갑니다.
     */
    public override void Leave()
    {
        Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;

        floor.Movable = false;
        pipeDetector.CanGeneratePipe = false;

        List<Pipe> pipes = pipeDetector.DetectPipes;
        foreach (Pipe pipe in pipes)
        {
            pipe.Movable = false;
        }

        InputManager.Get().UnbindWindowEventAction(EWindowEvent.FOCUS_LOST);

        base.Leave();
    }
}