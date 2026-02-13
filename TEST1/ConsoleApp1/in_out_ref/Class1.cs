using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace in_out_ref
{
    internal class Class1
    {
        public static void ExchangeInt(ref int a, ref int b) 
        {
            int c = a;
            a = b;
            b = c;
        } 

        public static void RectangleAreaAndPerimeter(float x, float y, out float Area, out float Perimeter)
        {
            Area = x * y;
            Perimeter = (x + y) * 2;
        }
    }
}
