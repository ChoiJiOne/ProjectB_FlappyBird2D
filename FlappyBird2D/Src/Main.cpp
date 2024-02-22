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
#include "FrameBuffer.h"
#include "GeometryGenerator.h"
#include "PostEffectPass.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "SDLManager.h"
#include "Shader.h"
#include "ShadowMap.h"
#include "Skybox.h"
#include "SkyboxPass.h"
#include "StaticMesh.h"
#include "Texture2D.h"
#include "Vertex.h"

int32_t WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR pCmdLine, _In_ int32_t nCmdShow)
{
	CHECK(CrashModule::RegisterExceptionFilter());

	SDLManager::Get().Startup();
	RenderManager::Get().Startup();
	ResourceManager::Get().Startup();

	RenderManager::Get().SetDepthMode(true);
	RenderManager::Get().SetAlphaBlendMode(true);
	RenderManager::Get().SetMultisampleMode(true);

	SDL_Event e;
	bool bIsDone = false;
	while (!bIsDone)
	{
		while (SDL_PollEvent(&e))
		{
			switch (e.type)
			{
			case SDL_QUIT:
				bIsDone = true;
				break;
			}
		}

		RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);
		RenderManager::Get().EndFrame();
	}

	ResourceManager::Get().Shutdown();
	RenderManager::Get().Shutdown();
	SDLManager::Get().Shutdown();

	CrashModule::UnregisterExceptionFilter();
	return 0;
}