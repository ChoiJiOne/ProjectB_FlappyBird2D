#include "Assertion.h"
#include "Background.h"
#include "RenderManager.h"

Background::Background(const RUID& textureID)
	: textureID_(textureID)
{
	bIsInitialized_ = true;
	bIsActive_ = true;
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
}

void Background::Render()
{
	RenderManager::Get().RenderSprite2D(textureID_);
}

void Background::Release()
{
	if (bIsInitialized_)
	{
		bIsActive_ = false;
		bIsInitialized_ = false;
	}
}