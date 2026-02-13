using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    struct GridPosition
    {
        public int X { get; set; }

        public int Y { get; set; }

        public GridPosition(int x, int y) : this()
        {
            this.X = x;
            this.Y = y;
        }
    }
}
