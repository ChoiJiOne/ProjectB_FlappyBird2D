using SDL2;


/**
 * @brief 게임 내에 시간 측정에 사용될 타이머 클래스입니다.
 */
class GameTimer
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
            return (float)(currTime_ - prevTime_);
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
            return (float)(stopTime_ - pausedTime_ - baseTime_);
        }
        else
        {
            return (float)(currTime_ - pausedTime_ - baseTime_);
        }
    }


	/**
	 * 타이머의 모든 상태를 리셋합니다.
	 */
	public void Reset()
    {
		ulong tick = SDL.SDL_GetTicks64();

        bIsStop_ = false;
        baseTime_ = tick;
        pausedTime_ = 0UL;
        stopTime_ = 0UL;
        prevTime_ = tick;
        currTime_ = tick;
    }


	/**
	 * 타이머를 시작합니다.
	 * 이때, 타이머가 중지되어 있다면 중시된 시점부터 시작됩니다.
	 */
	public void Start()
    {
        if (bIsStop_)
        {
			ulong tick = SDL.SDL_GetTicks64();

            pausedTime_ += (tick - stopTime_);
            prevTime_ = tick;
            stopTime_ = 0UL;

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
			ulong tick = SDL.SDL_GetTicks64();

			stopTime_ = tick;
			
            bIsStop_ = true;
        }
    }


	/**
	 * 타이머를 업데이트합니다.
	 */
	public void Tick()
    {
        prevTime_ = currTime_;
        currTime_ = SDL.SDL_GetTicks64();
    }


	/**
	 * 타이머의 중지 여부를 확인합니다.
	 */
	private bool bIsStop_ = false;


	/**
	 * 타이머가 시작된 시간입니다.
	 */
	private ulong baseTime_ = 0UL;


	/**
	 * 타이머가 중지된 동안의 누적 시간값 입니다.
	 */
	private ulong pausedTime_ = 0UL;


	/**
	 * 타이머가 중지된 시간입니다.
	 */
	private ulong stopTime_ = 0UL;


	/**
	 * 직전의 Tick 호출 시간입니다.
	 */
	private ulong prevTime_ = 0UL;


	/**
	 * Tick 호출 시간입니다.
	 */
	private ulong currTime_ = 0UL;
}