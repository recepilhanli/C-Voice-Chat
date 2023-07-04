using System;
using System.Buffers.Text;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Vectors
{

    public class Vector
    {
        public float x; public float y; public float z;

        public static float GetDistance(Vector v1, Vector v2)
        {
            float dist = Math.Abs((v1.x- v2.x) + (v1.y- v2.y) + (v1.z- v2.z));
            return dist;
        }


    }
}
