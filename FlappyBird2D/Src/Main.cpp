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
#include "Button.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "GameTimer.h"
#include "GeometryPass2D.h"
#include "GlyphPass2D.h"
#include "InputManager.h"
#include "Panel.h"
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
	ConfigManager::Get().Startup();

	RenderManager::Get().SetDepthMode(false);
	RenderManager::Get().SetAlphaBlendMode(true);
	RenderManager::Get().SetMultisampleMode(true);

	InputManager::Get().AddWindowEventAction(EWindowEvent::CLOSE, [&]() {bIsDone = true; }, true);

	RUID fontID = ResourceManager::Get().Create<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 32.0f);
	
	std::vector<EUID> entites = {
		EntityManager::Get().Create<Background>(),
		//EntityManager::Get().Create<Button>("Resource/Button/Start.json", fontID, EMouseButton::Left, [&]() { SDL_Log("CLICK START!"); }),
		//EntityManager::Get().Create<Button>("Resource/Button/Setting.json", fontID, EMouseButton::Left, [&]() { SDL_Log("CLICK SETTING!"); }),
		//EntityManager::Get().Create<Button>("Resource/Button/Quit.json", fontID, EMouseButton::Left, [&]() { SDL_Log("CLICK QUIT!"); }),
		EntityManager::Get().Create<Panel>("Resource/Panel/Background.json", fontID),
		EntityManager::Get().Create<Panel>("Resource/Panel/Level.json", fontID),
		EntityManager::Get().Create<Button>("Resource/Button/Day.json", fontID, EMouseButton::Left, [&]() { 
			ConfigManager::Get().SetCurrentBackgroundID(ConfigManager::EBackground::Day); 
		}),
		EntityManager::Get().Create<Button>("Resource/Button/Night.json", fontID, EMouseButton::Left, [&]() { 
			ConfigManager::Get().SetCurrentBackgroundID(ConfigManager::EBackground::Night);
		}),
		EntityManager::Get().Create<Button>("Resource/Button/Easy.json", fontID, EMouseButton::Left, [&]() {
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Easy);
		}),
		EntityManager::Get().Create<Button>("Resource/Button/Normal.json", fontID, EMouseButton::Left, [&]() {
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Normal);
		}),
		EntityManager::Get().Create<Button>("Resource/Button/Hard.json", fontID, EMouseButton::Left, [&]() {
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Hard);
		}),
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

		RenderManager::Get().EndFrame();
	}

	ConfigManager::Get().Shutdown();
	EntityManager::Get().Shutdown();
	ResourceManager::Get().Shutdown();
	RenderManager::Get().Shutdown();
	AudioManager::Get().Shutdown();
	InputManager::Get().Shutdown();
	SDLManager::Get().Shutdown();

	CrashModule::UnregisterExceptionFilter();
	return 0;
}