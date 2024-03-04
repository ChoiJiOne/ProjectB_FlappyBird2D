#include "Assertion.h"
#include "Background.h"
#include "Bird.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "Land.h"
#include "Pipe.h"
#include "PipeController.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "PlayScene.h"
#include "ScoreViewer.h"

void PlayScene::Tick(float deltaSeconds)
{
	EntityManager::Get().UpdateBatch(entityIDs_, deltaSeconds);

	RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);
	EntityManager::Get().RenderBatch(entityIDs_);
	RenderManager::Get().EndFrame();

	if (countDown_ >= 0.0f)
	{
		countDown_ -= deltaSeconds;
		
		if (countDown_ <= 0.0f)
		{
			PipeController* pipeController = EntityManager::Get().GetEntity<PipeController>(pipeController_);
			pipeController->SetStatus(PipeController::EStatus::Active);
		}
	}

	Bird* bird = EntityManager::Get().GetEntity<Bird>(birdID_);
	if (bird->GetStatus() == Bird::EStatus::Done)
	{
		// TODO
	}
}

void PlayScene::Enter()
{
	CHECK(!bIsEnter_);

	countDown_ = 3.0f;
	
	int32_t w = 0;
	int32_t h = 0;
	float gap = 50.0f;
	RenderManager::Get().GetRenderTargetWindowSize(w, h);

	Pipe::SetStartLocation(Vec2f(static_cast<float>(w) + gap, static_cast<float>(h) * 0.4f));
	Pipe::SetEndLocation(Vec2f(-gap, static_cast<float>(h) * 0.4f));

	std::vector<Pipe*> pipes =
	{
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed_)),
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed_)),
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed_)),
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed_)),
	};

	pipeController_ = EntityManager::Get().Create<PipeController>(pipes, 300.0f);
	scoreViewerID_ = EntityManager::Get().Create<ScoreViewer>();

	entityIDs_ =
	{
		backgroundID_,
		pipes[0]->GetID(),
		pipes[1]->GetID(),
		pipes[2]->GetID(),
		pipes[3]->GetID(),
		pipeController_,
		landID_,
		birdID_,
		scoreViewerID_,
	};

	Bird* bird = EntityManager::Get().GetEntity<Bird>(birdID_);
	bird->SetPipeControllerID(pipeController_);
	bird->SetLandID(landID_);
	bird->SetBackgroundID(backgroundID_);

	ScoreViewer* scoreViewer = EntityManager::Get().GetEntity<ScoreViewer>(scoreViewerID_);
	scoreViewer->SetBirdID(birdID_);
	
	bIsEnter_ = true;
}

void PlayScene::Exit()
{
	CHECK(bIsEnter_);

	EntityManager::Get().Destroy(birdID_);
	EntityManager::Get().Destroy(scoreViewerID_);

	bDetectSwitch_ = false;
	bIsEnter_ = false;
}