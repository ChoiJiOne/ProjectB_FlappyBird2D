#include "Assertion.h"
#include "Background.h"
#include "Button.h"
#include "EntityManager.h"
#include "MainTitle.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "StartScene.h"
#include "TTFont.h"

void StartScene::Tick(float deltaSeconds)
{
	EntityManager::Get().UpdateBatch(entities_, deltaSeconds);

	RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);
	EntityManager::Get().RenderBatch(entities_);
	RenderManager::Get().EndFrame();
}

void StartScene::Enter()
{
	CHECK(!bIsEnter_);

	static auto startEvent = [&]()
	{
		link_ = readyScene_;
		bDetectSwitch_ = true;
	};

	static auto settingEvent = [&]()
	{
		link_ = settingScene_;
		bDetectSwitch_ = true;
	};

	static RUID font90ID = ResourceManager::Get().Create<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 90.0f);
	static RUID font32ID = ResourceManager::Get().Create<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 32.0f);

	static EUID background = EntityManager::Get().Create<Background>();
	static EUID mainTitle = EntityManager::Get().Create<MainTitle>(font90ID);
	static EUID startButton = EntityManager::Get().Create<Button>("Resource/Button/Start.json", font32ID, EMouseButton::Left, startEvent);
	static EUID settingButton = EntityManager::Get().Create<Button>("Resource/Button/Setting.json", font32ID, EMouseButton::Left, settingEvent);
	static EUID quitButton = EntityManager::Get().Create<Button>("Resource/Button/Quit.json", font32ID, EMouseButton::Left, quitLoopEvent_);

	entities_ = { background, mainTitle, startButton, settingButton, quitButton, };

	bIsEnter_ = true;
}

void StartScene::Exit()
{
	CHECK(bIsEnter_);

	bDetectSwitch_ = false;
	bIsEnter_ = false;
}