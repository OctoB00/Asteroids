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
            sprite.Origin = (Vector2f) sprite.Texture.Size * 0.5f;
        }
        public Vector2f Velocity
        {
            get;
            set;
        }
        public Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }
        public float Radius => Math.Max(sprite.Origin.X, sprite.Origin.Y);

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
