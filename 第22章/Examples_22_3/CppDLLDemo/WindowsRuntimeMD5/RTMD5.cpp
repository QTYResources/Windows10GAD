#include "pch.h"
#include "RTMD5.h"
#include "md5.h"
#include <string>
#include <vector>
#include <iostream>

using namespace WindowsRuntimeMD5;
using namespace Platform;
using namespace std;

RTMD5::RTMD5()
{
}

Platform::String^ RTMD5::GetMd5(Platform::String ^text)
{
	std::wstring strr(text->Data());
	std::locale const loc("");
	wchar_t const* from = strr.c_str();
	std::size_t const len = strr.size();
	std::vector<char> buffer(len + 1);
	std::use_facet<std::ctype<wchar_t>>(loc).narrow(from, from + len, '_', &buffer[0]);
	std::string str = std::string(&buffer[0], &buffer[len]);
	MD5 md5(str);
	string result = md5.md5();

	std::wstring wstr;
	wstr.resize(result.size() + 1);
	size_t charsConverted;
	errno_t err = ::mbstowcs_s(&charsConverted, (wchar_t *)wstr.data(), wstr.size(), result.data(), result.size());
	wstr.pop_back();
	return ref new String(wstr.c_str());
}

