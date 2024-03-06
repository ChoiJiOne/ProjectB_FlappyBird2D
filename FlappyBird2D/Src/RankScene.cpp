#include "Assertion.h"
#include "AudioManager.h"
#include "Background.h"
#include "Button.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "Land.h"
#include "Panel.h"
#include "Pipe.h"
#include "PipeController.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "RankViewer.h"
#include "RankScene.h"
#include "Sound.h"
#include "TTFont.h"

void RankScene::Tick(float deltaSeconds)
{
	EntityManager::Get().UpdateBatch(entityIDs_, deltaSeconds);

	RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);
	EntityManager::Get().RenderBatch(entityIDs_);
	RenderManager::Get().EndFrame();
}

void RankScene::Enter()
{
	CHECK(!bIsEnter_);

	static RUID clickSound = ResourceManager::Get().Create<Sound>("Resource/Sound/swoosh.wav");

	static auto resetEvent = [&]()
	{
		Sound* sound = ResourceManager::Get().GetResource<Sound>(clickSound);
		sound->Reset();
		sound->SetLooping(false);
		sound->Play();

		link_ = startScene_;
		bDetectSwitch_ = true;
	};

	static RUID font32ID = ResourceManager::Get().Create<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 32.0f);

	static EUID resetButton = EntityManager::Get().Create<Button>("Resource/Button/Reset.json", font32ID, EMouseButton::Left, resetEvent);
	static EUID quitButton = EntityManager::Get().Create<Button>("Resource/Button/Quit.json", font32ID, EMouseButton::Left, quitLoopEvent_);
	static EUID rankViewer = EntityManager::Get().Create<RankViewer>(font32ID);

	std::vector<Pipe*> pipes = EntityManager::Get().GetEntity<PipeController>(pipeController_)->GetPipes();

	entityIDs_ = 
	{
		backgroundID_,
		pipes[0]->GetID(),
		pipes[1]->GetID(),
		pipes[2]->GetID(),
		pipes[3]->GetID(),
		pipeController_,
		landID_,
		rankViewer,
		resetButton,
		quitButton,
	};

	bIsEnter_ = true;
}

void RankScene::Exit()
{
	CHECK(bIsEnter_);

	std::vector<Pipe*> pipes = EntityManager::Get().GetEntity<PipeController>(pipeController_)->GetPipes();
	for (Pipe* pipe : pipes)
	{
		EntityManager::Get().Destroy(pipe->GetID());
	}

	EntityManager::Get().Destroy(backgroundID_);
	EntityManager::Get().Destroy(pipeController_);
	EntityManager::Get().Destroy(landID_);
	EntityManager::Get().Destroy(backgroundID_);

	bDetectSwitch_ = false;
	bIsEnter_ = false;
}