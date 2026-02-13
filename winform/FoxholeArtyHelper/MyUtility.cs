using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;


namespace FoxholeArtyHelper
{
    public static class MyUtility
    {
        public static float GetFullAngleBetweenVectors(Vector2 vector1, Vector2 vector2)
        {
            // 计算点积
            double dot = vector1.X * vector2.X + vector1.Y * vector2.Y;

            // 计算叉积（确定方向）
            double cross = vector1.X * vector2.Y - vector1.Y * vector2.X;

            // 计算弧度
            double angleRad = Math.Atan2(cross, dot);

            // 转换为度，并确保在 0-360 范围内
            float angleDeg = (float)(angleRad * (180 / Math.PI));

            if (angleDeg < 0)
                angleDeg += 360;

            return angleDeg;
        }
    }
}
