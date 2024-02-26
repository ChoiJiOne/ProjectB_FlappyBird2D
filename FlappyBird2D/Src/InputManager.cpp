#include <SDL.h>

#include "Assertion.h"
#include "InputManager.h"

InputManager& InputManager::Get()
{
	static InputManager instance;
	return instance;
}

void InputManager::Startup()
{
	CHECK(!bIsStartup_);

	bIsStartup_ = true;
}

void InputManager::Shutdown()
{
	CHECK(bIsStartup_);

	bIsStartup_ = false;
}