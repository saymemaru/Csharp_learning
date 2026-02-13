using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FoxholeArtyHelper
{
    internal class KeyHook:IDisposable
    {
        #region 钩子
        //按键API
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        // 常量
        private const int WH_KEYBOARD_LL = 13; // 低级键盘钩子
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101; //按键释放
        private const int VK_LEFT = 0x25; // 方向键左
        private const int VK_UP = 0x26; // 方向键上
        private const int VK_DOWN = 0x28;//方向键 下
        private const int VK_RIGHT = 0x27;//方向键 右

        // 钩子句柄和委托
        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelKeyboardProc _proc;

        // 防止重复触发的计时器
        private DateTime _lastKeyPressTime = DateTime.MinValue;
        private const int KEY_REPEAT_DELAY_MS = 100; // 100毫秒内不重复触发

        public KeyHook()
        {
            StartGlobalHook();
        }

        private void StartGlobalHook()
        {
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                // 检查是否是按键按下事件
                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    // 防止重复触发（当按住按键时）
                    if ((DateTime.Now - _lastKeyPressTime).TotalMilliseconds > KEY_REPEAT_DELAY_MS)
                    {
                        _lastKeyPressTime = DateTime.Now;

                        // 使用switch case检测不同按键
                        switch (vkCode)
                        {
                            case VK_LEFT: //  方向键左
                                OnLeftArrowKeyPressed();
                                break;
                            case VK_UP:
                                OnUpArrowKeyPressed();
                                break;
                            case VK_DOWN: //  方向键下
                                OnDownArrowKeyPressed();
                                break;
                            case VK_RIGHT: //  方向键右
                                OnRightArrowKeyPressed();
                                break;
                        }
                    }
                }
            }
            // 传递消息给下一个钩子
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public event EventHandler? LeftArrowKeyPressed;
        public event EventHandler? UpArrowKeyPressed;
        public event EventHandler? DownArrowKeyPressed;
        public event EventHandler? RightArrowKeyPressed;

        private void OnRightArrowKeyPressed()
        {
            DownArrowKeyPressed?.Invoke(this, EventArgs.Empty);
        }
        private void OnDownArrowKeyPressed()
        {
            DownArrowKeyPressed?.Invoke(this, EventArgs.Empty);
        }
        private void OnUpArrowKeyPressed()
        {
            UpArrowKeyPressed?.Invoke(this, EventArgs.Empty);
        }
        private void OnLeftArrowKeyPressed()
        {
            LeftArrowKeyPressed?.Invoke(this, EventArgs.Empty);
        }

        private void StopGlobalHook()
        {
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
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
        ~KeyHook()
        {
            Dispose(false);
        }


        #endregion
    }
}
