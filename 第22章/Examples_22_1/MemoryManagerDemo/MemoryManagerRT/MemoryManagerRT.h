#pragma once

namespace MemoryManagerRT
{
	public ref class CMemoryManagerRT sealed
	{
	public:
		CMemoryManagerRT();
		static uint64 GetProcessCommittedBytes();
		static uint64 GetProcessCommittedLimit();
	};
}