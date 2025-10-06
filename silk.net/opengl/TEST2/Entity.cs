using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TEST1.Model;

namespace TEST1
{
    public class Entity
    {
        public TexturedModel Model { get;  set; }
        public Vector3 Position { get;  set; }
        public float RotX { get;  set; }
        public float RotY { get;  set; } 
        public float RotZ { get;  set; }
        public float Scale { get;  set; }
        public Entity(TexturedModel model, Vector3 position, float rotX, float rotY, float rotZ, float scale)
        {
            this.Model = model;
            this.Position = position;
            this.RotX = rotX;
            this.RotY = rotY;
            this.RotZ = rotZ;
            this.Scale = scale;
        }

        public void ChangePosition(float dx, float dy, float dz)
        {
            this.Position = new Vector3(this.Position.X + dx, this.Position.Y + dy, this.Position.Z + dz);
        }
        public void ChangeRotation(float dx, float dy, float dz)
        {
            this.RotX += dx;
            this.RotY += dy;
            this.RotZ += dz;
        }
    }
}
