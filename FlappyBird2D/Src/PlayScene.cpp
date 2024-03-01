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
	EntityManager::Get().UpdateBatch(entityIDs_, deltaSeconds);

	RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);

	EntityManager::Get().RenderBatch(entityIDs_);

	RenderManager::Get().EndFrame();

	for (auto& pipe : pipes_)
	{
		if (pipe->GetStatus() == Pipe::EStatus::Active)
		{
			continue;
		}

		bool bActive = true;
		for (auto& other : pipes_)
		{
			if (other->GetStatus() == Pipe::EStatus::Active && other->GetGapPipe(pipe) <= pipeGap_)
			{
				bActive = false;
			}
		}

		if (bActive)
		{
			pipe->SetStatus(Pipe::EStatus::Active);
		}
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

	EUID background = EntityManager::Get().Create<Background>(50.0f);
	EUID land = EntityManager::Get().Create<Land>(gameSpeed);

	pipes_ =
	{
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed)),
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed)),
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed)),
		EntityManager::Get().GetEntity<Pipe>(EntityManager::Get().Create<Pipe>(gameSpeed)),
	};
		
	entityIDs_ = 
	{ 
		background, 
		pipes_[0]->GetID(),
		pipes_[1]->GetID(),
		pipes_[2]->GetID(),
		pipes_[3]->GetID(),
		land, 
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