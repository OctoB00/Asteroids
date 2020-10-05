using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Asteroids
{
    public class Bullet : Entity
    {
        private readonly Sprite sprite;

        public Bullet()
        {
            sprite = new Sprite();
        }
        public override Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }
        public Vector2f Velocity
        {
            get;
            set;
        }
        public override float Radius => MathF.Max(sprite.Origin.X, sprite.Origin.Y);
        public float Rotation
        {
            get => sprite.Rotation;
            set => sprite.Rotation = value;
        }

        public override void Load(Assets assets)
        {
            sprite.Texture = assets.GetTexture("bullet.png");
            sprite.Origin = (Vector2f)sprite.Texture.Size * 0.5f;
        }
        public override void Update(Scene scene, float deltaTime)
        {
            Position += Velocity * deltaTime;
            if (Position.X < -Radius || Position.X > Program.ScreenW + Radius
            ||  Position.Y < -Radius || Position.Y > Program.ScreenH + Radius)
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
