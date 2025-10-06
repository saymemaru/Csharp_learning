using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST1.Model
{
    public class TexturedModel
    {
        public RawModel RawModel { get; private set; }
        public Texture Texture { get; private set; }
        public TexturedModel(RawModel rawModel, Texture texture)
        {
            this.RawModel = rawModel;
            this.Texture = texture;
        }
    }
}
