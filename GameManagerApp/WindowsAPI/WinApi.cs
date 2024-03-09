using System;
using System.Runtime.InteropServices;

public static class WinApi
{
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    // 委托定义用于 EnumWindows 方法
    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    // 根据进程ID查找并激活窗口
    public static void BringProcessToFront(int processId)
    {
        EnumWindows((hWnd, lParam) =>
        {
            GetWindowThreadProcessId(hWnd, out uint windowProcessId);
            if (windowProcessId == processId)
            {
                SetForegroundWindow(hWnd);
                return false; // 找到窗口，停止枚举
            }
            return true; // 继续枚举
        }, IntPtr.Zero);
    }
}
