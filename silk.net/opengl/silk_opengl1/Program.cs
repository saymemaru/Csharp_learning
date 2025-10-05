using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using StbImageSharp; // For image loading
using System.Runtime.InteropServices;
// If you're using an earlier version of .NET which doesn't have file-scoped namespaces (.NET 5 or earlier), you'll need to encase your class inside the namespace.
namespace MySilkProgram;

public class Program
{
    private static IWindow _window;
    private static GL _gl;
    private static uint _vao;
    private static uint _vbo;
    private static uint _ebo;
    private static uint _program;

    private static uint _texture;

    public static void Main(string[] args)
    {
        WindowOptions options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(800, 600),
            Title = "My first Silk.NET application!"
        };

        _window = Window.Create(options);
        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;
        _window.Run();

    }


    private static unsafe void OnLoad()
    {
        Console.WriteLine("Load!");

        _gl = _window.CreateOpenGL();
        _gl.ClearColor(Color.CornflowerBlue);

        //窗口输入
        IInputContext input = _window.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++)
            input.Keyboards[i].KeyDown += KeyDown;

        //顶点数组对象
        _vao = _gl.GenVertexArray();
        //绑定对象更新gl状态
        _gl.BindVertexArray(_vao);
        //顶点数组
        //3位aPosition | 2位aTexCoords
        float[] vertices =
        {
             0.5f,  0.5f, 0.0f,  1.0f, 0.0f,
             0.5f, -0.5f, 0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, 0.0f,  0.0f, 1.0f,
            -0.5f,  0.5f, 0.0f,  0.0f, 0.0f
        };

        //顶点缓冲区对象
        _vbo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

        uint[] indices =
        {
            0u, 1u, 3u,
            1u, 2u, 3u
        };

        //元素缓冲区对象
        _ebo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);

        fixed (uint* buf = indices)
            _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(indices.Length * sizeof(uint)), buf, BufferUsageARB.StaticDraw);


        fixed (float* buf = vertices)
            _gl.BufferData(
                BufferTargetARB.ArrayBuffer,
                (nuint)(vertices.Length * sizeof(float)),
                buf,
                BufferUsageARB.StaticDraw);
        //StaticDraw - 设置一次数据，并且只能由 GPU 读取（在本例中用于绘图）
        //DynamicDraw - 与 StaticDraw 类似，但数据将被多次设置和更新。

        //创建顶点着色器
        //顶点着色器是针对顶点缓冲区中的每个顶点执行
        //我们在手动“位置”0 定义它
        const string vertexCode = @"
            #version 330 core

            layout (location = 0) in vec3 aPosition;
            // Add a new input attribute for the texture coordinates
            layout (location = 1) in vec2 aTextureCoord;
            
            // Add an output variable to pass the texture coordinate to the fragment shader
            // This variable stores the data that we want to be received by the fragment
            out vec2 frag_texCoords;
            
            void main()
            {
                gl_Position = vec4(aPosition, 1.0);
                // Assigin the texture coordinates without any modification to be recived in the fragment
                frag_texCoords = aTextureCoord;
            }";


        //创建片段着色器
        //输出颜色为 RGBA 格式
        const string fragmentCode = @"
            #version 330 core
            
            // Receive the input from the vertex shader in an attribute
            in vec2 frag_texCoords;
            
            out vec4 out_color;

            uniform sampler2D uTexture;
            
            void main()
            {
                // This will allow us to see the texture coordinates in action!
                //out_color = vec4(frag_texCoords.x, frag_texCoords.y, 0.0, 1.0);
                out_color = texture(uTexture, frag_texCoords);
            }";
        //在片段着色器中设置采样器 uniform
        int location = _gl.GetUniformLocation(_program, "uTexture");
        _gl.Uniform1(location, 0);

        /*#version 330 core

        out vec4 out_color;

        void main()
        {
            out_color = vec4(1.0, 0.5, 0.2, 1.0);
        }*/

        //创建顶点着色器对象
        uint vertexShader = _gl.CreateShader(ShaderType.VertexShader);
        _gl.ShaderSource(vertexShader, vertexCode);
        //编译顶点着色器
        _gl.CompileShader(vertexShader);

        _gl.GetShader(vertexShader, ShaderParameterName.CompileStatus, out int vStatus);
        if (vStatus != (int)GLEnum.True)
            throw new Exception("Vertex shader failed to compile: " + _gl.GetShaderInfoLog(vertexShader));

        //创建片段着色器对象
        uint fragmentShader = _gl.CreateShader(ShaderType.FragmentShader);
        _gl.ShaderSource(fragmentShader, fragmentCode);
        //编译片段着色器
        _gl.CompileShader(fragmentShader);

        _gl.GetShader(fragmentShader, ShaderParameterName.CompileStatus, out int fStatus);
        if (fStatus != (int)GLEnum.True)
            throw new Exception("Fragment shader failed to compile: " + _gl.GetShaderInfoLog(fragmentShader));

        //创建着色器程序
        _program = _gl.CreateProgram();
        _gl.AttachShader(_program, vertexShader);
        _gl.AttachShader(_program, fragmentShader);

        _gl.LinkProgram(_program);

        _gl.GetProgram(_program, ProgramPropertyARB.LinkStatus, out int lStatus);
        if (lStatus != (int)GLEnum.True)
            throw new Exception("Program failed to link: " + _gl.GetProgramInfoLog(_program));

        //释放着色器对象
        _gl.DetachShader(_program, vertexShader);
        _gl.DetachShader(_program, fragmentShader);
        _gl.DeleteShader(vertexShader);
        _gl.DeleteShader(fragmentShader);

        //使用着色器程序
        const uint positionLoc = 0;
        _gl.EnableVertexAttribArray(positionLoc);
        //尺寸 3，因为位置是 3D 的 (x, y, z)
        //stride步长: 3 个floats 标志位置 + 2 个 floats 标志材质坐标!
        //指针从0开始，因为位置数据在数组的开头
        _gl.VertexAttribPointer(positionLoc, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), (void*)0);

        const uint texCoordLoc = 1;
        _gl.EnableVertexAttribArray(texCoordLoc);
        //尺寸 2，因为材质坐标是 2D 的 (u, v)
        //stride步长: 3 个floats 标志位置 + 2 个 floats 标志材质坐标!
        //指针从3开始，因为材质坐标在第四位开始
        _gl.VertexAttribPointer(texCoordLoc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));

        //取消绑定各种缓冲区
        //取消绑定其他缓冲区之前，必须先取消绑定顶点数组
        _gl.BindVertexArray(0);
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);

        //加载并创建材质
        _texture = _gl.GenTexture();
        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, _texture);

        // ImageResult.从内存读取.png bytes
        // Define a pointer to the image data
        ImageResult result = ImageResult.FromMemory(File.ReadAllBytes("silk.png"), ColorComponents.RedGreenBlueAlpha);
        fixed (byte* ptr = result.Data)
            _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)result.Width,
                (uint)result.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);

        //_gl.TexParameter( [Texture target] , [Parameter to change] , [New value for parameter] );
        // The suffix of TexParameter will vary depending on the type of the expected value for the parameter
        // 将 TexParameterI 替换为 TexParameter，避免使用 C# 12 的 ref readonly 参数特性。
        // Silk.NET.OpenGL 的 TexParameter 方法签名兼容 C# 10。
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        _gl.BindTexture(TextureTarget.Texture2D, 0);

        //根据某个值从可见基元中选择输出颜色。这称为混合
        _gl.Enable(EnableCap.Blend);
        //根据 alpha 值进行混合
        _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

    }


    private static void OnUpdate(double deltaTime)
    {
    }

    private static unsafe void OnRender(double deltaTime)
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit);


        //绑定一个顶点数组。您绑定的顶点数组将取决于您要绘制的内容
        _gl.BindVertexArray(_vao);
        //使用之前创建的程序对象
        _gl.UseProgram(_program);
        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, _texture);
        //绘制三角形
        _gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, (void*)0);
    }

    private static void KeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        if (key == Key.Escape)
        {
            _window.Close();
        }
        else if (key == Key.R)
        {
            
        }

    }

    

}