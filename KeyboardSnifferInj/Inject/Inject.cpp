#include "pch.h"
#include "Inject.h"
#include <string>

void inject_DLL(const char* file_name, int PID)
{
    HANDLE h_process, h_rThread;
    char fullDLLPath[260];
    LPVOID DLLPath_addr, LoadLib_addr;
    DWORD exit_code;

    h_process = OpenProcess(PROCESS_ALL_ACCESS, FALSE, PID);

    GetFullPathNameA(file_name, 260, fullDLLPath, NULL);

    DLLPath_addr = VirtualAllocEx(h_process, NULL, 260,
        MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);

    WriteProcessMemory(h_process, DLLPath_addr, fullDLLPath,
        strlen(fullDLLPath), NULL);

    LoadLib_addr = GetProcAddress(GetModuleHandleA("Kernel32"), "LoadLibraryA");

    h_rThread = CreateRemoteThread(h_process, NULL, 0,
        (LPTHREAD_START_ROUTINE)LoadLib_addr, DLLPath_addr, 0, NULL);

    WaitForSingleObject(h_rThread, INFINITE);

    GetExitCodeThread(h_rThread, &exit_code);


    CloseHandle(h_rThread);
    VirtualFreeEx(h_process, DLLPath_addr, 0, MEM_RELEASE);
    CloseHandle(h_process);
}