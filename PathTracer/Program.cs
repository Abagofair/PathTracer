using SFML.Graphics;
using SFML.Window;
using System;
using System.Numerics;

namespace PathTracer
{
    class Program
    {
        struct Ray
        {
            public Vector3 Origin;
            public Vector3 Direction;

            public Ray(
                Vector3 origin,
                Vector3 direction)
            {
                Direction = Vector3.Normalize(direction);
                Origin = origin;
            }
        }

        private const double Radius = 0.25;
        private static Vector3 Center = new Vector3(0.0f, 0.0f, 2.0f);

        private const int Width = 800;
        private const int Height = 600;

        static void Main(string[] args)
        {
            var center = CalculateNDC(400, 300);
            Center = new Vector3((float)center.Item1, (float)center.Item2, 1.0f);

            RenderWindow window = new RenderWindow(new VideoMode(Width, Height), "PathTracer");

            Texture texture = new Texture(Width, Height);
            Image image = new Image(Width, Height);
            Sprite sprite = new Sprite();
            texture.Update(image);
            sprite.Texture = texture;

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(Color.Black);

                for (int y = 1; y <= Height; ++y)
                {
                    for (int x = 1; x <= Width; ++x)
                    {
                        var ndc = CalculateNDC(x, y);
                        var ray = CreateRay(ndc.Item1, ndc.Item2);

                        Console.WriteLine($"{ndc.Item1}, {ndc.Item2}");
                        Console.WriteLine(ray.Direction);

                        if (RaySphereAnalyticalIntersect(ref ray))
                        {
                            image.SetPixel((uint)x, (uint)y, Color.Red);
                        }

                        texture.Update(image);
                        window.Draw(sprite);
                        window.Display();
                    }
                }
            }
        }

        static bool RaySphereAnalyticalIntersect(ref Ray ray)
        {
            float a = 1.0f;
            float b = Vector3.Dot(ray.Direction * 2, ray.Origin - Center);
            float c = Math.Abs((ray.Origin - Center).LengthSquared()) - (float)(Radius * Radius);

            return (b * b - 4 * a * c) >= 0;
        }

        static (double, double) CalculateNDC(
            int y,
            int x)
        {
            double NDC_X = (x + 0.5) / Width;
            double NDC_Y = (y + 0.5) / Width;
            return (NDC_X, NDC_Y);
        }

        static Ray CreateRay(
            double x,
            double y)
        {
            return new Ray(
                new Vector3((float)x, (float)y, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f));
        }
    }
}
