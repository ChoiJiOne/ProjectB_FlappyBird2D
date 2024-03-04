#include "MathModule.h"

#include "Assertion.h"
#include "Bird.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "InputManager.h"
#include "Land.h"
#include "Pipe.h"
#include "PipeController.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "Texture2D.h"

Vec2f Bird::startLocation_;

Bird::Bird()
{
	width_ = 60.0f;
	height_ = 40.0f;
	maxSpeed_ = 500.0f;
	currentSpeed_ = 0.0f;
	dampingSpeed_ = 30.0f;

	rotate_ = 0.0f;
	minRotate_ = -MathModule::Pi / 6.0f; // 육십분법 각 = 30도
	maxRotate_ = MathModule::PiDiv2; // 육십분법 각 = 90도

	bound_ = BoundCircle2D(startLocation_, height_ * 0.5f);

	maxAnimationTime_ = 0.05f;

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
	switch (status_)
	{
	case EStatus::Ready:
		TickReadyStatus(deltaSeconds);
		break;

	case EStatus::Fly:
		TickFlyStatus(deltaSeconds);
		break;

	case EStatus::Dead:
		TickDeadStatus(deltaSeconds);
		break;
	}
}

void Bird::Render()
{
	RenderManager::Get().RenderSprite2D(textureIDs_[index_], bound_.GetCenter(), width_, height_, rotate_);
}

void Bird::Release()
{
	if (bIsInitialized_)
	{
		bIsInitialized_ = false;
	}
}

void Bird::TickReadyStatus(float deltaSeconds)
{
	animationTime_ += deltaSeconds;

	if (InputManager::Get().GetMousePressState(EMouseButton::Left) == EPressState::Pressed)
	{
		status_ = EStatus::Fly;
		currentSpeed_ = maxSpeed_;
		rotate_ = minRotate_;
	}

	if (animationTime_ >= maxAnimationTime_)
	{
		index_ = (index_ + 1) % textureIDs_.size();
		animationTime_ -= maxAnimationTime_;
	}
}

void Bird::TickFlyStatus(float deltaSeconds)
{
	animationTime_ += deltaSeconds;

	currentSpeed_ -= (4.0f * maxSpeed_ * deltaSeconds);

	if (currentSpeed_ <= 0.0f)
	{
		rotate_ += 2.0f * deltaSeconds;
		rotate_ = MathModule::Clamp<float>(rotate_, minRotate_, maxRotate_);
	}

	Vec2f center = bound_.GetCenter();
	center.y -= (currentSpeed_ * deltaSeconds);
	bound_.SetCenter(center);

	if (currentSpeed_ <= 0.0f && InputManager::Get().GetMousePressState(EMouseButton::Left) == EPressState::Pressed)
	{
		currentSpeed_ = maxSpeed_;
		rotate_ = minRotate_;
	}

	PipeController* pipeController = EntityManager::Get().GetEntity<PipeController>(pipeControllerID_);
	Land* land = EntityManager::Get().GetEntity<Land>(landID_);

	const std::vector<Pipe*>& pipes = pipeController->GetPipes();
	for (const Pipe* pipe : pipes)
	{
		if (pipe->GetStatus() == Pipe::EStatus::Active && (bound_.Intersect(pipe->GetTopBound()) || bound_.Intersect(pipe->GetBottomBound())))
		{
			status_ = EStatus::Dead;
			pipeController->SetStatus(PipeController::EStatus::Wait);
			land->SetCanMove(false);
		}
	}

	if (bound_.Intersect(land->GetBound()))
	{
		status_ = EStatus::Dead;
		pipeController->SetStatus(PipeController::EStatus::Wait);
		land->SetCanMove(false);
	}

	if (animationTime_ >= maxAnimationTime_)
	{
		index_ = (index_ + 1) % textureIDs_.size();
		animationTime_ -= maxAnimationTime_;
	}
}

void Bird::TickDeadStatus(float deltaSeconds)
{
}
