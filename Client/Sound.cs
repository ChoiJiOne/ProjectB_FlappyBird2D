using System;


/**
 * @brief 사운드를 로딩하고 관리하는 클래스입니다.
 */
class Sound : IContent
{
    /**
     * @brief 사운드를 로딩하고 관리하는 클래스의 생성자입니다.
     * 
     * @param path 사운드 리소스의 경로입니다.
     * 
     * @thorws 사운드 리소스 로딩에 실패하면 예외를 던집니다.
     */
    public Sound(string path)
    {
        soundID_ = AudioModule.CreateSound(path);
        if(soundID_ == -1)
        {
            throw new Exception("failed to create sound resource...");
        }
    }


    /**
     * @brief 사운드 리소스의 데이터를 명시적으로 정리합니다.
     */
    public void Release()
    {
    }


    /**
     * @brief 사운드의 크기를 설정합니다.
     *
     * @param volume 사운드의 크기입니다. 범위는 0.0 ~ 1.0 입니다.
     */
    public void SetVolume(float volume)
    {
        AudioModule.SetSoundVolume(soundID_, volume);
    }


    /**
     * @brief 현재 사운드의 크기를 얻습니다.
     *
     * @return 현재 사운드의 크기를 얻습니다.
     */
    public float GetVolume()
    {
        return AudioModule.GetSoundVolume(soundID_);
    }


    /**
     * @brief 사운드의 반복 여부를 설정합니다.
     *
     * @param bIsLoop 사운드 반복 여부입니다.
     */
    public void SetLooping(bool bIsLoop)
    {
        AudioModule.SetSoundLooping(soundID_, bIsLoop);
    }


    /**
     * @brief 사운드의 반복 여부를 얻습니다.
     *
     * @return 사운드가 반복한다면 true, 그렇지 않다면 false를 반환합니다.
     */
    public bool GetLooping()
    {
        return AudioModule.GetSoundLooping(soundID_);
    }


    /**
     * @brief 사운드를 플레이합니다.
     *
     * @note 이전에 중지한 적이 있다면 중지한 시점부터 플레이됩니다.
     */
    public void Play()
    {
        AudioModule.PlaySound(soundID_);
    }


    /**
     * @brief 사운드가 플레이중인지 확인합니다.
     *
     * @return 사운드가 플레이 중이라면 true, 그렇지 않다면 false를 반환합니다.
     */
    public bool IsPlaying()
    {
        return AudioModule.IsPlayingSound(soundID_);
    }


    /**
     * @brief 사운드 플레이가 끝났는지 확인합니다.
     * 
     * @return 사운드 플레이가 끝났다면 true, 그렇지 않다면 false를 반환합니다.
     */
    public bool IsDone()
    {
        return AudioModule.IsDoneSound(soundID_);
    }


    /**
     * @brief 사운드 플레이를 중지합니다.
     */
    public void Stop()
    {
        AudioModule.StopSound(soundID_);
    }


    /**
     * @brief 사운드 플레이가 중지 되었는지 확인합니다.
     * 
     * @return 사운드 플레이가 중지 되었다면 true, 그렇지 않으면 false를 반환합니다.
     */
    public bool IsStopping()
    {
        return AudioModule.IsStoppingSound(soundID_);
    }


    /**
     * @brief 사운드를 초기화합니다.
     *
     * @param 초기화할 사운드의 아이디입니다.
     */
    public void Reset()
    {
        AudioModule.ResetSound(soundID_);
    }


    /**
     * @brief 사운드 리소스의 아이디입니다.
     */
    private int soundID_ = 0;
}