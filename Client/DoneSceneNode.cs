using System.Collections.Generic;


/**
 * @brief 게임의 종료 씬 노드입니다.
 */
class DoneSceneNode : SceneNode
{
    /**
     * @brief 게임의 종료 씬 노드에 입장합니다.
     */
    public override void Entry()
    {
        WorldManager.Get().RemoveGameObject("PressButton");

        gameObjectSignatures_ = new List<string>();

        gameObjectSignatures_.Add("OkButton");
        gameObjectSignatures_.Add("ResultBoard");
        gameObjectSignatures_.Add("Floor");
        gameObjectSignatures_.Add("Bird");
        gameObjectSignatures_.Add("PipeDetector");
        gameObjectSignatures_.Add("ScoreBoard");

        ScoreBoard scoreBoard = WorldManager.Get().GetGameObject("ScoreBoard") as ScoreBoard;
        scoreBoard.UpdateOrder = 7;
        scoreBoard.Active = true;

        Vector2<float> center = scoreBoard.Center;
        center.x = 600.0f;
        center.y = 450.0f;
        scoreBoard.Center = center;

        Button okButton = new Button();
        okButton.UpdateOrder = 6;
        okButton.Active = true;
        okButton.UITexture = "OkButton";
        okButton.EventAction = () => { DetectSwitch = true; };
        okButton.ReduceRatio = 0.95f;
        okButton.CreateUIBody(new Vector2<float>(500.0f, 600.0f), 160.0f, 60.0f);

        ResultBoard resultBoard = new ResultBoard();
        resultBoard.UpdateOrder = 6;
        resultBoard.Active = true;
        resultBoard.UIWidth = 200.0f;
        resultBoard.UIHeight = 50.0f;
        resultBoard.PlayerIDPosition = new Vector2<float>(400.0f, 350.0f);
        resultBoard.PlayerScorePosition = new Vector2<float>(400.0f, 450.0f);
        resultBoard.CreateBody(new Vector2<float>(500.0f, 400.0f), 500.0f, 300.0f);

        WorldManager.Get().AddGameObject("OkButton", okButton);
        WorldManager.Get().AddGameObject("ResultBoard", resultBoard);
    }


    /**
     * @brief 게임의 종료 씬 노드에서 나갑니다.
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

        foreach (string gameObjectSignature in gameObjectSignatures_)
        {
            WorldManager.Get().RemoveGameObject(gameObjectSignature);
        }

        DetectSwitch = false;
    }


    /**
     * @brief 종료 씬 노드 내의 게임 오브젝트 시그니처입니다.
     */
    private List<string> gameObjectSignatures_ = null;
}