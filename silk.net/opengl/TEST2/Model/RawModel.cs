using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST1.Model
{
    public class RawModel
    {
        public uint handle { get;private set; }
        public uint vertexCount { get; private set; }
        public RawModel(uint vaoID, uint vertexCount)
        {
            this.handle = vaoID;
            this.vertexCount = vertexCount;
        }
    }
}
