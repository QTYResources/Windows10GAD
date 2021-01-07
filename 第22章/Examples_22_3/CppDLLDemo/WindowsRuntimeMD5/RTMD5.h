#pragma once
using namespace std;

namespace WindowsRuntimeMD5
{
	public ref class RTMD5 sealed
	{
	public:
		RTMD5();
		Platform::String^ GetMd5(Platform::String ^text);
	};
}