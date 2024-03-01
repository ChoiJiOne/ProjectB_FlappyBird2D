#include "Assertion.h"
#include "Background.h"
#include "ConfigManager.h"
#include "RenderManager.h"

Background::Background()
{
	bCanMove_ = false;
	bIsInitialized_ = true;
}

Background::Background(float scrollSpeed)
	: bCanMove_(true)
	, scrollSpeed_(scrollSpeed)
{
	int32_t w = 0;
	int32_t h = 0;
	RenderManager::Get().GetRenderTargetWindowSize(w, h);

	maxScrollX_ = static_cast<float>(w);

	bIsInitialized_ = true;
}

Background::~Background()
{
	if (bIsInitialized_)
	{
		Release();
	}
}

void Background::Tick(float deltaSeconds)
{
	textureID_ = ConfigManager::Get().GetCurrentBackgroundID();

	if (bCanMove_)
	{
		scrollX_ += scrollSpeed_ * deltaSeconds;

		if (scrollX_ >= maxScrollX_)
		{
			scrollX_ -= maxScrollX_;
		}
	}
}

void Background::Render()
{
	if (bCanMove_)
	{
		float rate = scrollX_ / maxScrollX_;
		RenderManager::Get().RenderHorizonScrollSprite2D(textureID_, rate);
	}
	else
	{
		RenderManager::Get().RenderSprite2D(textureID_);
	}
}

void Background::Release()
{
	if (bIsInitialized_)
	{
		bIsInitialized_ = false;
	}
}