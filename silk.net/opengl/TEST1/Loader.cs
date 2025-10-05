using Silk.NET.OpenGL;
using System;

namespace TEST1
{
    public class Loader
    {
        private readonly GL _gl;
        private List<uint> vaos = new List<uint>();
        private List<uint> vbos = new List<uint>();
        private static BufferObject<float> Vbo;
        private static BufferObject<uint> Ebo;
        private static VertexArrayObject<float, uint> Vao;

        public Loader(GL gl)
        {
            _gl = gl;
        }

        public RawModel LoadToVAO(float[] vertices, uint[] indices)
        {

            Ebo = new BufferObject<uint>(_gl, indices, BufferTargetARB.ElementArrayBuffer);
            Vbo = new BufferObject<float>(_gl, vertices, BufferTargetARB.ArrayBuffer);
            Vao = new VertexArrayObject<float, uint>(_gl, Vbo, Ebo);
            vaos.Add(Vao.handle);
            vbos.Add(Vbo.handle);

            StoreDataInAttributeList(0);

            return new RawModel(Vao.handle, (uint)indices.Length);
        }

        public void CleanUp()
        {
            foreach (uint vao in vaos)
            {
                _gl.DeleteVertexArray(vao);
            }
            foreach (uint vbo in vbos)
            {
                _gl.DeleteBuffer(vbo);
            }
            vaos.Clear();
            vbos.Clear();
        }

        
        private unsafe void StoreDataInAttributeList(uint attributeNumber)
        {

            _gl.VertexAttribPointer(attributeNumber, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), (void*)0);

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
