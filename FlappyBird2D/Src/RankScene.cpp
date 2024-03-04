#include "Assertion.h"
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
#include "RankScene.h"
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
	};

	bIsEnter_ = true;
}

void RankScene::Exit()
{
	CHECK(bIsEnter_);

	for (const auto& entityID : entityIDs_)
	{
		EntityManager::Get().Destroy(entityID);
	}

	bDetectSwitch_ = false;
	bIsEnter_ = false;
}