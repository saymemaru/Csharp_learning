
using System.Numerics;

namespace TEST1.Tool
{
    internal class MyMath
    {
        public static Matrix4x4 CreateTransformationMatrix(Vector3 translation, float rx, float ry, float rz, float scale)
        {
            Matrix4x4 transformationMatrix =
                Matrix4x4.CreateScale(scale) *
                Matrix4x4.CreateRotationX(rx * (MathF.PI / 180.0f)) *
                Matrix4x4.CreateRotationY(ry * (MathF.PI / 180.0f)) *
                Matrix4x4.CreateRotationZ(rz * (MathF.PI / 180.0f)) *
                Matrix4x4.CreateTranslation(translation);
            ;
            return transformationMatrix;
        }
    }
}
