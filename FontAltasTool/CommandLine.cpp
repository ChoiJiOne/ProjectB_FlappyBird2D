#include "StringHelper.hpp"
#include "CommandLine.h"

std::string CommandLine::executePath_;
std::unordered_map<std::string, std::string> CommandLine::arguments_;

void CommandLine::Parse(int32_t argc, char* argv[])
{
	executePath_ = argv[0];

	for (int32_t index = 1; index < argc; ++index)
	{
		std::string argument = argv[index];
		std::vector<std::string> elements = StringHelper::Split(argument, "=");

		if (elements.size() == 2)
		{
			arguments_.insert({ elements.front(), elements.back() });
		}
	}
}

bool CommandLine::IsValid(const std::string& key)
{
	return (arguments_.find(key) != arguments_.end());
}

std::string CommandLine::GetValue(const std::string& key)
{
	CHECK(IsValid(key), "not valid key value...");
	return arguments_.at(key);
}
