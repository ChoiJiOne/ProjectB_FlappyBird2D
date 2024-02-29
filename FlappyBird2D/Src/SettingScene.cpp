#include "Assertion.h"
#include "Background.h"
#include "Button.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "Panel.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "SettingScene.h"
#include "TTFont.h"

void SettingScene::Tick(float deltaSeconds)
{
	EntityManager::Get().UpdateBatch(entities_, deltaSeconds);

	RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);

	EntityManager::Get().RenderBatch(entities_);

	RenderManager::Get().EndFrame();
}

void SettingScene::Enter()
{
	CHECK(!bIsEnter_);

	static RUID font32ID = ResourceManager::Get().Create<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 32.0f);
	static RUID font64ID = ResourceManager::Get().Create<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 64.0f);

	static RUID background = EntityManager::Get().Create<Background>();

	static RUID settingPanel = EntityManager::Get().Create<Panel>("Resource/Panel/Setting.json", font64ID);
	static RUID backgroundPanel = EntityManager::Get().Create<Panel>("Resource/Panel/Background.json", font32ID);
	static RUID levelPanel = EntityManager::Get().Create<Panel>("Resource/Panel/Level.json", font32ID);
	static RUID birdPanel = EntityManager::Get().Create<Panel>("Resource/Panel/Bird.json", font32ID);

	static RUID dayButton = EntityManager::Get().Create<Button>("Resource/Button/Day.json", font32ID, EMouseButton::Left, 
		[&]() 
		{
			ConfigManager::Get().SetCurrentBackgroundID(ConfigManager::EBackground::Day);
		}
	);

	static RUID nightButton = EntityManager::Get().Create<Button>("Resource/Button/Night.json", font32ID, EMouseButton::Left,
		[&]()
		{
			ConfigManager::Get().SetCurrentBackgroundID(ConfigManager::EBackground::Night);
		}
	);

	static RUID easyButton = EntityManager::Get().Create<Button>("Resource/Button/Easy.json", font32ID, EMouseButton::Left,
		[&]()
		{
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Easy);
		}
	);

	static RUID normalButton = EntityManager::Get().Create<Button>("Resource/Button/Normal.json", font32ID, EMouseButton::Left,
		[&]()
		{
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Normal);
		}
	);

	static RUID hardButton = EntityManager::Get().Create<Button>("Resource/Button/Hard.json", font32ID, EMouseButton::Left,
		[&]()
		{
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Hard);
		}
	);

	static RUID blueButton = EntityManager::Get().Create<Button>("Resource/Button/Blue.json", font32ID, EMouseButton::Left, 
		[&]() 
		{
			ConfigManager::Get().SetCurrentBird(ConfigManager::EBird::Blue);
		}
	);

	static RUID redButton = EntityManager::Get().Create<Button>("Resource/Button/Red.json", font32ID, EMouseButton::Left, 
		[&]() 
		{
			ConfigManager::Get().SetCurrentBird(ConfigManager::EBird::Red);
		}
	);

	static RUID yellowButton = EntityManager::Get().Create<Button>("Resource/Button/Yellow.json", font32ID, EMouseButton::Left, 
		[&]() 
		{
			ConfigManager::Get().SetCurrentBird(ConfigManager::EBird::Yellow);
		}
	);

	static RUID backButton = EntityManager::Get().Create<Button>("Resource/Button/Back.json", font32ID, EMouseButton::Left, 
		[&]() 
		{
			link_ = startScene_;
			bDetectSwitch_ = true;
		}
	);

	entities_ =
	{
		background,
		settingPanel,
		backgroundPanel,
		levelPanel,
		birdPanel,
		dayButton,
		nightButton,
		easyButton,
		normalButton,
		hardButton,
		blueButton,
		redButton,
		yellowButton,
		backButton,
	};

	bIsEnter_ = true;
}

void SettingScene::Exit()
{
	CHECK(bIsEnter_);

	bDetectSwitch_ = false;
	bIsEnter_ = false;
}