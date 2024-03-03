#include "Assertion.h"
#include "Background.h"
#include "Bird.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "Land.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "ReadyScene.h"
#include "TTFont.h"

void ReadyScene::Tick(float deltaSeconds)
{
	EntityManager::Get().UpdateBatch(entityIDs_, deltaSeconds);

	RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);
	EntityManager::Get().RenderBatch(entityIDs_);
	RenderManager::Get().EndFrame();
}

void ReadyScene::Enter()
{
	CHECK(!bIsEnter_);
	
	ConfigManager::ELevel level = ConfigManager::Get().GetCurrentLevel();
	switch (level)
	{
	case ConfigManager::ELevel::Easy:
		gameSpeed_ = 150.0f;
		break;

	case ConfigManager::ELevel::Normal:
		gameSpeed_ = 200.0f;
		break;

	case ConfigManager::ELevel::Hard:
		gameSpeed_ = 300.0f;
		break;

	default:
		ASSERT(false, "undefined config level type");
		break;
	}

	EUID background = EntityManager::Get().Create<Background>(backgroundScrollSpeed_);
	EUID land = EntityManager::Get().Create<Land>(gameSpeed_);

	entityIDs_ =
	{
		background,
		land,
	};

	bIsEnter_ = true;
}

void ReadyScene::Exit()
{
	CHECK(bIsEnter_);

	bDetectSwitch_ = false;
	bIsEnter_ = false;
}