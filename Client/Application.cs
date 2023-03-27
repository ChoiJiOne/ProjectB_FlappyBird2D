using System;
using System.Collections.Generic;
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
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CrashHandler.DetectApplicationCrash);

        SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
        SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG | SDL_image.IMG_InitFlags.IMG_INIT_JPG);

        Window_ = SDL.SDL_CreateWindow(
            "FlappyBird2D",
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            1000, 800,
            SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
        );

        Renderer_ = SDL.SDL_CreateRenderer(
            Window_,
            -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC
        );

        GameTimer_ = new Timer();

        string ContentPath = CommandLine.GetValue("Content");

        Background BGObject = new Background();
        BGObject.Center = new Vector2<float>(500.0f, 400.0f);
        BGObject.Width = 1000.0f;
        BGObject.Height = 800.0f;
        BGObject.SetTexture(new Texture(Renderer_, ContentPath + "Background.png"));

        Floor FloorObject = new Floor();
        FloorObject.Center = new Vector2<float>(500.0f, 700.0f);
        FloorObject.Width = 1000.0f;
        FloorObject.Height = 200.0f;
        FloorObject.Speed = 3.0f;
        FloorObject.Movable = true;
        FloorObject.SetTexture(new Texture(Renderer_, ContentPath + "Base.png"));

        GameObjects_.Add(BGObject);
        GameObjects_.Add(FloorObject);
    }


    /**
     * @brief FlappyBird2D 게임을 실행합니다.
     */
    public void Run()
    {
        GameTimer_.Reset();

        SDL.SDL_Event Event;
        while (!bIsDone_)
        {
            GameTimer_.Tick();

            while (SDL.SDL_PollEvent(out Event) != 0)
            {
                if (Event.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    bIsDone_ = true;
                }
            }

            SDL.SDL_SetRenderDrawColor(Renderer_, 0, 0, 0, 255);
            SDL.SDL_RenderClear(Renderer_);

            foreach (IGameObject GameObject in GameObjects_)
            {
                GameObject.Update(GameTimer_.GetDeltaSeconds());
                GameObject.Render(Renderer_);
            }

            SDL.SDL_RenderPresent(Renderer_);
        }
    }


    /**
     * @brief FlappyBird2D 게임 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        foreach (IGameObject GameObject in GameObjects_)
        {
            GameObject.Cleanup();
        }

        SDL.SDL_DestroyRenderer(Renderer_);
        SDL.SDL_DestroyWindow(Window_);

        SDL_image.IMG_Quit();
        SDL.SDL_Quit();
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
    private IntPtr Window_;


    /**
     * @brief SDL 렌더러의 포인터 값입니다.
     * 
     * @note 반드시 할당 해제 해주어야 합니다.
     */
    private IntPtr Renderer_;


    /**
     * @brief 게임 타이머입니다.
     */
    private Timer GameTimer_;


    /**
     * @brief 게임 내의 오브젝트들입니다.
     */
    private List<IGameObject> GameObjects_ = new List<IGameObject>();
}


/**
 * @brief 클라이언트 애플리케이션을 실행합니다.
 */
class ClientApplication
{
    /**
     * @brief 클라이언트를 실행합니다.
     * 
     * @param Args 명령행 인수입니다.
     */
    static void Main(string[] Args)
    {
        CommandLine.Parse(Args);
        FlappyBird2D Game = new FlappyBird2D();

        Game.Setup();
        Game.Run();
        Game.Cleanup();
    }
}