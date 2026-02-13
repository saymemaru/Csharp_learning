using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D9;
using Font = SharpDX.Direct3D9.Font;
using SharpDX.Mathematics.Interop;
using System.Diagnostics;
using System.Numerics;
using System.Windows;

namespace FoxholeArtyHelper
{
    public partial class Form1 : Form
    {
        private KeyHook _keyHook;
        private MouseHook _mouseHook;

        private Direct3D _direct3D;
        private Device _device;
        private Font _font;
        private Sprite _sprite;
        private bool _isInitialized = false;

        private System.Windows.Forms.Timer _renderTimer;

        // 用于使窗口透明和点击穿透
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_TOOLWINDOW = 0x80;
        private const int WS_EX_TOPMOST = 0x8;

        //屏幕API
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        public Form1()
        {
            InitializeForm();

            // 安装全局钩子
            _mouseHook = new MouseHook();
            _keyHook = new KeyHook();
            // 程序退出时卸载钩子
            Application.ApplicationExit += (s, e) =>
            {
                _keyHook.Dispose();
                _mouseHook.Dispose();

            };
            //订阅按键事件
            _keyHook.RightArrowKeyPressed += KeyHook_RightArrowKeyPressed;
            _keyHook.DownArrowKeyPressed += KeyHook_DownArrowKeyPressed;
            _keyHook.UpArrowKeyPressed += KeyHook_UpArrowKeyPressed;
            _keyHook.LeftArrowKeyPressed += KeyHook_LeftArrowKeyPressed;
            _mouseHook.LeftMousePressed += MouseHook_LeftMousePressed;
        }

        private void KeyHook_RightArrowKeyPressed(object? sender, EventArgs e)
        {
  
        }

        private void KeyHook_DownArrowKeyPressed(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void KeyHook_UpArrowKeyPressed(object? sender, EventArgs e)
        {
            if (!isCalibrated && !isCalibrateMode)
            {
                modeText = "测量前请先标定";
            }
            if(isCalibrated && !isMeasuringMode && !isCalibrateMode)
            {
                modeText = "1. 开始测量，请点击地图任意两点（不要移动和缩放）";
                isMeasuringMode = true;
            }
        }

        private bool isMeasuringMode = false;
        private bool isCalibrated = false;
        private bool isCalibrateMode = false;
        public string modeText = "按键盘左方向键标定，上方向键测量，下方向键关闭程序";
        private string mousePosText = "鼠标位置";

        private void KeyHook_LeftArrowKeyPressed(object? sender, EventArgs e)
        {
            if (!isCalibrateMode && ! isMeasuringMode)
            {
                isCalibrateMode = true;
                modeText = "1. 标定模式，请点击水平地图边界线段的两端";  
            }
        }

        private bool hasFirstPoint = false;
        private Vector2 firstPoint;
        private Vector2 secondPoint;

        private float scale;
        private const float mapLength = 1097;
        private void MouseHook_LeftMousePressed(object? sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            //开始标定
            if(isCalibrateMode)
            {
                //有第一个点
                if(hasFirstPoint)
                {
                    hasFirstPoint = false;
                    secondPoint = new Vector2(me.X, me.Y);
                    mousePosText += $",（{me.X}，{me.Y}）";

                    //计算比例
                    scale = mapLength / MathF.Abs(secondPoint.X - firstPoint.X);
                    isCalibrateMode = false;
                    isCalibrated = true;
                    mousePosText += $", 比例 = {scale}";
                    modeText = "3. 标定完成，按上方向键开始测量";
                }
                //无第一个点
                else
                {
                    mousePosText = "";
                    hasFirstPoint = true;
                    firstPoint = new Vector2(me.X, me.Y);
                    mousePosText = $"（{me.X}，{me.Y}）";
                    modeText = "2. 标定模式，请点击地图边界线段另一端";
                }

            }

            //测量模式
            if(isMeasuringMode)
            {
                //有第一个点
                if (hasFirstPoint)
                {
                    hasFirstPoint = false;
                    secondPoint = new Vector2(me.X, me.Y);
                    mousePosText += $",（{me.X}，{me.Y}）";

                    isMeasuringMode = false;
                    Vector2 vector = secondPoint - firstPoint;
                    float angle = MyUtility.GetFullAngleBetweenVectors(- Vector2.UnitY, vector);
                    float distance = vector.Length() * scale;

                    mousePosText += $", 距离 = {distance}，角度 = {angle}";
                    modeText = "3. 测量完成，缩放地图后请重新标定";
                }
                //无第一个点
                else
                {
                    mousePosText = "";
                    hasFirstPoint = true;
                    firstPoint = new Vector2(me.X, me.Y);
                    mousePosText = $"（{me.X}，{me.Y}）";
                    modeText = "2. 测量模式，请点击目标位置";
                }
            }

        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 清理资源
            _sprite?.Dispose();
            _font?.Dispose();
            _device?.Dispose();
            _direct3D?.Dispose();

            _mouseHook.LeftMousePressed -= MouseHook_LeftMousePressed;
            _keyHook.LeftArrowKeyPressed -= KeyHook_LeftArrowKeyPressed;
            _keyHook.UpArrowKeyPressed -= KeyHook_UpArrowKeyPressed;
            _keyHook.DownArrowKeyPressed -= KeyHook_DownArrowKeyPressed;
            _keyHook.RightArrowKeyPressed -= KeyHook_RightArrowKeyPressed;
            _keyHook.Dispose();
            base.OnFormClosing(e);
        }


        #region Form
        private int sizeHeight = 200;
        private void InitializeForm()
        {
            // 设置窗口属性
            this.Text = "DirectX Overlay";
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;

            // 获取屏幕尺寸
            var screenBounds = Screen.PrimaryScreen.Bounds;
            this.Location = new Point(0, 0);
            this.Size = new Size(screenBounds.Width, sizeHeight); // 顶部100像素高度

            // 设置窗口样式
            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20,
                initialStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);

            // 设置窗口始终在最前
            SetWindowPos(this.Handle, new IntPtr(-1), 0, 0, 0, 0, 0x0001 | 0x0002);

            // 设置透明背景
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;
            this.TopMost = true;

            // 设置窗口点击穿透
            this.AllowTransparency = true;
        }

        /*protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            base.OnKeyDown(e);
        }*/

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;

            // 使窗口点击穿透
            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)(-1); // HTTRANSPARENT
                return;
            }

            base.WndProc(ref m);
        }
        #endregion

        #region DirectX
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeDirectX();
            _isInitialized = true;
        }
        
        private void InitializeDirectX()
        {
            try
            {
                // 创建Direct3D对象
                _direct3D = new Direct3D();

                // 获取屏幕尺寸
                var screenBounds = Screen.PrimaryScreen.Bounds;

                // 创建Present参数，显示在winform窗口中
                var presentParams = new PresentParameters
                {
                    Windowed = true,
                    SwapEffect = SwapEffect.Discard,
                    DeviceWindowHandle = this.Handle,
                    PresentationInterval = PresentInterval.Default,
                    BackBufferFormat = Format.A8R8G8B8,
                    BackBufferWidth = screenBounds.Width,
                    BackBufferHeight = sizeHeight,
                    MultiSampleType = MultisampleType.None,
                    MultiSampleQuality = 0
                };

                // 创建设备
                _device = new Device(_direct3D, 0, DeviceType.Hardware, this.Handle,
                    CreateFlags.HardwareVertexProcessing, presentParams);

                // 创建字体
                _font = new Font(_device, new SharpDX.Direct3D9.FontDescription
                {
                    Height = 30,
                    Italic = false,
                    FaceName = "Arial",
                    Width = 0,
                    MipLevels = 1,
                    CharacterSet = SharpDX.Direct3D9.FontCharacterSet.Default,
                    OutputPrecision = FontPrecision.Default,
                    Quality = FontQuality.ClearTypeNatural,
                    PitchAndFamily = FontPitchAndFamily.Default | FontPitchAndFamily.DontCare,
                    Weight = FontWeight.Bold
                });

                // 创建Sprite用于绘制
                _sprite = new Sprite(_device);

                //渲染计时器
                _renderTimer = new System.Windows.Forms.Timer();
                _renderTimer.Interval = 40; // 大约25FPS
                _renderTimer.Tick += (s, args) => Render();
                _renderTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DirectX初始化失败: {ex.Message}");
                this.Close();
            }
        }

        private void Render()
        {
            try
            {
                if (_device == null || _font == null)
                {
                    Debug.WriteLine("未发现设备，未发现字体");
                    return;
                }
                    

                // 开始渲染
                _device.Clear(ClearFlags.Target, new RawColorBGRA(0, 0, 0, 0), 1.0f, 0);
                _device.BeginScene();
                _sprite.Begin(SpriteFlags.AlphaBlend);

                // 绘制文本
                string text = $"Foxhole Arty Helper 0.1 - FPS: {CalculateFPS()} - Time: {DateTime.Now:HH:mm:ss}";

                // 白色文本，带黑色边框实现更好的可读性
                _font.DrawText(_sprite, text, 10, 10, new RawColorBGRA(0, 0, 0, 0));
                _font.DrawText(_sprite, text, 12, 12, new RawColorBGRA(0, 0, 0, 0));
                _font.DrawText(_sprite, text, 11, 11, new RawColorBGRA(255, 255, 255, 255));

                // 绘制多个文本示例
                _font.DrawText(_sprite, this.modeText, 10, 50, new RawColorBGRA(255, 255, 0, 255));
                _font.DrawText(_sprite, this.mousePosText, 10, 90, new RawColorBGRA(0, 0, 255, 255));

                // 结束渲染
                _sprite.End();
                _device.EndScene();
                _device.Present();

            }
            catch (SharpDX.SharpDXException)
            {
                // 设备丢失，重新初始化
                InitializeDirectX();
            }
        }

        private int _frameCount = 0;
        private DateTime _lastTime = DateTime.Now;
        private int _fps = 0;

        private int CalculateFPS()
        {
            _frameCount++;
            var currentTime = DateTime.Now;
            var elapsed = (currentTime - _lastTime).TotalSeconds;

            if (elapsed >= 1.0)
            {
                _fps = (int)(_frameCount / elapsed);
                _frameCount = 0;
                _lastTime = currentTime;
            }

            return _fps;
        }

        #endregion

       

    }
}
