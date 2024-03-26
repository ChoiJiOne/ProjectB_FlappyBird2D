#include "Assertion.h"
#include "Background.h"
#include "Button.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "Panel.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "StatusViewer.h"
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

	static RUID font32ID = ResourceManager::Get().CreateID<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 32.0f);
	static RUID font64ID = ResourceManager::Get().CreateID<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 64.0f);

	static EUID background = EntityManager::Get().Create<Background>();

	static EUID settingPanel = EntityManager::Get().Create<Panel>("Resource/Panel/Setting.json", font64ID);
	static EUID backgroundPanel = EntityManager::Get().Create<Panel>("Resource/Panel/Background.json", font32ID);
	static EUID levelPanel = EntityManager::Get().Create<Panel>("Resource/Panel/Level.json", font32ID);
	static EUID birdPanel = EntityManager::Get().Create<Panel>("Resource/Panel/Bird.json", font32ID);

	static EUID dayButton = EntityManager::Get().Create<Button>("Resource/Button/Day.json", font32ID, EMouseButton::Left, 
		[&]() 
		{
			ConfigManager::Get().SetCurrentBackgroundID(ConfigManager::EBackground::Day);
		}
	);

	static EUID nightButton = EntityManager::Get().Create<Button>("Resource/Button/Night.json", font32ID, EMouseButton::Left,
		[&]()
		{
			ConfigManager::Get().SetCurrentBackgroundID(ConfigManager::EBackground::Night);
		}
	);

	static EUID easyButton = EntityManager::Get().Create<Button>("Resource/Button/Easy.json", font32ID, EMouseButton::Left,
		[&]()
		{
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Easy);
		}
	);

	static EUID normalButton = EntityManager::Get().Create<Button>("Resource/Button/Normal.json", font32ID, EMouseButton::Left,
		[&]()
		{
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Normal);
		}
	);

	static EUID hardButton = EntityManager::Get().Create<Button>("Resource/Button/Hard.json", font32ID, EMouseButton::Left,
		[&]()
		{
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Hard);
		}
	);

	static EUID blueButton = EntityManager::Get().Create<Button>("Resource/Button/Blue.json", font32ID, EMouseButton::Left, 
		[&]() 
		{
			ConfigManager::Get().SetCurrentBird(ConfigManager::EBird::Blue);
		}
	);

	static EUID redButton = EntityManager::Get().Create<Button>("Resource/Button/Red.json", font32ID, EMouseButton::Left, 
		[&]() 
		{
			ConfigManager::Get().SetCurrentBird(ConfigManager::EBird::Red);
		}
	);

	static EUID yellowButton = EntityManager::Get().Create<Button>("Resource/Button/Yellow.json", font32ID, EMouseButton::Left, 
		[&]() 
		{
			ConfigManager::Get().SetCurrentBird(ConfigManager::EBird::Yellow);
		}
	);

	static EUID backButton = EntityManager::Get().Create<Button>("Resource/Button/Back.json", font32ID, EMouseButton::Left, 
		[&]() 
		{
			link_ = startScene_;
			bDetectSwitch_ = true;
		}
	);

	static EUID statusViewer = EntityManager::Get().Create<StatusViewer>(font32ID);

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
		statusViewer,
	};

	bIsEnter_ = true;
}

void SettingScene::Exit()
{
	CHECK(bIsEnter_);

	bDetectSwitch_ = false;
	bIsEnter_ = false;
}