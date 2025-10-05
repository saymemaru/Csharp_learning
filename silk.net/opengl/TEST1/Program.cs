using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;

namespace TEST1
{
    class Program
    {
        private static IWindow _window;
        private static GL _gl;
        private static Renderer _renderer;
        private static Loader _loader;

        private static void Main(string[] args)
        {
           
            //Create a window.
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "LearnOpenGL with Silk.NET";

            _window = Window.Create(options);

            //Assign events.
            _window.Load += OnLoad;
            _window.Update += OnUpdate;
            _window.Render += OnRender;
            _window.FramebufferResize += OnFramebufferResize;

            //Run the window.
            _window.Run();

            // window.Run() is a BLOCKING method - this means that it will halt execution of any code in the current
            // method until the window has finished running. Therefore, this dispose method will not be called until you
            // close the window.
            _window.Dispose();
        }


        private static void OnLoad()
        {
            
            Console.WriteLine("Load!");

            _gl = GL.GetApi(_window);
            _gl.ClearColor(Color.CornflowerBlue);

            _loader = new(_gl);
            _renderer = new(_gl);
            StaticShader staticShader = new(_gl);

            //Set-up input context.
            IInputContext input = _window.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                input.Keyboards[i].KeyDown += KeyDown;
            }

            //render test
            float[] vertices =
            {
                 0.5f,  0.5f, 0.0f, 
                 0.5f, -0.5f, 0.0f,
                -0.5f, -0.5f, 0.0f,
                -0.5f,  0.5f, 0.0f,
            };
            uint[] indices =
            {
                0u, 1u, 3u,
                1u, 2u, 3u
            };

            RawModel rawModel = _loader.LoadToVAO(vertices, indices);
            staticShader.Start();
            _renderer.Render(rawModel);
        }

        private static void OnRender(double obj)
        {
            //Here all rendering should be done.
            //_gl.Clear(ClearBufferMask.ColorBufferBit);
            _renderer.Prepare();
            
        }

        private static void OnUpdate(double obj)
        {
            //Here all updates to the program should be done.
        }

        private static void OnFramebufferResize(Vector2D<int> newSize)
        {
            //Update aspect ratios, clipping regions, viewports, etc.
        }

        private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
        {
            //Check to close the window on escape.
            if (arg2 == Key.Escape)
            {
                _loader.CleanUp();
                _window.Close();
                
            }
        }
    }
}