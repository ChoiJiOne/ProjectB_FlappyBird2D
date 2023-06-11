using System.Collections.Generic;


/**
 * @brief 플레이어의 랭킹을 보여주는 씬입니다.
 */
class RankScene : Scene
{
    /**
     * @brief 플레이어의 랭킹 씬에 입장합니다.
     */
    public override void Entry()
    {
        base.Entry();

        gameObjectSignatures_ = new List<string>();
        gameObjectSignatures_.Add("OkButton");

        Button okButton = new Button();
        okButton.UpdateOrder = 6;
        okButton.Active = true;
        okButton.UITexture = "OkButton";
        okButton.EventAction = () =>
        {
            DetectSwitch = true;

            Sound doneSound = ContentManager.Get().GetSound("Done") as Sound;
            doneSound.Reset();
            doneSound.Play();
        };
        okButton.ReduceRatio = 0.95f;
        okButton.CreateUIBody(new Vector2<float>(500.0f, 600.0f), 160.0f, 60.0f);

        WorldManager.Get().AddGameObject("OkButton", okButton);
    }


    /**
     * @brief 플레이의 랭킹 씬으로부터 나갑니다.
     */
    public override void Leave()
    {
        CleanupGameObjects();
        base.Leave();
    }
}