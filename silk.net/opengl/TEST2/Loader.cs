using Silk.NET.OpenGL;
using StbImageSharp;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection.Metadata;
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

        public RawModel LoadToVAO(float[] vertices, float[] colorCoordinate, uint[] indices)
        {
            uint vaoID = CreateVAO();
            BindIndicesBuffer(indices);
            StoreDataInAttributeList(0, 3, vertices);
            StoreDataInAttributeList(1, 2, colorCoordinate);

            return new RawModel(vaoID, (uint)indices.Length);
        }
         
        private uint CreateVAO()
        {
            uint vaoID = _gl.GenVertexArray();//创建
            vaos.Add(vaoID);
            _gl.BindVertexArray(vaoID);//绑定
            return vaoID;
        }

        private unsafe void StoreDataInAttributeList(uint attributeNumber, int coordinateSize, float[] data)
        {
            uint vboID = _gl.GenBuffer();//创建
            vbos.Add(vboID);
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, vboID);//绑定

            fixed (void* d = data)//存储
                _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(data.Length * sizeof(float)), d, BufferUsageARB.StaticDraw);
            
            _gl.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribPointerType.Float,
                false, (uint)coordinateSize * sizeof(float), (void*)0);//读取方式

            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);//解绑
        }

        private unsafe void BindIndicesBuffer(uint[] indices)
        {
            uint vboID = _gl.GenBuffer();
            vbos.Add(vboID);
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, vboID);

            fixed (void* i = indices)
                _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(indices.Length * sizeof(uint)), i, BufferUsageARB.StaticDraw);
        }

        public Texture LoadTexture(string filePath)
        {
            Texture texture = new(_gl, filePath);
            textures.Add(texture.handle);
            _gl.BindTexture(TextureTarget.Texture2D, 0);
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

        private void Unbind()
        {
            //取消绑定各种缓冲区
            _gl.BindVertexArray(0);
            
        }


    }
}
