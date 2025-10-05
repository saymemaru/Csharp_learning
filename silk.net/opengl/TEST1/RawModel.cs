using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST1
{
    public class RawModel
    {
        public uint vaoID { get;private set; }
        public uint vertexCount { get; private set; }
        public RawModel(uint vaoID, uint vertexCount)
        {
            this.vaoID = vaoID;
            this.vertexCount = vertexCount;
        }
    }
}
