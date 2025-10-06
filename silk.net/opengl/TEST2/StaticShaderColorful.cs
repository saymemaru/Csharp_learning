using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.OpenGL;

namespace TEST1
{
    internal class StaticShaderColorful : Shader
    {
        private static readonly string  VERTEX_FILE = "Shader/shader1.vert";  
        private static readonly string FRAGMENT_FILE = "Shader/shader1.frag";

        public StaticShaderColorful(GL gl) : base(gl, VERTEX_FILE, FRAGMENT_FILE)
        {
            
        }

        protected override void GetAllUniformLocations()
        {

        }
        protected override void BindAttributes()
        {
            BindAttribute(0, "position");
        }
    }
}
