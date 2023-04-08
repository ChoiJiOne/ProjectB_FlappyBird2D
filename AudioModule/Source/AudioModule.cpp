#include "AudioModule.h"

#include <algorithm>
#include <memory>
#include <vector>

// @third party code - BEGIN
#include <miniaudio.h>
// @third party code - END

/**
 * @brief AudioManager가 초기화되었는지 확인합니다.
 */
static bool bIsSetup = false;

/**
 * @brief AudioManager에서 생성한 사운드 리소스 수입니다.
 */
static int32_t CountSoundResource = 0;

/**
 * @brief miniaudio 엔진입니다.
 */
static std::unique_ptr<ma_engine> MiniAudioEngine = nullptr;

/**
 * @brief 사운드 리소스 오브젝트입니다.
 */
static std::vector<std::unique_ptr<ma_sound>> Sounds;


bool Setup()
{
	if (bIsSetup) return false;

	MiniAudioEngine = std::make_unique<ma_engine>();
	if (ma_engine_init(nullptr, MiniAudioEngine.get()) != MA_SUCCESS)
	{
		return false;
	}

	Sounds.resize(0);
	bIsSetup = true;

	return true;
}

void Cleanup()
{
	if (!bIsSetup) return;

	for (auto& Sound : Sounds)
	{
		ma_sound_uninit(Sound.get());
		Sound.reset();
	}

	ma_engine_uninit(MiniAudioEngine.get());
	MiniAudioEngine.reset();

	bIsSetup = false;
}

int32_t CreateSound(const char* Path)
{
	std::unique_ptr<ma_sound> Sound = std::make_unique<ma_sound>();

	if (ma_sound_init_from_file(MiniAudioEngine.get(), Path, 0, nullptr, nullptr, Sound.get()) != MA_SUCCESS)
	{
		return -1;
	}

	Sounds.push_back(std::move(Sound));
	return CountSoundResource;
}

void SetSoundVolume(int32_t SoundID, float Volume)
{
	if (SoundID == -1) return;

	Volume = std::clamp<float>(Volume, 0.0f, 1.0f);
	ma_sound_set_volume(Sounds[SoundID].get(), Volume);
}

float GetSoundVolume(int32_t SoundID)
{
	if (SoundID == -1) return 0.0f;

	return ma_sound_get_volume(Sounds[SoundID].get());
}

void SetSoundLooping(int32_t SoundID, bool bIsLoop)
{
	if (SoundID == -1) return;

	ma_sound_set_looping(Sounds[SoundID].get(), static_cast<ma_bool32>(bIsLoop));
}

bool GetSoundLooping(int32_t SoundID)
{
	if (SoundID == -1) return false;

	return ma_sound_is_looping(Sounds[SoundID].get()) == MA_TRUE;
}

void PlaySound(int32_t SoundID)
{
	if (SoundID == -1) return;

	ma_sound_start(Sounds[SoundID].get());
}

bool IsPlayingSound(int32_t SoundID)
{
	if (SoundID == -1) return false;

	return ma_sound_is_playing(Sounds[SoundID].get()) == MA_TRUE;
}

bool IsDoneSound(int32_t SoundID)
{
	if (SoundID == -1) return false;

	return ma_sound_at_end(Sounds[SoundID].get()) == MA_TRUE;
}

void StopSound(int32_t SoundID)
{
	if (SoundID == -1) return;

	ma_sound_stop(Sounds[SoundID].get());
}

bool IsStoppingSound(int32_t SoundID)
{
	if (SoundID == -1) return false;

	return !IsDoneSound(SoundID) && !IsPlayingSound(SoundID);
}

void ResetSound(int32_t SoundID)
{
	if (SoundID == -1) return;

	ma_sound_seek_to_pcm_frame(Sounds[SoundID].get(), 0);
}
