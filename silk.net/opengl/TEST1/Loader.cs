using Silk.NET.OpenGL;
using System;
using TEST1.Model;

namespace TEST1
{
    public class Loader : IDisposable
    {
        private readonly GL _gl;
        private List<uint> vaos = new List<uint>();
        private List<uint> vbos = new List<uint>();
        private List<uint> textures = new List<uint>();

        public Loader(GL gl)
        {
            _gl = gl;
        }

        public RawModel LoadToVAO(float[] vertices, uint[] indices)
        {

            BufferObject<uint>  Ebo = new BufferObject<uint>(_gl, indices, BufferTargetARB.ElementArrayBuffer);
            BufferObject<float> Vbo = new BufferObject<float>(_gl, vertices, BufferTargetARB.ArrayBuffer);
            SetVertexAttribPointer(0,3);
            VertexArrayObject<float, uint> Vao = new VertexArrayObject<float, uint>(_gl, Vbo, Ebo);
            vaos.Add(Vao.handle);
            vbos.Add(Ebo.handle);
            Unbind();
            Ebo.Dispose();
            Vbo.Dispose();

            return new RawModel(Vao.handle, (uint)indices.Length);
        }
         
        public Texture LoadTexture(string filePath)
        {
            Texture texture = new Texture(_gl, filePath);
            textures.Add(texture.handle);
            return texture;
        }

        public void Dispose()
        {
            foreach (uint vao in vaos)
            {
                _gl.DeleteVertexArray(vao);
            }
            foreach (uint vbo in vbos)
            {
                _gl.DeleteBuffer(vbo);
            }
            foreach (uint texture in textures)
            {
                _gl.DeleteTexture(texture);
            }
            vaos.Clear();
            vbos.Clear();
            textures.Clear();
        }

        
        private unsafe void SetVertexAttribPointer(uint attributeNumber, int coordinateSize)
        {
            _gl.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribPointerType.Float,
                false, (uint)coordinateSize * sizeof(float), (void*)0);
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        }

        private void Unbind()
        {
            //取消绑定各种缓冲区
            _gl.BindVertexArray(0);
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
        }


    }
}
