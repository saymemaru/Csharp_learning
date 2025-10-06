using Silk.NET.OpenGL;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TEST1
{
    public abstract class Shader : IDisposable
    {
        //Our handle and the GL instance this class will use, these are private because they have no reason to be public.
        //Most of the time you would want to abstract items to make things like this invisible.
        private uint _handle;
        private GL _gl;

        public Shader(GL gl, string vertexPath, string fragmentPath)
        {
            _gl = gl;

            //Load the individual shaders.
            uint vertex = LoadShader(ShaderType.VertexShader, vertexPath);
            uint fragment = LoadShader(ShaderType.FragmentShader, fragmentPath);
            //Create the shader program.
            _handle = _gl.CreateProgram();
            //Attach the individual shaders.
            _gl.AttachShader(_handle, vertex);
            _gl.AttachShader(_handle, fragment);
            BindAttributes();
            _gl.LinkProgram(_handle);
            //Check for linking errors.
            _gl.GetProgram(_handle, GLEnum.LinkStatus, out var status);
            if (status == 0)
            {
                throw new Exception($"Program failed to link with error: {_gl.GetProgramInfoLog(_handle)}");
            }
            GetAllUniformLocations();
            Console.WriteLine($"Shader loaded: {_handle}");
            //Detach and delete the shaders
            _gl.DetachShader(_handle, vertex);
            _gl.DetachShader(_handle, fragment);
            _gl.DeleteShader(vertex);
            _gl.DeleteShader(fragment);
            
        }

        protected abstract void BindAttributes();

        protected void BindAttribute(uint index, string name)
        {
            //Binding an attribute to a specific index.
            _gl.BindAttribLocation(_handle, index, name);
        }

        public void Start()
        {
            //Using the program
            _gl.UseProgram(_handle);
        }

        public void Stop()
        {
            //Stopping the use of the program
            _gl.UseProgram(0);
        }

        protected abstract void GetAllUniformLocations();

        public int GetUniformLocation(string name)
        {
            //Getting the location of a uniform using its name.
            int location = _gl.GetUniformLocation(_handle, name);
            if (location == -1) //If GetUniformLocation returns -1 the uniform is not found.
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            return location;
        }

        //Uniforms are properties that applies to the entire geometry
        public void SetUniform(int location, int value)
        {
            _gl.Uniform1(location, value);
        }
  
        public void SetUniform(int location, float value)
        {
            _gl.Uniform1(location, value);
        }

        public void SetUniform(int location, Vector3 value)
        {
            _gl.Uniform3(location, value);
        }

        public void SetUniform(int location, bool value)
        {
            if (value)
                _gl.Uniform1(location, 1);
            else
                _gl.Uniform1(location, 0);
        }

        public void SetUniform(int location, Matrix4x4 value)
        {
            // Create a Span<Matrix4x4> that points to our single matrix.
            var matrixSpan = MemoryMarshal.CreateSpan(ref value, 1);
            // Cast the Span<Matrix4x4> to a Span<float> without copying data.
            // This is a safe and efficient way to reinterpret the memory.
            var floatSpan = MemoryMarshal.Cast<Matrix4x4, float>(matrixSpan);
            _gl.UniformMatrix4(location, 1, false, floatSpan);
        }

        public void Dispose()
        {
            //Remember to delete the program when we are done.
            _gl.DeleteProgram(_handle);
        }

        private uint LoadShader(ShaderType type, string path)
        {
            //To load a single shader we need to:
            //1) Load the shader from a file.
            //2) Create the handle.
            //3) Upload the source to opengl.
            //4) Compile the shader.
            //5) Check for errors.
            string src = File.ReadAllText(path);
            uint handle = _gl.CreateShader(type);
            _gl.ShaderSource(handle, src);
            _gl.CompileShader(handle);
            string infoLog = _gl.GetShaderInfoLog(handle);
            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                throw new Exception($"Error compiling shader of type {type}, failed with error {infoLog}");
            }

            return handle;
        }
    }
}
