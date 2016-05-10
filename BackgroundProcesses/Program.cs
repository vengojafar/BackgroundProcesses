using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundProcesses
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] plist = Process.GetProcesses();
            foreach (Process p in plist)
            {
                if (hasWindowStyle(p))
                    Console.WriteLine(p.ProcessName);
            }
            Console.Read();
        }

        public static bool hasWindowStyle(Process p)
        {
            IntPtr hnd = p.MainWindowHandle;
            UInt32 WS_DISABLED = 0x8000000;
            int GWL_STYLE = -16;
            bool visible = false;
            if (hnd != IntPtr.Zero)
            {
                UInt32 style = GetWindowLong(hnd, GWL_STYLE);
                visible = ((style & WS_DISABLED) != WS_DISABLED);
            }
            return visible;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
    }
}
