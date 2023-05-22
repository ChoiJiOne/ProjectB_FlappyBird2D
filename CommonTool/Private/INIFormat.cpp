#include <iostream>

#include "Macro.h"
#include "FileHelper.hpp"
#include "StringHelper.hpp"
#include "INIFormat.h"

void INIFormat::AddSection(const std::string& key, const INISection& section)
{
	CHECK(!IsValidKey(key), "collision ini section...");

	iniSection_.insert({ key, section });
}

INISection& INIFormat::GetSection(const std::string& key)
{
	CHECK(IsValidKey(key), "invalid ini section...");

	return iniSection_.at(key);
}

const INISection& INIFormat::GetSection(const std::string& key) const
{
	CHECK(IsValidKey(key), "invalid ini section...");

	return iniSection_.at(key);
}

void INIFormat::RemoveSection(const std::string& key)
{
	if (IsValidKey(key))
	{
		iniSection_.erase(key);
	}
}

void INIFormat::ExportINIFile(const std::string& path, const INIFormat& iniFormat)
{
	const std::map<std::string, INISection>& sections = iniFormat.GetSections();

	std::string context;
	for (const auto& section : sections)
	{
		context += StringHelper::Format("[%s]\n", section.first.c_str());

		const std::map<std::string, std::string>& datas = section.second.GetSectionData();
		
		for (const auto& data : datas)
		{
			context += StringHelper::Format("%s=%s\n", data.first.c_str(), data.second.c_str());
		}
	}

	std::vector<uint8_t> contextBuffer;
	contextBuffer.assign(context.begin(), context.end());

	FileHelper::WriteBufferFromFile(path, contextBuffer);
}

bool INIFormat::IsValidKey(const std::string& key) const
{
	return iniSection_.find(key) != iniSection_.end();
}
