using System.Collections.Generic;


/**
 * @brief 게임의 플레이 씬 노드입니다.
 */
class PlaySceneNode : SceneNode
{
    /**
     * @brief 게임의 플레이 씬 노드에 입장합니다.
     */
    public override void Entry()
    {
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
    }


    /**
     * @brief 게임의 플레이 씬 노드로 부터 나갑니다.
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

        DetectSwitch = false;
    }
}