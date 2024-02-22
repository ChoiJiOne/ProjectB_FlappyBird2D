#include "CrashModule.h"

#include <cstdint>
#include <windows.h>
#include <dbghelp.h>
#include <pathcch.h>
#include <Shlwapi.h>
#include <string>

const uint32_t MAX_BUFFER_SIZE = 1024;
char crashErrorMessageBuffer[MAX_BUFFER_SIZE];
wchar_t crashSavePath[MAX_BUFFER_SIZE];
LPTOP_LEVEL_EXCEPTION_FILTER topLevelExceptionFilter;

std::wstring PrintFormat(const wchar_t* format, ...)
{
	static wchar_t buffer[MAX_BUFFER_SIZE];

	va_list args;
	va_start(args, format);
	int32_t size = _vsnwprintf_s(buffer, MAX_BUFFER_SIZE, format, args);
	va_end(args);

	return std::wstring(buffer, size);
}

std::wstring GetCurrentSystemTimeAsString()
{
	SYSTEMTIME systemTime;
	GetLocalTime(&systemTime);

	return PrintFormat(
		L"%4d-%02d-%02d-%02d-%02d-%02d",
		static_cast<int32_t>(systemTime.wYear),
		static_cast<int32_t>(systemTime.wMonth),
		static_cast<int32_t>(systemTime.wDay),
		static_cast<int32_t>(systemTime.wHour),
		static_cast<int32_t>(systemTime.wMinute),
		static_cast<int32_t>(systemTime.wSecond)
	);
}

void WriteErrorMessage(const std::wstring& path)
{
	HANDLE fileHandle = CreateFileW(path.c_str(), GENERIC_WRITE, 0, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
	if (fileHandle == INVALID_HANDLE_VALUE)
	{
		return;
	}

	DWORD writeByteSize = 0;
	WriteFile(fileHandle, &crashErrorMessageBuffer[0], static_cast<DWORD>(MAX_BUFFER_SIZE), &writeByteSize, nullptr);
	CloseHandle(fileHandle);
}

bool CreateMinidumpFile(const std::wstring& path, EXCEPTION_POINTERS* exceptionPtr)
{
	HANDLE fileHandle = CreateFileW(path.c_str(), GENERIC_WRITE, FILE_SHARE_WRITE, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
	if (fileHandle == INVALID_HANDLE_VALUE)
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			crashErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(crashErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to create minidump file", MAX_BUFFER_SIZE);
		}
		else
		{
			strncat_s(crashErrorMessageBuffer, "\nfailed to create minidump file", MAX_BUFFER_SIZE);
		}
		
		return false;
	}

	_MINIDUMP_EXCEPTION_INFORMATION exception;
	exception.ThreadId = GetCurrentThreadId();
	exception.ExceptionPointers = exceptionPtr;
	exception.ClientPointers = FALSE;

	if (!MiniDumpWriteDump(GetCurrentProcess(), GetCurrentProcessId(), fileHandle, MiniDumpWithFullMemory, &exception, nullptr, nullptr))
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			crashErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(crashErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to write minidump file", MAX_BUFFER_SIZE);
		}
		else
		{
			strncat_s(crashErrorMessageBuffer, "\nfailed to write minidump file", MAX_BUFFER_SIZE);
		}

		return false;
	}
	
	if (!CloseHandle(fileHandle))
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			crashErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(crashErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to close minidump file", MAX_BUFFER_SIZE);
		}
		else
		{
			strncat_s(crashErrorMessageBuffer, "\nfailed to close minidump file", MAX_BUFFER_SIZE);
		}

		return false;
	}

	return true;
}

LONG WINAPI DetectApplicationCrash(EXCEPTION_POINTERS* exceptionPtr)
{
	std::wstring systemTime = GetCurrentSystemTimeAsString();
	std::wstring minidumpPath = PrintFormat(L"%sWindows-%s-Minidump.dmp", crashSavePath, systemTime.c_str());

	if (!CreateMinidumpFile(minidumpPath, exceptionPtr))
	{
		std::wstring writeErrorPath = PrintFormat(L"%sWindows-%s-WriteError.txt", crashSavePath, systemTime.c_str());
		WriteErrorMessage(writeErrorPath);
	}

	return EXCEPTION_EXECUTE_HANDLER;
}

const char* CrashModule::GetErrorMessage()
{
	return &crashErrorMessageBuffer[0];
}

bool CrashModule::RegisterExceptionFilter()
{
	topLevelExceptionFilter = SetUnhandledExceptionFilter(DetectApplicationCrash);

	std::wstring systemTime = GetCurrentSystemTimeAsString();

	static wchar_t exePath[MAX_BUFFER_SIZE];
	if (!GetModuleFileNameW(nullptr, exePath, MAX_BUFFER_SIZE))
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			crashErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(crashErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to get execute file name", MAX_BUFFER_SIZE);
		}
		else
		{
			strncat_s(crashErrorMessageBuffer, "\nfailed to get execute file name", MAX_BUFFER_SIZE);
		}

		std::wstring writeErrorPath = PrintFormat(L"%sWindows-%s-WriteError.txt", crashSavePath, systemTime.c_str());
		WriteErrorMessage(writeErrorPath);

		return false;
	}

	if (FAILED(PathCchRemoveFileSpec(exePath, MAX_BUFFER_SIZE)))
	{
		strncpy_s(crashErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to remove execute file name", MAX_BUFFER_SIZE);

		std::wstring writeErrorPath = PrintFormat(L"%sWindows-%s-WriteError.txt", crashSavePath, systemTime.c_str());
		WriteErrorMessage(writeErrorPath);

		return false;
	}

	std::wstring savePath = PrintFormat(L"%s\\Crash\\", exePath);

	if (!PathFileExistsW(savePath.c_str()) && !CreateDirectoryW(savePath.c_str(), nullptr))
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			crashErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(crashErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to create crash directory", MAX_BUFFER_SIZE);
		}
		else
		{
			strncat_s(crashErrorMessageBuffer, "\nfailed to create crash directory", MAX_BUFFER_SIZE);
		}

		std::wstring writeErrorPath = PrintFormat(L"%sWindows-%s-WriteError.txt", crashSavePath, systemTime.c_str());
		WriteErrorMessage(writeErrorPath);

		return false;
	}

	wcsncpy_s(crashSavePath, MAX_BUFFER_SIZE, savePath.c_str(), savePath.size());
	return true;
}

void CrashModule::UnregisterExceptionFilter()
{
	SetUnhandledExceptionFilter(topLevelExceptionFilter);
}