#include "Assertion.h"
#include "Button.h"
#include "RenderManager.h"
#include "ResourceManager.h"

Button::Button(const Layout& layout, const EMouseButton& mouseButton, const std::function<void()>& clickEvent)
	: layout_(layout)
	, mouseButton_(mouseButton)
	, clickEvent_(clickEvent)
{
	bIsInitialized_ = true;
	bIsActive_ = true;
}

Button::~Button()
{
	if (bIsInitialized_)
	{
		Release();
	}
}

void Button::Tick(float deltaSeconds)
{
	EPressState pressState = InputManager::Get().GetMousePressState(mouseButton_);
	state_ = GetMouseButtonState(pressState, state_);
	if (state_ == EState::Released)
	{
		clickEvent_();
	}
}

void Button::Render()
{
	Vec4f backgroundColor;
	switch (state_)
	{
	case EState::Disabled:
		backgroundColor = layout_.disableColor;
		break;

	case EState::Enabled:
		backgroundColor = layout_.enableColor;
		break;

	case EState::Pressed:
		backgroundColor = layout_.pressColor;
		break;

	case EState::Released:
		backgroundColor = layout_.releaseColor;
		break;
	}
	
	RenderManager::Get().RenderRoundRectangle2D(layout_.center, layout_.width, layout_.height, layout_.side, 0.0f, backgroundColor);
	RenderManager::Get().RenderWireframeRoundRectangle2D(layout_.center, layout_.width, layout_.height, layout_.side, 0.0f, layout_.outlineColor);
	if (!layout_.text.empty())
	{
		RenderManager::Get().RenderText2D(layout_.fontID, layout_.text, layout_.center, layout_.textColor);
	}
}

void Button::Release()
{
	if (bIsInitialized_)
	{
		bIsActive_ = false;
		bIsInitialized_ = false;
	}
}

Button::EState Button::GetMouseButtonState(const EPressState& pressState, const EState& state)
{
	if (DetectMouseCursorInButton())
	{
		if (state == EState::Disabled)
		{
			if (pressState == EPressState::Held || pressState == EPressState::Pressed)
			{
				return EState::Disabled;
			}
			else // pressState == EPressState::None || pressState == EPressState::Released
			{
				return EState::Enabled;
			}
		}
		else if (state == EState::Enabled)
		{
			if (pressState == EPressState::Held || pressState == EPressState::Pressed)
			{
				return EState::Pressed;
			}
			else
			{
				return EState::Enabled;
			}
		}
		else if (state == EState::Pressed)
		{
			if (pressState == EPressState::Released)
			{
				return EState::Released;
			}
			else if (pressState == EPressState::Held || pressState == EPressState::Pressed)
			{
				return EState::Pressed;
			}
			else // pressState == EPressState::None
			{
				return EState::Enabled;
			}
		}
		else // state == EState::Released
		{
			if (pressState == EPressState::None || pressState == EPressState::Released)
			{
				return EState::Enabled;
			}
			else // pressState == EPressState::Held || pressState == EPressState::Pressed
			{
				return EState::Pressed;
			}
		}
	}
	else
	{
		if (state == EState::Pressed && pressState == EPressState::Held)
		{
			return EState::Pressed;
		}
		else
		{
			return EState::Disabled;
		}
	}

	return EState::Disabled;
}

bool Button::DetectMouseCursorInButton()
{
	Vec2i cursorPos = InputManager::Get().GetCurrMousePosition();

	Vec2f minPosition = layout_.center - Vec2f(layout_.width, layout_.height) * 0.5f;
	Vec2f maxPosition = layout_.center + Vec2f(layout_.width, layout_.height) * 0.5f;
	Vec2f currentCursorPosition = Vec2f(static_cast<float>(cursorPos.x), static_cast<float>(cursorPos.y));

	return (minPosition.x <= currentCursorPosition.x && currentCursorPosition.x <= maxPosition.x)
		&& (minPosition.y <= currentCursorPosition.y && currentCursorPosition.y <= maxPosition.y);
}