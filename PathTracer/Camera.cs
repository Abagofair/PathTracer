using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PathTracer
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Up { get; set; }
        public Vector3 Right { get; set; }
        public Vector3 Front { get; set; }

        public Camera()
        {
            Position = new Vector3(0.0f, 0.0f, 0.0f);

            //Can be done with Gram–Schmidt process
            Up = new Vector3(0.0f, 1.0f, 0.0f);
            Right = new Vector3(1.0f, 0.0f, 0.0f);
            Front = new Vector3(0.0f, 0.0f, -1.0f);
        }
    }
}
