using System;
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

    }


    /**
     * @brief FlappyBird2D 게임을 실행합니다.
     */
    public void Run()
    {

    }


    /**
     * @brief FlappyBird2D 게임 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {

    }


    /**
     * 
     */
    private bool bIsDone_ = false;
}


/**
 * @brief 클라이언트 애플리케이션을 실행합니다.
 */
class ClientApplication
{
    /**
     * @brief 클라이언트를 실행합니다.
     */
    static void Main(string[] args)
    {
        SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

        IntPtr window = SDL.SDL_CreateWindow(
            "FlappyBird2D",
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            1000, 800,
            SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
        );

        SDL.SDL_Event e;
        bool quit = false;
        while (!quit)
        {
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    quit = true;
                }
            }
        }

        SDL.SDL_DestroyWindow(window);
        SDL.SDL_Quit();
    }
}