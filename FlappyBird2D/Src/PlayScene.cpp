#include "Assertion.h"
#include "Background.h"
#include "Button.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "Land.h"
#include "Panel.h"
#include "Pipe.h"
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

	int32_t w = 0;
	int32_t h = 0;
	float gap = 50.0f;
	RenderManager::Get().GetRenderTargetWindowSize(w, h);

	Pipe::SetStartLocation(Vec2f(static_cast<float>(w) + gap, static_cast<float>(h) * 0.4f));
	Pipe::SetEndLocation(Vec2f(-gap, static_cast<float>(h) * 0.4f));

	static EUID pipe = EntityManager::Get().Create<Pipe>(200.0f);
	
	entities_ = 
	{ 
		background, 
		pipe,
		land, 
	};

	bIsEnter_ = true;
}

void PlayScene::Exit()
{
	CHECK(bIsEnter_);

	bDetectSwitch_ = false;
	bIsEnter_ = false;
}