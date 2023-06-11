-- FlappyBird2D 게임 개발 작업 영역입니다.
workspace "FlappyBird2D"
    -- 빌드 구성 요소입니다.
    configurations { 
        "Debug", 
        "Release", 
        "Shipping" 
    }

    -- 플랫폼을 설정합니다.
    platforms { "Win64" }

    -- 솔루션 경로를 설정합니다.
    location "%{wks.name}"

    -- 게임 소스 경로를 설정합니다.
    game="%{wks.location}/../Game"

    -- 오디오 모듈 경로를 설정합니다.
    audiomodule="%{wks.location}/../AudioModule"

    -- 데이터베이스 모듈 경로를 설정합니다.
    databasemodule="%{wks.location}/../DatabaseModule"

    -- 툴에서 공통적으로 사용할 유틸리티 소스 경로를 설정합니다.
    commonTool="%{wks.location}/../CommonTool"

    -- 폰트 아틀라스 생성 툴 소스 경로를 설정합니다.
    fontatlas="%{wks.location}/../FontAltasTool"

    -- 텍스처 아틀라스 생성 툴 소스 경로를 설정합니다.
    textureatlas="%{wks.location}/../TextureAtlasTool"

    -- 서드파티 라이브러리 경로를 설정합니다.
    thirdparty="%{wks.location}/../ThirdParty"

    -- 시작 프로젝트를 게임 프로젝트로 설정합니다.
    startproject "Game"
    
    -- 오디오 모듈 프로젝트입니다.
    project "AudioModule"
        -- 오디오 모듈 프로젝트의 종류를 설정합니다.
        kind "SharedLib"

        -- 프로그래밍 언어를 설정합니다.
        language "C++"

        -- C++의 표준을 설정합니다.
        cppdialect "C++17"

        -- 오디오 모듈 소스 코드의 include 경로를 추가합니다.
        includedirs { 
            "%{audiomodule}", 
            "%{thirdparty}/miniaudio",
        }

        -- 오디오 모듈 소스 코드의 file을 추가합니다.
        files { 
            "%{audiomodule}/*", 
            "%{thirdparty}/miniaudio/*",
        }

        -- Debug 빌드의 설정을 수행합니다.
        filter "configurations:Debug"
            defines { "DEBUG" }
            runtime  "Debug"
            optimize "Off"
            symbols "On"

        -- Release 빌드의 설정을 수행합니다.
        filter "configurations:Release"
            defines { "NDEBUG", "RELEASE" }
            runtime "Release"
            optimize "On"
            symbols "On"

        -- Shipping 빌드의 설정을 수행합니다.
        filter "configurations:Shipping"
            defines { "NDEBUG", "SHIPPING" }
            runtime "Release"
            optimize "Full"
            symbols "Off"
        

    -- 폰트 아틀라스 생성 툴 프로젝트입니다.
    project "FontAltasTool"
        -- 폰트 아틀라스 생성 툴 프로젝트의 종류를 설정합니다.
        kind "ConsoleApp"

        -- 프로그래밍 언어를 설정합니다.
        language "C++"

        -- C++의 표준을 설정합니다.
        cppdialect "C++17"

        -- 폰트 아틀라스 생성 툴 소스 코드의 include 경로를 추가합니다.
        includedirs { 
            "%{fontatlas}", 
            "%{thirdparty}/stb",
        }

        -- 폰트 아틀라스 생성 툴 소스 코드의 file을 추가합니다.
        files { 
            "%{fontatlas}/*",
            "%{thirdparty}/stb/*",
        }

        -- 명령행 인수를 설정합니다.
        debugargs {
            "FontPath=%{wks.location}..\\Content\\Font\\SeoulNamsanEB.ttf",
            "BeginCodePoint=32",
            "EndCodePoint=127",
            "FontSize=32",
            "OutputPath=%{wks.location}..\\Content\\",
        }

        -- 외부 라이브러리를 연결합니다.
        links { 
            "Dbghelp.lib",
            "shlwapi.lib",
        }

        -- Debug 빌드의 설정을 수행합니다.
        filter "configurations:Debug"
            defines { "DEBUG" }
            runtime  "Debug"
            optimize "Off"
            symbols "On"

        -- Release 빌드의 설정을 수행합니다.
        filter "configurations:Release"
            defines { "NDEBUG", "RELEASE" }
            runtime "Release"
            optimize "On"
            symbols "On"
        
        -- Shipping 빌드의 설정을 수행합니다.
        filter "configurations:Shipping"
            defines { "NDEBUG", "SHIPPING" }
            runtime "Release"
            optimize "Full"
            symbols "Off"

    
    -- 게임 프로젝트입니다.
    project "Game"
        -- 프로젝트의 종류를 설정합니다.
        kind "ConsoleApp"

        -- 프로젝트의 언어를 설정합니다.
        language "C#"

        -- C# 버전을 8.0으로 설정합니다.
        csversion("8.0")

        -- 포인터 사용을 위해 활성화합니다.
        clr "Unsafe"

        -- 경고 무시 옵션 추가
        disablewarnings { 
            "MSB3245", 
            "MSB3246", 
        }

        -- 소스 코드 경로를 추가합니다.
        includedirs { 
            "%{game}",
            "%{thirdparty}/SDL2",
        }

        -- 파일을 추가합니다.
        files { 
            "%{game}/*",
            "%{thirdparty}/SDL2/*",
        }

        -- 라이브러리 경로를 추가합니다.
        libdirs { "%{thirdparty}/Bin", }

        -- 외부 라이브러리를 추가합니다.
        links {
            "System",
            "AudioModule",
            "DatabaseModule",
            "System.Data.SQLite",
            "System.Data.SQLite.Linq",
            "SDL2.lib", 
            "SDL2main.lib",
            "SDL2_image.lib",
        }

        -- 명령행 인수를 설정합니다.
        debugargs {
            "Crash=%{wks.location}..\\Crash\\",
            "Content=%{wks.location}..\\Content\\",
        }

        -- Debug 빌드의 설정을 수행합니다.
        filter "configurations:Debug"
            defines { "DEBUG" }
            runtime  "Debug"
            optimize "Off"
            symbols "On"
        
        -- Release 빌드의 설정을 수행합니다.
        filter "configurations:Release"
            defines { "NDEBUG", "RELEASE" }
            runtime "Release"
            optimize "On"
            symbols "On"

        -- Shipping 빌드의 설정을 수행합니다.
        filter "configurations:Shipping"
            defines { "NDEBUG", "SHIPPING" }
            runtime "Release"
            optimize "Full"
            symbols "Off"