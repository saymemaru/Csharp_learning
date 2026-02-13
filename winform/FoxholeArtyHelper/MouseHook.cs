using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FoxholeArtyHelper
{
    internal class MouseHook
    {
        // 定义钩子委托
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        // API函数声明
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // 鼠标消息常量
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_RBUTTONUP = 0x0205;
        private const int WM_MBUTTONDOWN = 0x0207;
        private const int WM_MBUTTONUP = 0x0208;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_MOUSEMOVE = 0x0200;

        // 钩子句柄
        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelMouseProc _proc;

        // 事件
        public event EventHandler<MouseEventArgs> MouseAction;

        public MouseHook()
        {
            StartGlobalHook();
        }

        // 安装全局钩子
        public void StartGlobalHook()
        {
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        // 卸载钩子
        public void StopGlobalHook()
        {
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
        }

        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // 解析鼠标消息
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                // 获取屏幕坐标
                Point point = new Point(hookStruct.pt.x, hookStruct.pt.y);

                // 触发事件
                MouseButtons button = MouseButtons.None;
                int clicks = 1;

                switch ((int)wParam)
                {
                    case WM_LBUTTONDOWN:
                        button = MouseButtons.Left;
                        OnLeftMousePressed(button,clicks, point.X, point.Y);
                        break;
                    case WM_RBUTTONDOWN:
                        button = MouseButtons.Right;
                        break;
                    /*case WM_MBUTTONDOWN:
                        button = MouseButtons.Middle;
                        break;
                    case WM_MOUSEWHEEL:
                        // 可以处理滚轮事件
                        break;*/
                }
                /*if (button != MouseButtons.None)
                {
                    MouseAction?.Invoke(null, new MouseEventArgs(button, clicks, point.X, point.Y, 0));
                }*/
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public event EventHandler? LeftMousePressed;
        //按下向左键
        private void OnLeftMousePressed(MouseButtons button, int clicks, int x, int y)
        {
            LeftMousePressed?.Invoke(this, new MouseEventArgs(button, clicks, x, y, 0));
        }


        // 释放标志
        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // 释放托管资源（如果有）
                // 目前没有其它托管可释放项，但保留位置以便扩展
            }

            // 释放非托管资源：确保钩子被撤销
            try
            {
                StopGlobalHook();
            }
            catch
            {
                // 不要在析构/Dispose 中抛出异常，捕获并吞掉或记录（此处简单吞掉）
            }

            // 释放对回调委托的引用，允许 GC 回收
            _proc = null;
            _disposed = true;
        }

        // 终结器，防止忘记调用 Dispose 时资源泄露
        ~MouseHook()
        {
            Dispose(false);
        }

        // 鼠标钩子结构体
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
    }
}

