#include "Macro.h"
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

bool INIFormat::IsValidKey(const std::string& key) const
{
	return iniSection_.find(key) != iniSection_.end();
}
