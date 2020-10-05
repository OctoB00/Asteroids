using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Graphics;
using SFML.Window;


namespace Asteroids
{
    public class Ship : Entity
    {
        private readonly Sprite sprite;
        private const float Torque = 300;
        private const float Acceleration = 200;
        private const float ReloadTime = 0.01f;
        private const float BulletSpeed = 400;
        private const float ImmortalTime = 1.5f;
        private Clock immortalTimer = new Clock();
        private int health = 3;
        private bool Immortal => immortalTimer.ElapsedTime.AsSeconds() < ImmortalTime;

        public Ship()
        {
            sprite = new Sprite();
        }
        public override void Load(Assets assets)
        {
            sprite.Texture = assets.GetTexture("ship.png");
            sprite.Origin = (Vector2f)sprite.Texture.Size * 0.5f;
        }
        public Vector2f Velocity
        {
            get;
            set;
        }
        public override Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }
        public override float Radius => Math.Max(sprite.Origin.X, sprite.Origin.Y);
        public Vector2f Direction => new Vector2f(
            MathF.Cos(sprite.Rotation / 180.0f * MathF.PI),
            MathF.Sin(sprite.Rotation / 180.0f * MathF.PI)
        );

        public void SpinLeft(float deltaTime)
        {
            sprite.Rotation -= Torque * deltaTime;
        }
        public void SpinRight(float deltaTime)
        {
            sprite.Rotation += Torque * deltaTime;
        }
        public void Thrust(float deltaTime)
        {
            Velocity += Direction * (Acceleration * deltaTime);
        }
        public void Brake(float deltaTime)
        {
            float factor = 1.0f - deltaTime;
            if (factor > 0.0f && factor < 1.0f)
            {
                Velocity *= factor;
            }
        }
        private Clock shootTimer = new Clock();
        public void Shoot(Scene scene)
        {
            if (shootTimer.ElapsedTime.AsSeconds() > ReloadTime)
            {
                shootTimer.Restart();
                scene.Spawn(new Bullet()
                {
                    Position = this.Position,
                    Rotation = sprite.Rotation,
                    Velocity = Direction * BulletSpeed
                });
            }
        }
        public override void CollideWith(Scene scene, Entity other)
        {
            if (!Immortal && other is Asteroid ast)
            {
                immortalTimer.Restart();
                scene.Destroy(ast);
                health--;
            }
        }

        public override void Update(Scene scene, float deltaTime)
        {
            Vector2f newPos = Position + Velocity * deltaTime;

            if (newPos.X < -Radius) newPos.X = Program.ScreenW + Radius;
            if (newPos.Y < -Radius) newPos.Y = Program.ScreenH + Radius;
            if (newPos.X > Program.ScreenW + Radius) newPos.X = -Radius;
            if (newPos.Y > Program.ScreenH + Radius) newPos.Y = -Radius;

            Position = newPos;

            if (health <= 0)
            {
                scene.Destroy(this);
            }
        }
        public override void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }
}
