using System;
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
                bool restart = true;

                Ship ship = null;

                bool spinLeft = false,
                    spinRight = false,
                    thrust = false,
                    brake = false,
                    shoot = false;
                window.KeyPressed += (s, e) =>
                {
                    switch(e.Code)
                    {
                        case Keyboard.Key.Left: spinLeft = true; break;
                        case Keyboard.Key.Right: spinRight = true; break;
                        case Keyboard.Key.Up: thrust = true; break;
                        case Keyboard.Key.Down: brake = true; break;
                        case Keyboard.Key.Space: shoot = true; break;
                    }
                };
                window.KeyReleased += (s, e) =>
                {
                    switch (e.Code)
                    {
                        case Keyboard.Key.Left: spinLeft = false; break;
                        case Keyboard.Key.Right: spinRight = false; break;
                        case Keyboard.Key.Up: thrust = false; break;
                        case Keyboard.Key.Down: brake = false; break;
                        case Keyboard.Key.Space: shoot = false; break;
                    }
                };

                while (window.IsOpen)
                {
                    window.DispatchEvents();
                    window.Clear(Color.Black);
                    float deltaTime = clock.Restart().AsSeconds();
                    if (restart)
                    {
                        scene.Clear();
                        for (int i = 0; i < 5; i++)
                        {
                            scene.Spawn(new Asteroid(3)
                            {
                                Position = new Vector2f(ScreenW, ScreenH) * 0.5f + VectorMath.Random() * ScreenH,
                                Velocity = VectorMath.Random() * 100.0f
                            });
                        }
                        ship = new Ship()
                        {
                            Position = new Vector2f(ScreenW, ScreenH) * 0.5f
                        };
                        scene.Spawn(ship);
                        restart = false;
                    }
                    else
                    {
                        if (spinLeft)
                        {
                            ship.SpinLeft(deltaTime);
                        }
                        if (spinRight)
                        {
                            ship.SpinRight(deltaTime);
                        }
                        if (thrust)
                        {
                            ship.Thrust(deltaTime);
                        }
                        if (brake)
                        {
                            ship.Brake(deltaTime);
                        }
                        if (shoot)
                        {
                            ship.Shoot(scene);
                        }
                        scene.UpdateAll(deltaTime);
                        scene.RenderAll(window);
                        restart = scene.PlayerWon || scene.PlayerLost;
                    }
                    window.Display();
                }
            }
        }
    }
}
