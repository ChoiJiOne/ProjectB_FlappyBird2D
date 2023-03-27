using SDL2;


/**
 * @brief 게임 내에 시간 측정에 사용될 타이머 클래스입니다.
 */
class Timer
{

	/**
	 * 타이머의 초 단위 델타 시간값을 얻습니다.
	 * 
	 * @return 초 단위의 델타 시간값을 반환합니다.
	 */
	public float GetDeltaSeconds()
    {
		return GetDeltaMilliseconds() / 1000.0f;
	}


	/**
	 * 타이머의 밀리초 단위의 델타 시간값을 얻습니다.
	 * 
	 * @return 밀리초 단위의 델타 시간값을 반환합니다.
	 */
	public float GetDeltaMilliseconds()
    {
        if (bIsStop_)
        {
            return 0.0f;
        }
        else
        {
            return (float)(CurrTime_ - PrevTime_);
        }
    }


	/**
	 * 타이머가 중지된 시간을 제외한 전체 시간값을 반환합니다.
	 * 
	 * @return 타이머가 시작된 이후의 중지된 시간을 제외한 전체 시간값을 반환합니다.
	 */
	public float GetTotalSeconds()
    {
		return GetTotalMilliseconds() / 1000.0f;
	}


	/**
	 * 타이머가 중지된 시간을 제외한 전체 시간값을 반환합니다.
	 *
	 * @return 타이머가 시작된 이후의 중지된 시간을 제외한 전체 시간값을 반환합니다.
	 */
	public float GetTotalMilliseconds()
    {
        if (bIsStop_)
        {
            return (float)(StopTime_ - PausedTime_ - BaseTime_);
        }
        else
        {
            return (float)(CurrTime_ - PausedTime_ - BaseTime_);
        }
    }


	/**
	 * 타이머의 모든 상태를 리셋합니다.
	 */
	public void Reset()
    {
		ulong TickTime = SDL.SDL_GetTicks64();

        bIsStop_ = false;
        BaseTime_ = TickTime;
        PausedTime_ = 0UL;
        StopTime_ = 0UL;
        PrevTime_ = TickTime;
        CurrTime_ = TickTime;
    }


	/**
	 * 타이머를 시작합니다.
	 * 이때, 타이머가 중지되어 있다면 중시된 시점부터 시작됩니다.
	 */
	public void Start()
    {
        if (bIsStop_)
        {
			ulong TickTime = SDL.SDL_GetTicks64();

            PausedTime_ += (TickTime - StopTime_);
            PrevTime_ = TickTime;
            StopTime_ = 0UL;

            bIsStop_ = false;
        }
    }


	/**
	 * 타이머를 중지시킵니다.
	 */
	public void Stop()
    {
        if (!bIsStop_)
        {
			ulong TickTime = SDL.SDL_GetTicks64();

			StopTime_ = TickTime;

            bIsStop_ = true;
        }
    }


	/**
	 * 타이머를 업데이트합니다.
	 */
	public void Tick()
    {
        PrevTime_ = CurrTime_;
        CurrTime_ = SDL.SDL_GetTicks64();
    }


	/**
	 * 타이머의 중지 여부를 확인합니다.
	 */
	private bool bIsStop_ = false;


	/**
	 * 타이머가 시작된 시간입니다.
	 */
	private ulong BaseTime_ = 0UL;


	/**
	 * 타이머가 중지된 동안의 누적 시간값 입니다.
	 */
	private ulong PausedTime_ = 0UL;


	/**
	 * 타이머가 중지된 시간입니다.
	 */
	private ulong StopTime_ = 0UL;


	/**
	 * 직전의 Tick 호출 시간입니다.
	 */
	private ulong PrevTime_ = 0UL;


	/**
	 * Tick 호출 시간입니다.
	 */
	private ulong CurrTime_ = 0UL;
}