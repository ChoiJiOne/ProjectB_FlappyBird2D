#include "Assertion.h"
#include "Background.h"
#include "Button.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "Land.h"
#include "Panel.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "PlayScene.h"
#include "TTFont.h"

void PlayScene::Tick(float deltaSeconds)
{
	EntityManager::Get().UpdateBatch(entities_, deltaSeconds);

	RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);

	EntityManager::Get().RenderBatch(entities_);

	RenderManager::Get().EndFrame();
}

void PlayScene::Enter()
{
	CHECK(!bIsEnter_);

	static EUID background = EntityManager::Get().Create<Background>(50.0f);
	static EUID land = EntityManager::Get().Create<Land>(200.0f);

	entities_ = { background, land };

	bIsEnter_ = true;
}

void PlayScene::Exit()
{
	CHECK(bIsEnter_);

	bDetectSwitch_ = false;
	bIsEnter_ = false;
}