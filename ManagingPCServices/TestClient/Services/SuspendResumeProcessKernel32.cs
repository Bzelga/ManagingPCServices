using System;
using System.Runtime.InteropServices;

namespace TestClient.Services
{
    public class SuspendResumeProcessKernel32
    {
        [Flags]
        public enum ThreadAccess : int
        {
            SUSPEND_RESUME = 0x0002
        }
        
        private static class Kernel32
        {
            [DllImport("kernel32", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern nint OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
            [DllImport("kernel32", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern uint SuspendThread(nint hThread);
            [DllImport("kernel32", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int ResumeThread(nint hThread);
            [DllImport("kernel32", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool CloseHandle(nint handle);
        }

        public nint OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId)
        {
            return Kernel32.OpenThread(dwDesiredAccess, bInheritHandle, dwThreadId);
        }

        public uint SuspendThread(nint hThread)
        {
            return Kernel32.SuspendThread(hThread);
        }

        public int ResumeThread(nint hThread)
        {
            return Kernel32.ResumeThread(hThread);
        }

        public bool CloseHandle(nint handle)
        {
            return Kernel32.CloseHandle(handle);
        }
    }
}
