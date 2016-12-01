#pragma once
#include <windows.h>
#include <iostream>

#define _WIN32_WINNT 0x0501
#define WIN32_LEAN_AND_MEAN

#define DllExport  __declspec(dllexport)
#define STDCALL    __cdecl

//export the StartDialog function
#if __cplusplus
extern "C" {
#endif

	DllExport int STDCALL StartDialog(const char *str , int l);

#if __cplusplus
};
#endif

//define message mapping for win messages
#define MESSAGE_MAP(MessageParam, MessageCallback)\
    if(message == MessageParam)\
        return MessageCallback(hwnd, message, wParam, lParam)

#define MESSAGE_MAP_COMMAND(Identifier, MessageCallback)\
    if((message == WM_COMMAND) && (LOWORD(wParam) == Identifier))\
    return MessageCallback(hwnd, message, wParam, lParam);

extern HINSTANCE hInstance;

//define the main program
BOOL CALLBACK DialogProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);

//other member functions
BOOL OnDialogInit(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);
//BOOL OnDialogDestroy(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);
//BOOL OnDialogClose(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);
BOOL OnDialogButtonExitClicked(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);
BOOL OnDialogButtonSayHiClicked(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);