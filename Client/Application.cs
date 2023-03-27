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
     * 
     * @param Args 명령행 인수입니다.
     */
    public void Setup(string[] Args)
    {
        foreach (string Arg in Args)
        {
            string[] Tokens = Arg.Split('=');

            if(Tokens.Length == 2)
            {
                Arguments_.Add(Tokens[0], Tokens[1]);
            }
        }

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

        string ContentPath = Arguments_["Content"];

        Background_ = new Sprite();
        Background_.Center = new Vector2<float>(500.0f, 400.0f);
        Background_.Width = 1000.0f;
        Background_.Height = 800.0f;
        Background_.Rotate = 0.0f;
        Background_.LoadTexture(Renderer_, ContentPath + "Background.png");
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

            Background_.Update(GameTimer_.GetDeltaSeconds());
            Background_.Render(Renderer_);

            SDL.SDL_RenderPresent(Renderer_);
        }
    }


    /**
     * @brief FlappyBird2D 게임 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        Background_.ReleaseTexture();

        SDL.SDL_DestroyRenderer(Renderer_);
        SDL.SDL_DestroyWindow(Window_);

        SDL_image.IMG_Quit();
        SDL.SDL_Quit();
    }


    /**
     * @brief 명령행 인자의 키-값 쌍입니다.
     */
    private Dictionary<string, string> Arguments_ = new Dictionary<string, string>();


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
     * @brief 백그라운드 스프라이트입니다.
     */
    private Sprite Background_;
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
        FlappyBird2D Game = new FlappyBird2D();

        Game.Setup(Args);
        Game.Run();
        Game.Cleanup();
    }
}