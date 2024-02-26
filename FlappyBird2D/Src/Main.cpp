#include <array>
#include <cstdint>
#include <windows.h>
#include <vector>

#include <glad/glad.h>
#include <SDL.h>

#include "CrashModule.h"
#include "FileModule.h"
#include "MathModule.h"

#include "Assertion.h"
#include "AudioManager.h"
#include "Background.h"
#include "BoundBox2D.h"
#include "BoundCircle2D.h"
#include "EntityManager.h"
#include "GameTimer.h"
#include "GeometryPass2D.h"
#include "GlyphPass2D.h"
#include "InputManager.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "SDLManager.h"
#include "Shader.h"
#include "SpritePass2D.h"
#include "StringUtils.h"
#include "Texture2D.h"
#include "TTFont.h"

bool bIsDone = false;

int32_t WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR pCmdLine, _In_ int32_t nCmdShow)
{
	CHECK(CrashModule::RegisterExceptionFilter());

	SDLManager::Get().Startup();
	InputManager::Get().Startup();
	AudioManager::Get().Startup();
	RenderManager::Get().Startup();
	ResourceManager::Get().Startup();
	EntityManager::Get().Startup();

	RenderManager::Get().SetDepthMode(false);
	RenderManager::Get().SetAlphaBlendMode(true);
	RenderManager::Get().SetMultisampleMode(true);

	InputManager::Get().AddWindowEventAction(EWindowEvent::CLOSE, [&]() {bIsDone = true; }, true);

	RUID backgroundID = ResourceManager::Get().Create<Texture2D>("Resource/Texture/Background_Day.png");
	RUID fontID = ResourceManager::Get().Create<TTFont>("Resource/Font/SeoulNamsanEB.ttf", 0x00, 0xD7A3, 12.0f);
	
	std::vector<EUID> entites = {
		EntityManager::Get().Create<Background>(backgroundID)
	};

	GameTimer timer;
	timer.Reset();

	while (!bIsDone)
	{
		timer.Tick();
		InputManager::Get().Tick();

		EntityManager::Get().UpdateBatch(entites, timer.GetDeltaSeconds());
		
		RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);

		EntityManager::Get().RenderBatch(entites);

		//if (InputManager::Get().GetMousePressState(EMouseButton::Right) == EPressState::Held)
		//{
		//	RenderManager::Get().RenderText2D(fontID, L"PRESS MOUSE", Vec2f(0.0f, 0.0f), Vec4f(1.0f, 0.0f, 0.0f, 1.0f), false);
		//}

		const Vec2i& mousePos = InputManager::Get().GetCurrMousePosition();
		RenderManager::Get().RenderText2D(fontID, StringUtils::PrintF(L"(%d, %d)", mousePos.x, mousePos.y), Vec2f(0.0f, 0.0f), Vec4f(1.0f, 0.0f, 0.0f, 1.0f), false);

		RenderManager::Get().EndFrame();
	}

	EntityManager::Get().Shutdown();
	ResourceManager::Get().Shutdown();
	RenderManager::Get().Shutdown();
	AudioManager::Get().Shutdown();
	InputManager::Get().Shutdown();
	SDLManager::Get().Shutdown();

	CrashModule::UnregisterExceptionFilter();
	return 0;
}