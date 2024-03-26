#include "Assertion.h"
#include "AudioManager.h"
#include "Background.h"
#include "Button.h"
#include "EntityManager.h"
#include "MainTitle.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "StartScene.h"
#include "Sound.h"
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

	static RUID clickSound = ResourceManager::Get().CreateID<Sound>("Resource/Sound/swoosh.wav");

	static auto startEvent = [&]()
	{
		Sound* sound = ResourceManager::Get().GetResource<Sound>(clickSound);
		sound->Reset();
		sound->SetLooping(false);
		sound->Play();

		link_ = readyScene_;
		bDetectSwitch_ = true;
	};

	static auto settingEvent = [&]()
	{
		link_ = settingScene_;
		bDetectSwitch_ = true;
	};

	static RUID font90ID = ResourceManager::Get().CreateID<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 90.0f);
	static RUID font32ID = ResourceManager::Get().CreateID<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 32.0f);

	static EUID background = EntityManager::Get().CreateID<Background>();
	static EUID mainTitle = EntityManager::Get().CreateID<MainTitle>(font90ID);
	static EUID startButton = EntityManager::Get().CreateID<Button>("Resource/Button/Start.json", font32ID, EMouseButton::Left, startEvent);
	static EUID settingButton = EntityManager::Get().CreateID<Button>("Resource/Button/Setting.json", font32ID, EMouseButton::Left, settingEvent);
	static EUID quitButton = EntityManager::Get().CreateID<Button>("Resource/Button/Quit.json", font32ID, EMouseButton::Left, quitLoopEvent_);

	entities_ = 
	{ 
		background, 
		mainTitle, 
		startButton, 
		settingButton, 
		quitButton, 
	};

	bIsEnter_ = true;
}

void StartScene::Exit()
{
	CHECK(bIsEnter_);

	bDetectSwitch_ = false;
	bIsEnter_ = false;
}