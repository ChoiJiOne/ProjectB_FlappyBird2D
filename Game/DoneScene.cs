using System.Collections.Generic;


/**
 * @brief 게임의 종료 씬 입니다.
 */
class DoneScene : Scene
{
    /**
     * @brief 게임의 종료 씬에 입장합니다.
     */
    public override void Entry()
    {
        base.Entry();

        WorldManager.Get().RemoveGameObject("PressButton");

        gameObjectSignatures_ = new List<string>();

        gameObjectSignatures_.Add("GameOverSlate");
        gameObjectSignatures_.Add("OkButton");
        gameObjectSignatures_.Add("RankButton");
        gameObjectSignatures_.Add("ResultBoard");
        gameObjectSignatures_.Add("Floor");
        gameObjectSignatures_.Add("Bird");
        gameObjectSignatures_.Add("PipeDetector");
        gameObjectSignatures_.Add("BirdScore");

        Number birdScore = WorldManager.Get().GetGameObject("BirdScore") as Number;
        birdScore.UpdateOrder = 7;
        birdScore.Active = true;

        Vector2<float> center = birdScore.Center;
        center.x = 650.0f;
        center.y = 400.0f;
        birdScore.Center = center;

        SlideSlate gameOverSlate = new SlideSlate();
        gameOverSlate.UpdateOrder = 6;
        gameOverSlate.Active = true;
        gameOverSlate.UITexture = "GameOver";
        gameOverSlate.Movable = true;
        gameOverSlate.MaxWaitTimeForMove = 1.0f;
        gameOverSlate.MoveLength = 20.0f;
        gameOverSlate.CreateUIBody(new Vector2<float>(500.0f, 200.0f), 400.0f, 100.0f);

        Button rankButton = new Button();
        rankButton.UpdateOrder = 6;
        rankButton.Active = true;
        rankButton.UITexture = "RankButton";
        rankButton.EventAction = () =>
        {
            DetectSwitch = true;
            NextScene = WorldManager.Get().GetGameObject("RankScene") as Scene;

            Sound doneSound = ContentManager.Get().GetSound("Done") as Sound;
            doneSound.Reset();
            doneSound.Play();
        };
        rankButton.ReduceRatio = 0.95f;
        rankButton.CreateUIBody(new Vector2<float>(500.0f, 600.0f), 200.0f, 120.0f);
        
        Button okButton = new Button();
        okButton.UpdateOrder = 6;
        okButton.Active = true;
        okButton.UITexture = "OkButton";
        okButton.EventAction = () =>
        {
            DetectSwitch = true;
            NextScene = WorldManager.Get().GetGameObject("StartScene") as Scene;

            Sound doneSound = ContentManager.Get().GetSound("Done") as Sound;
            doneSound.Reset();
            doneSound.Play();
        };
        okButton.ReduceRatio = 0.95f;
        okButton.CreateUIBody(new Vector2<float>(500.0f, 750.0f), 160.0f, 60.0f);

        ResultBoard resultBoard = new ResultBoard();
        resultBoard.UpdateOrder = 6;
        resultBoard.Active = true;
        resultBoard.UIWidth = 200.0f;
        resultBoard.UIHeight = 50.0f;
        resultBoard.PlayerScorePosition = new Vector2<float>(400.0f, 400.0f);
        resultBoard.CreateBody(new Vector2<float>(500.0f, 400.0f), 500.0f, 200.0f);

        WorldManager.Get().AddGameObject("GameOverSlate", gameOverSlate);
        WorldManager.Get().AddGameObject("OkButton", okButton);
        WorldManager.Get().AddGameObject("RankButton", rankButton);
        WorldManager.Get().AddGameObject("ResultBoard", resultBoard);
    }


    /**
     * @brief 게임의 종료 씬으로부터 나갑니다.
     */
    public override void Leave()
    {
        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;
        List<Pipe> pipes = pipeDetector.DetectPipes;
        foreach (Pipe pipe in pipes)
        {
            string pipeSignature = string.Format("Pipe{0}", pipe.SignatureNumber);
            WorldManager.Get().RemoveGameObject(pipeSignature);
        }

        CleanupGameObjects();
        base.Leave();
    }
}