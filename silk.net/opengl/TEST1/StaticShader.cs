using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.OpenGL;

namespace TEST1
{
    internal class StaticShader : Shader
    {
        private static readonly string  VERTEX_FILE = "Shader/shader1.vert";  
        private static readonly string FRAGMENT_FILE = "Shader/shader1.frag";

        public StaticShader(GL gl) : base(gl, VERTEX_FILE, FRAGMENT_FILE)
        {
            
        }
    }
}
