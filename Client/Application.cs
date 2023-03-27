using System;
using SDL2;
using System.Numerics;


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
        SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

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
    }


    /**
     * @brief FlappyBird2D 게임을 실행합니다.
     */
    public void Run()
    {
        SDL.SDL_Event e;
        while (!bIsDone_)
        {
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    bIsDone_ = true;
                }
            }

            SDL.SDL_SetRenderDrawColor(Renderer_, 255, 0, 0, 255);
            SDL.SDL_RenderClear(Renderer_);

            SDL.SDL_RenderPresent(Renderer_);
        }
    }


    /**
     * @brief FlappyBird2D 게임 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        SDL.SDL_DestroyRenderer(Renderer_);
        SDL.SDL_DestroyWindow(Window_);
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

        Game.Setup();
        Game.Run();
        Game.Cleanup();
    }
}