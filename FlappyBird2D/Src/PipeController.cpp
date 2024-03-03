#include "Assertion.h"
#include "PipeController.h"
#include "Pipe.h"

PipeController::PipeController(const std::vector<Pipe*>& pipes, float gap)
	: pipes_(pipes)
	, gap_(gap)
{
	bIsInitialized_ = true;
}

PipeController::~PipeController()
{
	if (bIsInitialized_)
	{
		Release();
	}
}

void PipeController::Tick(float deltaSeconds)
{
	for (Pipe* pipe : pipes_)
	{
		if (pipe->GetStatus() == Pipe::EStatus::Active)
		{
			continue;
		}

		bool bActive = true;
		for (Pipe* other : pipes_)
		{
			if (other->GetStatus() == Pipe::EStatus::Active && other->GetGapPipe(pipe) <= gap_)
			{
				bActive = false;
			}
		}

		if (bActive)
		{
			pipe->SetStatus(Pipe::EStatus::Active);
		}
	}
}

void PipeController::Render()
{
}

void PipeController::Release()
{
	if (bIsInitialized_)
	{
		bIsInitialized_ = false;
	}
}