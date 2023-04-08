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
    [DllImport("AudioModule.dll")]
    public static extern bool Setup();


    /**
     * @brief 오디오 모듈의 초기화를 해제합니다.
     */
    [DllImport("AudioModule.dll")]
    public static extern void Cleanup();
}