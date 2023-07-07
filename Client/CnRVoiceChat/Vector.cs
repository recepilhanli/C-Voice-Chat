using System;
using System.Buffers.Text;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public class FVector
    {
        public float x = 0;
        public float y = 0;
        public float z = 0;
    }

    public class Vector
    {
        public float x; public float y; public float z;

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static float GetDistance(Vector v1, Vector v2)
        {
            float dist = Math.Abs((v1.x - v2.x) + (v1.y - v2.y) + (v1.z - v2.z));
            return dist;
        }

        public static Vector Serialize(FVector v)
        {
            Vector s = new Vector(v.x, v.y, v.z);
            return s;
        }


    }
}
