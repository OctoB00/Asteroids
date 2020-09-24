using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Asteroids
{
    class Program
    {
        public const int ScreenW = 800;
        public const int ScreenH = 600;
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(ScreenW, ScreenH), "Asteroids"))
            {
                window.Closed += (s, e) => window.Close();
                Clock clock = new Clock();
                Scene scene = new Scene();
                scene.Spawn(new Asteroid(3)
                {
                    Position = new Vector2f(ScreenW, ScreenH) * 0.5f,
                    Velocity = new Vector2f(200, 100)
                });

                while (window.IsOpen)
                {
                    window.DispatchEvents();
                    window.Clear(Color.Black);

                    float deltaTime = clock.Restart().AsSeconds();
                    scene.UpdateAll(deltaTime);
                    scene.RenderAll(window);


                    window.Display();
                }
            }
        }
    }
}
