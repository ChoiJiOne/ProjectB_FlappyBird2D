using System.Collections.Generic;


/**
 * 게임의 시작 씬 입니다.
 */
class StartScene : Scene
{
    /**
     * @brief 게임의 시작 씬 노드에 입장합니다.
     */
    public override void Entry()
    {
        base.Entry();

        gameObjectSignatures_ = new List<string>();

        gameObjectSignatures_.Add("FlappyBirdSlate");
        gameObjectSignatures_.Add("BirdSlate");
        gameObjectSignatures_.Add("PlayButton");

        SlideSlate flappyBirdSlate = new SlideSlate();
        flappyBirdSlate.UpdateOrder = 1;
        flappyBirdSlate.Active = true;
        flappyBirdSlate.UITexture = "FlappyBird";
        flappyBirdSlate.Movable = true;
        flappyBirdSlate.MaxWaitTimeForMove = 1.0f;
        flappyBirdSlate.MoveLength = 20.0f;
        flappyBirdSlate.CreateUIBody(new Vector2<float>(500.0f, 200.0f), 400.0f, 100.0f);

        BirdSlate birdSlate = new BirdSlate();
        birdSlate.UpdateOrder = 1;
        birdSlate.Active = true;
        birdSlate.Movable = true;
        birdSlate.MaxWaitTimeForMove = 1.0f;
        birdSlate.MoveLength = 20.0f;
        birdSlate.ChangeWingStateTime = 0.09f;
        birdSlate.CreateUIBody(new Vector2<float>(750.0f, 200.0f), 70.0f, 50.0f);

        Button playButton = new Button();
        playButton.UpdateOrder = 2;
        playButton.Active = true;
        playButton.UITexture = "PlayButton";
        playButton.EventAction = () => 
        { 
            DetectSwitch = true;

            Sound doneSound = ContentManager.Get().GetSound("Done") as Sound;
            doneSound.Reset();
            doneSound.Play();
        };
        playButton.ReduceRatio = 0.95f;
        playButton.CreateUIBody(new Vector2<float>(500.0f, 400.0f), 200.0f, 120.0f);

        WorldManager.Get().AddGameObject("FlappyBirdSlate", flappyBirdSlate);
        WorldManager.Get().AddGameObject("BirdSlate", birdSlate);
        WorldManager.Get().AddGameObject("PlayButton", playButton);
    }


    /**
     * @brief 게임의 시작 씬 노드로 부터 나갑니다.
     */
    public override void Leave()
    {
        CleanupGameObjects();
        base.Leave();
    }
}