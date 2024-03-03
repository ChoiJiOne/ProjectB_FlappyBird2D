#include "Assertion.h"
#include "Background.h"
#include "Bird.h"
#include "Button.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "Land.h"
#include "Panel.h"
#include "Pipe.h"
#include "PipeController.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "PlayScene.h"
#include "TTFont.h"

void PlayScene::Tick(float deltaSeconds)
{
	EntityManager::Get().UpdateBatch(entityIDs_, deltaSeconds);

	RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);

	EntityManager::Get().RenderBatch(entityIDs_);

	RenderManager::Get().EndFrame();

	if (bird_->GetStatus() == Bird::EStatus::Fly)
	{
		
	}
}

void PlayScene::Enter()
{
	CHECK(!bIsEnter_);

	float gameSpeed = 200.0f;

	int32_t w = 0;
	int32_t h = 0;
	float gap = 50.0f;
	RenderManager::Get().GetRenderTargetWindowSize(w, h);

	Pipe::SetStartLocation(Vec2f(static_cast<float>(w) + gap, static_cast<float>(h) * 0.4f));
	Pipe::SetEndLocation(Vec2f(-gap, static_cast<float>(h) * 0.4f));

	Bird::SetStartLocation(Vec2f(static_cast<float>(w) * 0.3f, static_cast<float>(h) * 0.4f));

	EUID background = EntityManager::Get().Create<Background>(50.0f);
	EUID land = EntityManager::Get().Create<Land>(gameSpeed);

	bird_ = EntityManager::Get().GetEntity<Bird>(EntityManager::Get().Create<Bird>());

	std::vector<Pipe*> pipes =
	{
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed)),
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed)),
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed)),
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed)),
	};
		
	entityIDs_ = 
	{ 
		background, 
		pipes[0]->GetID(),
		pipes[1]->GetID(),
		pipes[2]->GetID(),
		pipes[3]->GetID(),
		EntityManager::Get().Create<PipeController>(pipes, 300.0f),
		land, 
		bird_->GetID(),
	};

	bIsEnter_ = true;
}

void PlayScene::Exit()
{
	CHECK(bIsEnter_);

	for (const auto& id : entityIDs_)
	{
		EntityManager::Get().Destroy(id);
	}

	bDetectSwitch_ = false;
	bIsEnter_ = false;
}