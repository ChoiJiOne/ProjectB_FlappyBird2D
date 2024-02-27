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