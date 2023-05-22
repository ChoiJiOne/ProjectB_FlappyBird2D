#include "Macro.h"
#include "INISection.h"

void INISection::AddData(const std::string& key, const std::string& value)
{
	CHECK(!IsValidKey(key), "collision ini section key...");

	sectionData_.insert({ key, value });
}

void INISection::AddEnforceData(const std::string& key, const std::string& value)
{
	if (IsValidKey(key))
	{
		RemoveData(key);
	}

	sectionData_.insert({ key, value });
}

const std::pair<std::string, std::string>& INISection::GetData(const std::string& key) const
{
	CHECK(IsValidKey(key), "invalid ini section key...");

	return *sectionData_.find(key);
}

void INISection::RemoveData(const std::string& key)
{
	if (IsValidKey(key))
	{
		sectionData_.erase(key);
	}
}

const std::string& INISection::GetValue(const std::string& key) const
{
	CHECK(IsValidKey(key), "invalid ini section key...");

	return sectionData_.find(key)->second;
}

bool INISection::IsValidKey(const std::string& key) const
{
	return sectionData_.find(key) != sectionData_.end();
}