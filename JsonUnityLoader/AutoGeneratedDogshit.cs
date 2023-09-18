using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace JsonUnityLoader
{
    public class Finger
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
    public class Palm
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }
    }

    public class Auto
    {
        public Finger? Thumb { get; set; }
        public Finger? Pointer { get; set; }
        public Finger? Mi9dddle { get; set; }
        public Finger? Ring { get; set; }
        public Finger? Pinky { get; set; }
        public Palm? Palm { get; set; }
        public Vector3[] Positions { get; private set; }
        public Quaternion PalmRot { get; private set; }
        public void GenerateHandles()
        {
            Positions = new Vector3[]{
            new(Thumb.X, Thumb.Y, Thumb.Z),
            new(Pointer.X, Pointer.Y, Pointer.Z),
            new(Mi9dddle.X, Mi9dddle.Y, Mi9dddle.Z),
            new(Ring.X, Ring.Y, Ring.Z),
            new(Pinky.X, Pinky.Y, Pinky.Z),
        };
            PalmRot = new(Palm.X, Palm.Y, Palm.Z, Palm.W);
        }
    }

}
