// KeyboardDLL.cpp : 定義 DLL 應用程式的匯出函式。
//

#include "stdafx.h"
#include "DLLHeaders.h"
#include "resource.h"
#include <string>
using namespace std;
string theText;

//the exported function for clients to call in DLL
int StartDialog(const char* inStr, int length)
{
	string s(inStr);
	theText = s;

	//disable parent window
	HWND parent = FindWindowA(NULL, "MainWindow");
	if (parent != NULL)
		EnableWindow(parent, false);

	DialogBox(hInstance, MAKEINTRESOURCE(ID_DLL_Dialog), NULL, DialogProc);

	//enable parent window
	if (parent != NULL)
		EnableWindow(parent, true);


	return 10;
}

BOOL CALLBACK DialogProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	MESSAGE_MAP(WM_INITDIALOG, OnDialogInit);
	//MESSAGE_MAP(WM_DESTROY, OnDialogDestroy);
	//MESSAGE_MAP(WM_CLOSE, OnDialogClose);

	switch (message)
	{
	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case IDD_BUTTON_EXIT:
		{
			EndDialog(hwnd, NULL);
			return TRUE;
		}
		}
	}
	//MESSAGE_MAP_COMMAND(IDD_BUTTON_SAYHI, OnDialogButtonSayHiClicked);

	return FALSE;
}

BOOL OnDialogInit(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	//USES_CONVERSION;

	/*r.buttonState = false;
	r.s = L"nothing yet";

	SetWindowTextW(hwnd, gInTitle.c_str());*/
	SetDlgItemTextA(hwnd, ID_Text3, theText.c_str());

	return TRUE;
}

/*BOOL OnDialogDestroy(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	PostQuitMessage(0);
	return TRUE;
}*/

/*BOOL OnDialogClose(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	DestroyWindow(hwnd);
	return TRUE;
}*/

BOOL OnDialogButtonExitClicked(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	/*WCHAR out[150] = {};
	GetDlgItemTextW(hwnd, ID_TextBox, out, 150);
	wstring ws(out);
	//string outString(ws.begin(), ws.end());

	r.buttonState = false;
	r.s = ws;*/

	DestroyWindow(hwnd);
	//OnDialogClose(hwnd, message, wParam, lParam);
	return FALSE;
}

BOOL OnDialogButtonSayHiClicked(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	/*WCHAR out[150] = {};
	GetDlgItemTextW(hwnd, ID_TextBox, out, 150);
	wstring ws(out);
	//string outString(ws.begin(), ws.end());

	r.buttonState = true;
	r.s = ws;

	/*string s = "Hi, :D\n" + to_string(inputInt);
	MessageBoxA(hwnd, s.c_str(), "DLL", MB_OK);*/
	//OnDialogClose(hwnd, message, wParam, lParam);
	return TRUE;
}