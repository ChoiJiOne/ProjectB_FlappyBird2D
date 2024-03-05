#include "Assertion.h"
#include "ConfigManager.h"
#include "ResourceManager.h"
#include "Texture2D.h"

ConfigManager& ConfigManager::Get()
{
	static ConfigManager instance;
	return instance;
}

void ConfigManager::Startup()
{
	CHECK(!bIsStartup_);

	dayBackgroundID_ = ResourceManager::Get().Create<Texture2D>("Resource/Texture/Background_Day.png");
	nightBackgroundID_ = ResourceManager::Get().Create<Texture2D>("Resource/Texture/Background_Night.png");

	currentBackgroundID_ = dayBackgroundID_;

	bIsUpdateBestScore_ = false;

	recentScore_ = 
	{
		{ ELevel::Easy,   -1 },
		{ ELevel::Normal, -1 },
		{ ELevel::Hard,   -1 },
	};

	bestScore_ =
	{
		{ ELevel::Easy,   -1 },
		{ ELevel::Normal, -1 },
		{ ELevel::Hard,   -1 },
	};

	bIsStartup_ = true;
}

void ConfigManager::Shutdown()
{
	CHECK(bIsStartup_);

	bIsStartup_ = false;
}

void ConfigManager::SetCurrentBackgroundID(const EBackground& background)
{
	if (background == EBackground::Day)
	{
		currentBackgroundID_ = dayBackgroundID_;
	}
	else // background == EBackground::Night
	{
		currentBackgroundID_ = nightBackgroundID_;
	}
}

void ConfigManager::SetCurrentLevel(const ELevel& level)
{
	currentLevel_ = level;
}

void ConfigManager::SetCurrentBird(const EBird& bird)
{
	currentBird_ = bird;
}

void ConfigManager::RecordScore(int32_t score)
{
	if (score > bestScore_[currentLevel_])
	{
		bIsUpdateBestScore_ = true;
		bestScore_[currentLevel_] = score;
	}
	else
	{
		bIsUpdateBestScore_ = false;
	}

	recentScore_[currentLevel_] = score;
}

int32_t ConfigManager::GetRecentScore()
{
	return recentScore_[currentLevel_];
}

int32_t ConfigManager::GetBestScore()
{
	return bestScore_[currentLevel_];
}