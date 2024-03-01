#include "Assertion.h"
#include "Land.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "Texture2D.h"

Land::Land(float scrollSpeed)
	: bCanMove_(true)
	, scrollSpeed_(scrollSpeed)
{
	int32_t w = 0;
	int32_t h = 0;
	RenderManager::Get().GetRenderTargetWindowSize(w, h);

	bound_ = BoundBox2D(Vec2f(w * 0.5f, 0.9f * h), w, h * 0.2f);

	maxScrollX_ = static_cast<float>(bound_.GetWidth());

	static RUID textureID = ResourceManager::Get().Create<Texture2D>("Resource/Texture/Land.png");
	textureID_ = textureID;

	bIsInitialized_ = true;
}

Land::~Land()
{
	if (bIsInitialized_)
	{
		Release();
	}
}

void Land::Tick(float deltaSeconds)
{
	if (bCanMove_)
	{
		scrollX_ += scrollSpeed_ * deltaSeconds;

		if (scrollX_ >= maxScrollX_)
		{
			scrollX_ -= maxScrollX_;
		}
	}
}

void Land::Render()
{
	if (bCanMove_)
	{
		float rate = scrollX_ / maxScrollX_;
		RenderManager::Get().RenderHorizonScrollSprite2D(textureID_, bound_.GetCenter(), bound_.GetWidth(), bound_.GetHeight(), 0.0f, rate);
	}
	else
	{
		RenderManager::Get().RenderSprite2D(textureID_, bound_.GetCenter(), bound_.GetWidth(), bound_.GetHeight(), 0.0f);
	}
}

void Land::Release()
{
	if (bIsInitialized_)
	{
		bIsInitialized_ = false;
	}
}