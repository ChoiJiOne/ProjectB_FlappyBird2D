#include "FileModule.h"

#include <shlwapi.h>

const uint32_t MAX_BUFFER_SIZE = 1024;
char fileErrorMessageBuffer[MAX_BUFFER_SIZE];

const char* FileModule::GetErrorMessage()
{
	return &fileErrorMessageBuffer[0];
}

bool FileModule::ReadBufferFromFile(const std::string& path, std::vector<uint8_t>& outBuffer)
{
	HANDLE fileHandle = CreateFileA(path.c_str(), GENERIC_READ, FILE_SHARE_READ, nullptr, OPEN_EXISTING, 0, nullptr);
	if (fileHandle == INVALID_HANDLE_VALUE)
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to open file", MAX_BUFFER_SIZE);
		}

		return false;
	}

	DWORD fileSize = GetFileSize(fileHandle, nullptr);
	outBuffer.resize(fileSize);

	DWORD bytesRead;
	if (!ReadFile(fileHandle, &outBuffer[0], fileSize, &bytesRead, nullptr))
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to read file", MAX_BUFFER_SIZE);
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
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to close file", MAX_BUFFER_SIZE);
		}

		return false;
	}

	return true;
}

bool FileModule::ReadBufferFromFile(const std::wstring& path, std::vector<uint8_t>& outBuffer)
{
	HANDLE fileHandle = CreateFileW(path.c_str(), GENERIC_READ, FILE_SHARE_READ, nullptr, OPEN_EXISTING, 0, nullptr);
	if (fileHandle == INVALID_HANDLE_VALUE)
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to open file", MAX_BUFFER_SIZE);
		}

		return false;
	}

	DWORD fileSize = GetFileSize(fileHandle, nullptr);
	outBuffer.resize(fileSize);

	DWORD bytesRead;
	if (!ReadFile(fileHandle, &outBuffer[0], fileSize, &bytesRead, nullptr))
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to open file", MAX_BUFFER_SIZE);
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
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to open file", MAX_BUFFER_SIZE);
		}

		return false;
	}

	return true;
}

bool FileModule::WriteBufferToFile(const std::string& path, const std::vector<uint8_t>& buffer)
{
	HANDLE fileHandle = CreateFileA(path.c_str(), GENERIC_WRITE, 0, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
	if (fileHandle == INVALID_HANDLE_VALUE)
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to create file", MAX_BUFFER_SIZE);
		}

		return false;
	}

	DWORD writeByteSize = 0;
	if (!WriteFile(fileHandle, &buffer[0], static_cast<DWORD>(buffer.size()), &writeByteSize, nullptr))
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to write file", MAX_BUFFER_SIZE);
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
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to close file", MAX_BUFFER_SIZE);
		}

		return false;
	}

	return true;
}

bool FileModule::WriteBufferToFile(const std::wstring& path, const std::vector<uint8_t>& buffer)
{
	HANDLE fileHandle = CreateFileW(path.c_str(), GENERIC_WRITE, 0, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
	if (fileHandle == INVALID_HANDLE_VALUE)
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to create file", MAX_BUFFER_SIZE);
		}

		return false;
	}


	DWORD writeByteSize = 0;
	if (!WriteFile(fileHandle, &buffer[0], static_cast<DWORD>(buffer.size()), &writeByteSize, nullptr))
	{
		uint32_t size = FormatMessageA
		(
			FORMAT_MESSAGE_FROM_SYSTEM,
			nullptr,
			static_cast<DWORD>(GetLastError()),
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to write file", MAX_BUFFER_SIZE);
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
			fileErrorMessageBuffer,
			MAX_BUFFER_SIZE,
			nullptr
		);

		if (size == 0)
		{
			strncpy_s(fileErrorMessageBuffer, MAX_BUFFER_SIZE, "failed to close file", MAX_BUFFER_SIZE);
		}

		return false;
	}

	return true;
}

bool FileModule::IsValidPath(const std::string& path)
{
	return PathFileExistsA(path.c_str());
}

bool FileModule::IsValidPath(const std::wstring& path)
{
	return PathFileExistsW(path.c_str());
}

std::string FileModule::GetBasePath(const std::string& path)
{
	std::size_t lastSlash;

	if ((lastSlash = path.rfind('/')) != std::string::npos)
	{
		return path.substr(0, lastSlash + 1);
	}
	else if ((lastSlash = path.rfind('\\')) != std::string::npos)
	{
		return path.substr(0, lastSlash + 1);
	}
	else
	{
		return "";
	}
}

std::wstring FileModule::GetBasePath(const std::wstring& path)
{
	std::size_t lastSlash;

	if ((lastSlash = path.rfind(L'/')) != std::wstring::npos)
	{
		return path.substr(0, lastSlash + 1);
	}
	else if ((lastSlash = path.rfind(L'\\')) != std::wstring::npos)
	{
		return path.substr(0, lastSlash + 1);
	}
	else
	{
		return L"";
	}
}

std::string FileModule::RemoveBasePath(const std::string& path)
{
	std::size_t lastSlash;

	if ((lastSlash = path.rfind('/')) != std::string::npos)
	{
		return path.substr(lastSlash + 1, std::string::npos);
	}
	else if ((lastSlash = path.rfind('\\')) != std::string::npos)
	{
		return path.substr(lastSlash + 1, std::string::npos);
	}
	else
	{
		return path;
	}
}

std::wstring FileModule::RemoveBasePath(const std::wstring& path)
{
	std::size_t lastSlash;

	if ((lastSlash = path.rfind(L'/')) != std::string::npos)
	{
		return path.substr(lastSlash + 1, std::string::npos);
	}
	else if ((lastSlash = path.rfind(L'\\')) != std::string::npos)
	{
		return path.substr(lastSlash + 1, std::string::npos);
	}
	else
	{
		return path;
	}
}

std::string FileModule::GetFileExtension(const std::string& path)
{
	std::string filename = RemoveBasePath(path);
	std::size_t offset = filename.rfind('.');

	return (offset == std::string::npos) ? "" : filename.substr(offset + 1);
}

std::wstring FileModule::GetFileExtension(const std::wstring& path)
{
	std::wstring filename = RemoveBasePath(path);
	std::size_t offset = filename.rfind('.');

	return (offset == std::wstring::npos) ? L"" : filename.substr(offset + 1);
}