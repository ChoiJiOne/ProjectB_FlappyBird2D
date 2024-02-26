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
#include "RenderManager.h"
#include "ResourceManager.h"
#include "SDLManager.h"
#include "Shader.h"
#include "SpritePass2D.h"
#include "Texture2D.h"
#include "TTFont.h"

int32_t WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR pCmdLine, _In_ int32_t nCmdShow)
{
	CHECK(CrashModule::RegisterExceptionFilter());

	SDLManager::Get().Startup();
	AudioManager::Get().Startup();
	ResourceManager::Get().Startup();
	RenderManager::Get().Startup();
	EntityManager::Get().Startup();

	RenderManager::Get().SetDepthMode(false);
	RenderManager::Get().SetAlphaBlendMode(true);
	RenderManager::Get().SetMultisampleMode(true);

	RUID backgroundID = ResourceManager::Get().Create<Texture2D>("Resource/Texture/Background_Day.png");
	RUID fontID = ResourceManager::Get().Create<TTFont>("Resource/Font/Flappy_Font.ttf", 32, 127, 64.0f);

	std::vector<EUID> entites = {
		EntityManager::Get().Create<Background>(backgroundID)
	};
	
	GameTimer timer;
	timer.Reset();

	bool bIsDone = false;
	while (!bIsDone)
	{
		timer.Reset();

		SDL_Event e;
		while (SDL_PollEvent(&e))
		{
			switch (e.type)
			{
			case SDL_QUIT:
				bIsDone = true;
				break;
			}
		}

		EntityManager::Get().UpdateBatch(entites, timer.GetDeltaSeconds());
		RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);
		EntityManager::Get().RenderBatch(entites);
		RenderManager::Get().EndFrame();
	}

	EntityManager::Get().Shutdown();
	ResourceManager::Get().Shutdown();
	RenderManager::Get().Shutdown();
	AudioManager::Get().Shutdown();
	SDLManager::Get().Shutdown();

	CrashModule::UnregisterExceptionFilter();
	return 0;
}