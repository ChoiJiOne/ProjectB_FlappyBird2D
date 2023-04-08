#include <string>
#include <cstdint>


/**
 * @brief 오디오 모듈을 초기화합니다.
 * 
 * @return 오디오 모듈 초기화에 성공하면 true, 그렇지 않으면 false를 반환합니다.
 */
extern "C" __declspec(dllexport) bool Setup();


/**
 * @brief 오디오 모듈의 초기화를 해제합니다.
 */
extern "C" __declspec(dllexport) void Cleanup();


/**
 * @brief 사운드 리소스를 생성합니다.
 *
 * @param Path 사운드 리소스의 경로입니다.
 *
 * @return 생성된 사운드 리소스의 아이디를 반환합니다. 생성에 실패하면 -1을 반환합니다.
 */
extern "C" __declspec(dllexport) int32_t CreateSound(const std::string& Path);


/**
 * @brief 사운드의 크기를 설정합니다.
 *
 * @param SoundID 크기를 설정할 사운드의 아이디입니다.
 * @param Volume 사운드의 크기입니다. 범위는 0.0 ~ 1.0 입니다.
 */
extern "C" __declspec(dllexport) void SetSoundVolume(int32_t SoundID, float Volume);


/**
 * @brief 현재 사운드의 크기를 얻습니다.
 *
 * @param SoundID 소리 크기를 얻을 사운드의 아이디입니다.
 *
 * @return 현재 사운드의 크기를 얻습니다.
 */
extern "C" __declspec(dllexport) float GetSoundVolume(int32_t SoundID);


/**
 * @brief 사운드의 반복 여부를 설정합니다.
 *
 * @param SoundID 반복 여부를 설정할 사운드의 아이디입니다.
 * @param bIsLoop 사운드 반복 여부입니다.
 */
extern "C" __declspec(dllexport) void SetSoundLooping(int32_t SoundID, bool bIsLoop);


/**
 * @brief 사운드의 반복 여부를 얻습니다.
 *
 * @param SoundID 반복 여부를 확인할 사운드의 아이디입니다.
 *
 * @return 사운드가 반복한다면 true, 그렇지 않다면 false를 반환합니다.
 */
extern "C" __declspec(dllexport) bool GetSoundLooping(int32_t SoundID);


/**
 * @brief 사운드를 플레이합니다.
 *
 * @note 이전에 중지한 적이 있다면 중지한 시점부터 플레이됩니다.
 *
 * @param SoundID 플레이할 사운드의 아이디입니다.
 */
extern "C" __declspec(dllexport) void PlaySound(int32_t SoundID);


/**
 * @brief 사운드가 플레이중인지 확인합니다.
 *
 * @param SoundID 플레이 중인지 확인할 사운드 아이디입니다.
 *
 * @return 사운드가 플레이 중이라면 true, 그렇지 않다면 false를 반환합니다.
 */
extern "C" __declspec(dllexport) bool IsPlayingSound(int32_t SoundID);


/**
 * @brief 사운드 플레이가 끝났는지 확인합니다.
 *
 * @param 플레이가 끝났는지 확인할 사운드 아이디입니다.
 *
 * @return 사운드 플레이가 끝났다면 true, 그렇지 않다면 false를 반환합니다.
 */
extern "C" __declspec(dllexport) bool IsDoneSound(int32_t SoundID);


/**
 * @brief 사운드 플레이를 중지합니다.
 *
 * @param SoundID 플레이를 중지할 사운드 아이디입니다.
 */
extern "C" __declspec(dllexport) void StopSound(int32_t SoundID);


/**
 * @brief 사운드 플레이가 중지 되었는지 확인합니다.
 *
 * @param SoundID 플레이가 중지 되었는지 확인할 사운드 아이디입니다.
 *
 * @return 사운드 플레이가 중지 되었다면 true, 그렇지 않으면 false를 반환합니다.
 */
extern "C" __declspec(dllexport) bool IsStoppingSound(int32_t SoundID);


/**
 * @brief 사운드를 초기화합니다.
 *
 * @param 초기화할 사운드의 아이디입니다.
 */
extern "C" __declspec(dllexport) void ResetSound(int32_t SoundID);