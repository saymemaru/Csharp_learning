using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.OpenGL;
using System.Drawing;
using TEST1.Model;

namespace TEST1
{
    public class Renderer
    {
        private readonly GL _gl;
        public Renderer(GL gl)
        {
            this._gl = gl;
        }
        public void Prepare()
        {
            _gl.Clear(ClearBufferMask.ColorBufferBit);
        }
        public unsafe void Render(TexturedModel texturedModel)
        {
            RawModel rawModel = texturedModel.RawModel;
            Texture texture = texturedModel.Texture;
            _gl.BindVertexArray(rawModel.handle);
            _gl.EnableVertexAttribArray(0);//启用顶点坐标属性
            _gl.EnableVertexAttribArray(1);//启用纹理坐标属性
            texture.Bind();//绑定纹理
            _gl.DrawElements(PrimitiveType.Triangles, rawModel.vertexCount, DrawElementsType.UnsignedInt, (void*)0);
            
            //渲染完成
            _gl.DisableVertexAttribArray(0);//禁用顶点坐标属性
            _gl.DisableVertexAttribArray(1);
            _gl.BindVertexArray(0);//解绑
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
        }
    }
}
