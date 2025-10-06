using Silk.NET.OpenGL;
using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEST1.Tool;

namespace TEST1
{
    internal class StaticShaderTexture : Shader
    {
        private static readonly string VERTEX_FILE = "Shader/shader.vert";
        private static readonly string FRAGMENT_FILE = "Shader/shader.frag";
        private int location_transformationMatrix;

        public StaticShaderTexture(GL gl) : base(gl, VERTEX_FILE, FRAGMENT_FILE)
        {
            //LoadTransformationMatrix(MyMath.CreateTransformationMatrix(new Vector3(0f,0f,0f),60,0,0,0.9f));
        }

        protected override void GetAllUniformLocations()
        {
            location_transformationMatrix = GetUniformLocation("transformationMatrix");   
        }

        public void LoadTransformationMatrix(Matrix4x4 matrix)
        {
            //加载矩阵
            SetUniform(location_transformationMatrix, matrix);

        }
            

        protected override void BindAttributes()
        {
            BindAttribute(0, "position");
            BindAttribute(1, "textureCoords");
        }
    }
}
