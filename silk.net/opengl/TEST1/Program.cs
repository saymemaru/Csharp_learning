using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using TEST1.Model;

namespace TEST1
{
    class Program
    {
        private static IWindow _window;
        private static GL _gl;
        private static Renderer _renderer;
        private static Loader _loader;

        private static StaticShaderColorful _shader;
        private static StaticShaderOrange _shader2;
        private static RawModel rawModel;
        private static RawModel rawModel2;
        private static Texture texture; 
        private static TexturedModel texturedModel;
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

            //Set-up input context.
            IInputContext input = _window.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                input.Keyboards[i].KeyDown += KeyDown;
            }

            //render test
            float[] vertices1 =
            {
                 0.5f,  0.5f, 0.0f, 
                 0.5f, -0.5f, 0.0f,
                -0.5f, -0.5f, 0.0f,
                -0.5f,  0.5f, 0.0f,
            };
            uint[] indices1 =
            {
                0u, 1u, 3u,
                1u, 2u, 3u
            };

            float[] vertices2 =
            {
                 0.3f,  0.3f, 0.1f,
                 0.3f, -0.3f, 0.1f,
                -0.3f, -0.3f, 0.1f,
                -0.3f,  0.3f, 0.1f,
            };

            _shader = new StaticShaderColorful(_gl);
            _shader2 = new StaticShaderOrange(_gl);

            rawModel = _loader.LoadToVAO(vertices1,indices1);
            rawModel2 = _loader.LoadToVAO(vertices2,indices1);

            texture = _loader.LoadTexture("silk.png");
            texturedModel = new TexturedModel(rawModel, texture);
            

        }

        private static void OnRender(double obj)
        {
            //Here all rendering should be done.
            _renderer.Prepare();

            _shader.Start();
            _renderer.Render(texturedModel);
            _shader.Stop();

           /* _shader2.Start();
            _renderer.Render(rawModel2);
            _shader2.Stop();*/
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
                _loader.Dispose();
                _window.Close();
                
            }
            if(arg2 == Key.L)
            {
                _gl.PolygonMode(GLEnum.FrontAndBack,GLEnum.Line);
            }
            if(arg2 == Key.R)
            {
                _gl.PolygonMode(GLEnum.FrontAndBack, GLEnum.Fill);
            }
        }
    }
}