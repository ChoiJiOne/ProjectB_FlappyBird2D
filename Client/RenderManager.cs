using System;
using SDL2;


/**
 * @brief 렌더링을 수행하는 싱글턴 클래스입니다.
 */
class RenderManager
{
    /**
     * @brief 렌더링을 수행하는 렌더 매니저의 인스턴스를 얻습니다.
     * 
     * @return 렌더 매니저 인스턴스의 참조자를 반환합니다.
     */
    public static RenderManager Get()
    {
        if(renderManager_ == null)
        {
            renderManager_ = new RenderManager();
        }

        return renderManager_;
    }


    /**
     * @brief 렌더 매니저를 초기화합니다.
     * 
     * @param window 렌더러를 생성할 때 참조할 SDL 윈도우 포인터입니다.
     * @param bIsAccelerated 렌더러의 하드웨어 가속 여부입니다. 기본 값은 true입니다.
     * @param bIsSync 렌더러의 수직 동기화 여부입니다. 기본 값은 true입니다.
     * 
     * @throw 초기화에 실패하면 예외를 던집니다.
     */
    public void Setup(IntPtr window, bool bIsAccelerated = true, bool bIsSync = true)
    {
        int Flags = 0;
        Flags |= (bIsAccelerated ? (int)(SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED) : 0);
        Flags |= (bIsSync ? (int)(SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC) : 0);

        renderer_ = SDL.SDL_CreateRenderer(window, -1, (SDL.SDL_RendererFlags)(Flags));

        if (renderer_ == IntPtr.Zero)
        {
            throw new Exception("failed to create renderer...");
        }
    }


    /**
     * @brief 렌더 매니저 내의 리소스를 명시적으로 정리합니다.
     */
    public void Cleanup()
    {
        SDL.SDL_DestroyRenderer(renderer_);
    }


    /**
     * @brief SDL 렌더러의 포인터를 얻습니다.
     * 
     * @return SDL 렌더러의 포인터 값을 반환합니다.
     */
    public IntPtr GetRendererPtr()
    {
        return renderer_;
    }


    /**
     * @brief 백버퍼를 초기화합니다.
     * 
     * @param red 초기화 할 색상의 R값입니다. 범위는 0.0 ~ 1.0입니다.
     * @param green 초기화 할 색상의 G값입니다. 범위는 0.0 ~ 1.0입니다.
     * @param blue 초기화 할 색상의 B값입니다. 범위는 0.0 ~ 1.0입니다.
     * @param alpha 초기화 할 색상의 A값입니다. 범위는 0.0 ~ 1.0입니다.
     */
    public void Clear(float red, float green, float blue, float alpha)
    {
        SetDrawColor(red, green, blue, alpha);

        if(SDL.SDL_RenderClear(renderer_) != 0)
        {
            throw new Exception("failed to clear back buffer...");
        }
    }


    /**
     * @brief 백버퍼와 프론트 버퍼를 스왑합니다.
     */
    public void Present()
    {
        SDL.SDL_RenderPresent(renderer_);
    }


    /**
     * @brief 생성자는 외부에서 호출할 수 없도록 감춤니다.
     */
    private RenderManager() { }


    /**
     * @brief SDL 렌더링 컨텍스트에 색상을 설정합니다.
     * 
     * @param Red 설정할 색상의 R값입니다. 범위는 0.0 ~ 1.0입니다.
     * @param Green 설정할 색상의 G값입니다. 범위는 0.0 ~ 1.0입니다.
     * @param Blue 설정할 색상의 B값입니다. 범위는 0.0 ~ 1.0입니다.
     * @param Alpha 설정할 색상의 A값입니다. 범위는 0.0 ~ 1.0입니다.
     * 
     * @throws 색상 설정에 실패하면 예외를 던집니다.
     */
    private void SetDrawColor(float Red, float Green, float Blue, float Alpha)
    {
        byte r = (byte)(Red   * 255.0f);
        byte g = (byte)(Green * 255.0f);
        byte b = (byte)(Blue  * 255.0f);
        byte a = (byte)(Alpha * 255.0f);

        if(SDL.SDL_SetRenderDrawColor(renderer_, r, g, b, a) != 0)
        {
            throw new Exception("failed to set draw color...");
        }
    }


    /**
     * @brief SDL 렌더러의 포인터입니다.
     */
    private IntPtr renderer_;


    /**
     * @brief 렌더링을 수행하는 렌더 매니저의 인스턴스입니다.
     */
    private static RenderManager renderManager_;
}