﻿// dllmain.cpp : 定義 DLL 應用程式的進入點。
#include "stdafx.h"
#include "DLLHeaders.h"

HINSTANCE hInstance;  //declared in DLLHeader.h

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		hInstance = hModule;
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

