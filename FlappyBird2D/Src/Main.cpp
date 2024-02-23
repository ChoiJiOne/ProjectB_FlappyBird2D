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
	RenderManager::Get().Startup();
	ResourceManager::Get().Startup();

	RenderManager::Get().SetDepthMode(false);
	RenderManager::Get().SetAlphaBlendMode(true);
	RenderManager::Get().SetMultisampleMode(true);

	TTFont* font = ResourceManager::Get().CreateResource<TTFont>("Font");
	font->Initialize("Resource/Font/Flappy_Font.ttf", 32, 127, 64.0f);

	Texture2D* background = ResourceManager::Get().CreateResource<Texture2D>("Background");
	background->Initialize("Resource/Texture/Background_Day.png");

	Texture2D* land = ResourceManager::Get().CreateResource<Texture2D>("Land");
	land->Initialize("Resource/Texture/Land.png");

	GeometryPass2D* geometryPass = ResourceManager::Get().CreateResource<GeometryPass2D>("GeometryPass2D");
	geometryPass->Initialize();

	SpritePass2D* spritePass = ResourceManager::Get().CreateResource<SpritePass2D>("SpritePass2D");
	spritePass->Initialize();

	GlyphPass2D* glyphPass = ResourceManager::Get().CreateResource<GlyphPass2D>("GlyphPass2D");
	glyphPass->Initialize();

	Mat4x4f ortho = MathModule::CreateOrtho(0.0f, 600.0f, 800.0f, 0.0f, -1.0f, 1.0f);

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

		spritePass->DrawSprite2D(background);
		spritePass->DrawSprite2D(ortho, land, Vec2f(300.0f, 750.0f), 600.0f, 100.0f, 0.0f);

		for (float x = 0.0f; x <= 600; x += 10.0f)
		{
			geometryPass->DrawLine2D(ortho, Vec2f(x, 0.0f), Vec2f(x, 800.0f), Vec4f(0.0f, 0.0f, 0.0f, 1.0f));
		}

		for (float y = 0.0f; y <= 800; y += 10.0f)
		{
			geometryPass->DrawLine2D(ortho, Vec2f(0.0f, y), Vec2f(600.0f, y), Vec4f(0.0f, 0.0f, 0.0f, 1.0f));
		}

		glyphPass->DrawText2D(ortho, font, L"01. This is Flappy Bird", Vec2f(200.0f, 100.0f), GlyphPass2D::EAlignment::LEFT, Vec4f(1.0f, 0.0f, 1.0f, 1.0f));
		glyphPass->DrawText2D(ortho, font, L"02. This is Flappy Bird", Vec2f(200.0f, 200.0f), GlyphPass2D::EAlignment::LEFT, Vec4f(1.0f, 0.0f, 1.0f, 1.0f));
		glyphPass->DrawText2D(ortho, font, L"03. asdokokowef",         Vec2f(200.0f, 300.0f), GlyphPass2D::EAlignment::LEFT, Vec4f(1.0f, 0.0f, 1.0f, 1.0f));
		glyphPass->DrawText2D(ortho, font, L"04. ?????????????",       Vec2f(200.0f, 400.0f), GlyphPass2D::EAlignment::LEFT, Vec4f(1.0f, 0.0f, 1.0f, 1.0f));
		glyphPass->DrawText2D(ortho, font, L"05. !!!!!!!!!",           Vec2f(200.0f, 500.0f), GlyphPass2D::EAlignment::LEFT, Vec4f(1.0f, 0.0f, 1.0f, 1.0f));
		
		RenderManager::Get().EndFrame();
	}

	ResourceManager::Get().Shutdown();
	RenderManager::Get().Shutdown();
	SDLManager::Get().Shutdown();

	CrashModule::UnregisterExceptionFilter();
	return 0;
}