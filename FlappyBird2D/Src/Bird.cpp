#include "Assertion.h"
#include "Bird.h"
#include "ConfigManager.h"
#include "InputManager.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "Texture2D.h"

Vec2f Bird::startLocation_;

Bird::Bird()
{
	width_ = 60.0f;
	height_ = 40.0f;
	speed_ = 150.0f;

	bound_ = BoundCircle2D(startLocation_, height_ * 0.5f);

	maxAnimationTime_ = 0.1f;

	static RUID blueDown = ResourceManager::Get().Create<Texture2D>("Resource/Texture/BlueBird_Down.png");
	static RUID blueMid = ResourceManager::Get().Create<Texture2D>("Resource/Texture/BlueBird_Mid.png");
	static RUID blueUp = ResourceManager::Get().Create<Texture2D>("Resource/Texture/BlueBird_Up.png");

	static RUID redDown = ResourceManager::Get().Create<Texture2D>("Resource/Texture/RedBird_Down.png");
	static RUID redMid = ResourceManager::Get().Create<Texture2D>("Resource/Texture/RedBird_Mid.png");
	static RUID redUp = ResourceManager::Get().Create<Texture2D>("Resource/Texture/RedBird_Up.png");

	static RUID yellowDown = ResourceManager::Get().Create<Texture2D>("Resource/Texture/YellowBird_Down.png");
	static RUID yellowMid = ResourceManager::Get().Create<Texture2D>("Resource/Texture/YellowBird_Mid.png");
	static RUID yellowUp = ResourceManager::Get().Create<Texture2D>("Resource/Texture/YellowBird_Up.png");
	
	ConfigManager::EBird bird = ConfigManager::Get().GetCurrentBird();
	switch (bird)
	{
	case ConfigManager::EBird::Blue:
		textureIDs_ =
		{
			blueDown,
			blueMid,
			blueUp,
			blueMid,
		};
		break;

	case ConfigManager::EBird::Red:
		textureIDs_ =
		{
			redDown,
			redMid,
			redUp,
			redMid,
		};
		break;

	case ConfigManager::EBird::Yellow:
		textureIDs_ =
		{
			yellowDown,
			yellowMid,
			yellowUp,
			yellowMid,
		};
		break;
	}

	static RUID infoID = ResourceManager::Get().Create<Texture2D>("Resource/Texture/GameInfo.png");
	infoID_ = infoID;
	infoLocation_ = Vec2f(300.0f, 400.0f);
	infoWidth_ = 150.0f;
	infoHeight_ = 140.0f;

	static RUID getReadyID = ResourceManager::Get().Create<Texture2D>("Resource/Texture/GetReady.png");
	getReadyID_ = getReadyID;
	getReadyLocation_ = Vec2f(300.0f, 200.0f);
	getReadyWidth_ = 350.0f;
	getReadyHeight_ = 100.0f;

	bIsInitialized_ = true;
}

Bird::~Bird()
{
	if (bIsInitialized_)
	{
		Release();
	}
}

void Bird::Tick(float deltaSeconds)
{
	animationTime_ += deltaSeconds;

	if (animationTime_ >= maxAnimationTime_)
	{
		index_ = (index_ + 1) % textureIDs_.size();
		animationTime_ -= maxAnimationTime_;
	}

	switch (status_)
	{
	case EStatus::Ready:
		if (InputManager::Get().GetMousePressState(EMouseButton::Left) == EPressState::Pressed)
		{
			status_ = EStatus::Fly;
		}
		break;

	case EStatus::Fly:
		break;

	case EStatus::Dead:
		break;
	}
}

void Bird::Render()
{
	if (status_ == EStatus::Ready)
	{
		RenderManager::Get().RenderSprite2D(getReadyID_, getReadyLocation_, getReadyWidth_, getReadyHeight_, 0.0f);
		RenderManager::Get().RenderSprite2D(infoID_, infoLocation_, infoWidth_, infoHeight_, 0.0f);
	}
	
	RenderManager::Get().RenderSprite2D(textureIDs_[index_], bound_.GetCenter(), width_, height_, rotate_);
}

void Bird::Release()
{
	if (bIsInitialized_)
	{
		bIsInitialized_ = false;
	}
}