#include "Assertion.h"
#include "Background.h"
#include "ConfigManager.h"
#include "RenderManager.h"

Background::Background()
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
	textureID_ = ConfigManager::Get().GetCurrentBackgroundID();
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