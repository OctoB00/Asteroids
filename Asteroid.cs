using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Asteroids
{
    public class Asteroid : Entity
    {
        private readonly Sprite sprite;
        private readonly int size;

        public Asteroid(int size)
        {
            sprite = new Sprite();
            this.size = size;
        }
        public override void Load(Assets assets)
        {
            sprite.Texture = assets.GetTexture($"asteroid_{size}.png");
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
        public override float Radius => MathF.Max(sprite.Origin.X, sprite.Origin.Y);
        public override void CollideWith(Scene scene, Entity other)
        {
            if (other is Asteroid ast)
            {
                Vector2f d = this.Position - ast.Position;
                Vector2f dir = VectorMath.Normalize(d);
                Velocity = VectorMath.Reflect(-Velocity, dir);
                Position = ast.Position + dir * (this.Radius + ast.Radius);
            }
            else if (other is Bullet bul)
            {
                if (size > 1)
                {
                    const int children = 3;
                    float speed = 1.3f * VectorMath.LengthOf(Velocity);
                    for (int i = 0; i < children; i++)
                    {
                        float angle = i * (MathF.PI * 2) / children;
                        Vector2f dir = VectorMath.FromAngle(angle);
                        scene.Spawn(new Asteroid(size - 1)
                        {
                            Position = Position + dir * 0.5f * Radius,
                            Velocity = dir * speed
                        });
                    }
                }
                scene.Destroy(this); scene.Destroy(bul);
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
        }
        public override void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }
}
