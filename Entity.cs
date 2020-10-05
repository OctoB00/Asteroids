using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Asteroids
{
    public class Entity
    {
        public virtual void Load(Assets assets)
        {

        }
        public virtual void Update(Scene scene, float deltaTime)
        {

        }
        public virtual void Render(RenderTarget target)
        {

        }
        public virtual void CollideWith(Scene scene, Entity other)
        {

        }
        public virtual Vector2f Position { get; set; }
        public virtual float Radius { get; }
    }
}
