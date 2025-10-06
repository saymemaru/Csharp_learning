using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST1
{
    internal class StaticShaderOrange : Shader
    {
        private static readonly string VERTEX_FILE = "Shader/shader2.vert";
        private static readonly string FRAGMENT_FILE = "Shader/shader2.frag";

        public StaticShaderOrange(GL gl) : base(gl, VERTEX_FILE, FRAGMENT_FILE)
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
