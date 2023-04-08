using System;
using System.Runtime.InteropServices;


/**
 * @brief 오디오 모듈 라이브러리의 C# 바인딩입니다.
 */
class AudioModule
{
    /**
     * @brief 오디오 모듈을 초기화합니다.
     * 
     * @return 오디오 모듈 초기화에 성공하면 true, 그렇지 않으면 false를 반환합니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "Setup", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool Setup();


    /**
     * @brief 오디오 모듈의 초기화를 해제합니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "Cleanup", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Cleanup();


    /**
     * @brief 사운드 리소스를 생성합니다.
     *
     * @param Path 사운드 리소스의 경로입니다.
     *
     * @return 생성된 사운드 리소스의 아이디를 반환합니다. 생성에 실패하면 -1을 반환합니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "CreateSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern int CreateSound([MarshalAs(UnmanagedType.LPStr)] string Path);


    /**
     * @brief 사운드의 크기를 설정합니다.
     *
     * @param SoundID 크기를 설정할 사운드의 아이디입니다.
     * @param Volume 사운드의 크기입니다. 범위는 0.0 ~ 1.0 입니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "SetSoundVolume", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetSoundVolume(int SoundID, float Volume);


    /**
     * @brief 현재 사운드의 크기를 얻습니다.
     *
     * @param SoundID 소리 크기를 얻을 사운드의 아이디입니다.
     *
     * @return 현재 사운드의 크기를 얻습니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "GetSoundVolume", CallingConvention = CallingConvention.Cdecl)]
    public static extern float GetSoundVolume(int SoundID);


    /**
     * @brief 사운드의 반복 여부를 설정합니다.
     *
     * @param SoundID 반복 여부를 설정할 사운드의 아이디입니다.
     * @param bIsLoop 사운드 반복 여부입니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "SetSoundLooping", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetSoundLooping(int SoundID, bool bIsLoop);


    /**
     * @brief 사운드의 반복 여부를 얻습니다.
     *
     * @param SoundID 반복 여부를 확인할 사운드의 아이디입니다.
     *
     * @return 사운드가 반복한다면 true, 그렇지 않다면 false를 반환합니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "GetSoundLooping", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool GetSoundLooping(int SoundID);


    /**
     * @brief 사운드를 플레이합니다.
     *
     * @note 이전에 중지한 적이 있다면 중지한 시점부터 플레이됩니다.
     *
     * @param SoundID 플레이할 사운드의 아이디입니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "PlaySound", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PlaySound(int SoundID);


    /**
     * @brief 사운드가 플레이중인지 확인합니다.
     *
     * @param SoundID 플레이 중인지 확인할 사운드 아이디입니다.
     *
     * @return 사운드가 플레이 중이라면 true, 그렇지 않다면 false를 반환합니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "IsPlayingSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsPlayingSound(int SoundID);


    /**
     * @brief 사운드 플레이가 끝났는지 확인합니다.
     *
     * @param 플레이가 끝났는지 확인할 사운드 아이디입니다.
     *
     * @return 사운드 플레이가 끝났다면 true, 그렇지 않다면 false를 반환합니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "IsDoneSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsDoneSound(int SoundID);


    /**
     * @brief 사운드 플레이를 중지합니다.
     *
     * @param SoundID 플레이를 중지할 사운드 아이디입니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "StopSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern void StopSound(int SoundID);


    /**
     * @brief 사운드 플레이가 중지 되었는지 확인합니다.
     *
     * @param SoundID 플레이가 중지 되었는지 확인할 사운드 아이디입니다.
     *
     * @return 사운드 플레이가 중지 되었다면 true, 그렇지 않으면 false를 반환합니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "IsStoppingSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool IsStoppingSound(int SoundID);


    /**
     * @brief 사운드를 초기화합니다.
     *
     * @param 초기화할 사운드의 아이디입니다.
     */
    [DllImport("AudioModule.dll", EntryPoint = "ResetSound", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ResetSound(int SoundID);
}