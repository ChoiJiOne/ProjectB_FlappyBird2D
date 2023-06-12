using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using SDL2;


/**
 * @brief FlappyBird2D 게임을 초기화 및 실행합니다.
 */
class FlappyBird2D
{
    /**
     * @brief FlappyBird2D 게임을 초기화합니다.
     */
    public void Setup()
    {
        SetupCoreProperties();
        LoadTextureResource();
        LoadSoundResource();
        LoadDatabase();
        LoadTTFont();
        GenerateGameObjects();
    }


    /**
     * @brief FlappyBird2D 게임을 실행합니다.
     */
    public void Run()
    {
        gameTimer_.Reset();
        
        while (!bIsDone_)
        {
            gameTimer_.Tick();
            InputManager.Get().Tick();

            RenderManager.Get().Clear(Color.BLACK);
            WorldManager.Get().Tick(gameTimer_.GetDeltaSeconds());

            RenderManager.Get().Present();
        }
    }


    /**
     * @brief FlappyBird2D 게임 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        SDL.SDL_DestroyWindow(window_);

        WorldManager.Get().Cleanup();
        ContentManager.Get().Cleanup();
        RenderManager.Get().Cleanup();

        AudioModule.Cleanup();
        SDL_image.IMG_Quit();
        SDL.SDL_Quit();
    }


    /**
     * @brief 게임 진행에 필요한 핵심 요소들을 초기화합니다.
     * 
     * @throws 게임 진행에 필요한 핵심 요소들의 초기화에 실패하면 예외를 던집니다.
     */
    private void SetupCoreProperties()
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) != 0)
        {
            throw new Exception("failed to initialize SDL...");
        }

        int flags = (int)(SDL_image.IMG_InitFlags.IMG_INIT_PNG | SDL_image.IMG_InitFlags.IMG_INIT_JPG);
        int initFlag = SDL_image.IMG_Init((SDL_image.IMG_InitFlags)(flags));

        if ((initFlag & flags) != flags)
        {
            throw new Exception("failed to initialize SDL_image...");
        }

        if(!AudioModule.Setup())
        {
            throw new Exception("failed to initialize audio module...");
        }

        window_ = SDL.SDL_CreateWindow(
            "FlappyBird2D",
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            1000, 800,
            SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
        );

        if (window_ == IntPtr.Zero)
        {
            throw new Exception("failed to create window...");
        }

        InputManager.Get().Setup();
        RenderManager.Get().Setup(window_);
        ContentManager.Get().Setup(CommandLine.GetValue("Content"));
        WorldManager.Get().Setup();

        InputManager.Get().BindWindowEventAction(EWindowEvent.CLOSE, () => { bIsDone_ = true; });
    }


    /**
     * @brief 전체 텍스처 리소스를 로딩합니다.
     * 
     * @throws 텍스처 리소스 생성에 실패하면 예외를 던집니다.
     */
    private void LoadTextureResource()
    {
        string[] pngImageFilePaths = System.IO.Directory.GetFiles(CommandLine.GetValue("Content") + "Texture\\", "*.png");
        Dictionary<string, string> textures = new Dictionary<string, string>();

        foreach(string pngImageFilePath in pngImageFilePaths)
        {
            string[] tokens = pngImageFilePath.Split('\\');
            string pngImageFile = tokens.Last();

            string[] pngImageFileTokens = pngImageFile.Split('.');
            textures.Add(pngImageFileTokens.First(), pngImageFile);
        }

        foreach(KeyValuePair<string, string> texture in textures)
        {
            ContentManager.Get().CreateTexture(texture.Key, "Texture\\" + texture.Value);
        }
    }


    /**
     * @brief 전체 사운드 리소스를 로딩합니다.
     * 
     * @throws 사운드 리소스 생성에 실패하면 예외를 던집니다.
     */
    private void LoadSoundResource()
    {
        string[] soundFilePaths = System.IO.Directory.GetFiles(CommandLine.GetValue("Content") + "Audio\\", "*.mp3");
        Dictionary<string, string> sounds = new Dictionary<string, string>();

        foreach (string soundFilePath in soundFilePaths)
        {
            string[] tokens = soundFilePath.Split('\\');
            string soundFile = tokens.Last();

            string[] soundFileTokens = soundFile.Split('.');
            sounds.Add(soundFileTokens.First(), soundFile);
        }

        foreach (KeyValuePair<string, string> sound in sounds)
        {
            Sound soundResource = ContentManager.Get().CreateSound(sound.Key, "Audio\\" + sound.Value);
            soundResource.SetLooping(false);
        }
    }


    /**
     * @brief 데이터베이스를 로딩합니다.
     * 
     * @throws 데이터베이스 로딩에 실패하면 예외를 던집니다.
     */
    private void LoadDatabase()
    {
        string playRankDBFile = CommandLine.GetValue("Content") + "DB\\PlayRank.db";
        bool bIsCreateDBFile = false;

        if (!System.IO.File.Exists(playRankDBFile))
        {
            bIsCreateDBFile = true;
            SQLiteConnection.CreateFile(playRankDBFile);
        }

        Database playRankDB = ContentManager.Get().CreateDatabase("PlayRank", "DB\\PlayRank.db");

        if(bIsCreateDBFile)
        {
            playRankDB.Execute("CREATE TABLE \"PlayRank\" (" +
                "\"Time\"	TEXT NOT NULL," +
                "\"Pipe\"	INTEGER NOT NULL," +
                "PRIMARY KEY(\"Time\",\"Pipe\")" +
            ")");
        }
    }


    /**
     * @brief 트루 타입 폰트를 로딩합니다.
     * 
     * @throws 트루 타입 폰트 로딩에 실패하면 예외를 던집니다.
     */
    private void LoadTTFont()
    {
        ContentManager.Get().CreateTTFont("SeoulNamsanEB", "Font\\SeoulNamsanEB.ini", "Font\\SeoulNamsanEB.png");
    }


    /**
     * @brief 게임 오브젝트들을 생성합니다.
     */
    private void GenerateGameObjects()
    {
        Background background = new Background();
        background.UpdateOrder = 0;
        background.Active = true;
        background.CreateBody(new Vector2<float>(500.0f, 400.0f), 1000.0f, 800.0f);

        StartScene startScene = new StartScene();
        startScene.UpdateOrder = 8;
        startScene.Active = false;
        
        ReadyScene readyScene = new ReadyScene();
        readyScene.UpdateOrder = 8;
        readyScene.Active = false;

        PlayScene playScene = new PlayScene();
        playScene.UpdateOrder = 8;
        playScene.Active = false;

        DoneScene doneScene = new DoneScene();
        doneScene.UpdateOrder = 8;
        doneScene.Active = false;

        RankScene rankScene = new RankScene();
        rankScene.UpdateOrder = 8;
        rankScene.Active = false;

        startScene.NextScene = readyScene;
        readyScene.NextScene = playScene;
        playScene.NextScene = doneScene;
        doneScene.NextScene = startScene;
        rankScene.NextScene = startScene;

        startScene.Entry();

        WorldManager.Get().AddGameObject("Background", background);
        WorldManager.Get().AddGameObject("StartScene", startScene);
        WorldManager.Get().AddGameObject("ReadyScene", readyScene);
        WorldManager.Get().AddGameObject("PlayScene", playScene);
        WorldManager.Get().AddGameObject("DoneScene", doneScene);
        WorldManager.Get().AddGameObject("RankScene", rankScene);
    }


    /**
     * @brief 루프를 종료할 지 확인합니다.
     */
    private bool bIsDone_ = false;


    /**
     * @brief SDL 윈도우의 포인터 값입니다.
     * 
     * @note 반드시 할당 해제 해주어야 합니다.
     */
    private IntPtr window_;


    /**
     * @brief 게임 타이머입니다.
     */
    private GameTimer gameTimer_ = new GameTimer();
}


/**
 * @brief 클라이언트 애플리케이션을 실행합니다.
 */
class ClientApplication
{
    /**
     * @brief 클라이언트를 실행합니다.
     * 
     * @param args 명령행 인수입니다.
     */
    static void Main(string[] args)
    {
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CrashHandler.DetectApplicationCrash);

#if SHIPPING
        ConsoleHandler.SetVisible(false);
#endif

        CommandLine.Parse(args);
        FlappyBird2D game = new FlappyBird2D();

        game.Setup();
        game.Run();
        game.Cleanup();
    }
}