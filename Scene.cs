using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Asteroids
{
    public class Scene
    {
        private Assets assets;
        private List<Entity> entities;
        private List<Entity> spawnBuffer;
        private List<Entity> destroyBuffer;
        public bool PlayerWon =>
            spawnBuffer.Count == 0 &&
            !entities.Exists(e => e is Asteroid);
        public bool PlayerLost =>
            spawnBuffer.Count == 0 &&
            !entities.Exists(e => e is Ship);

        public Scene()
        {
            assets = new Assets();
            entities = new List<Entity>();
            spawnBuffer = new List<Entity>();
            destroyBuffer = new List<Entity>();
        }

        public void Spawn(Entity entity)
        {
            spawnBuffer.Add(entity);
            entity.Load(assets);
        }
        public void Destroy(Entity entity)
        {
            destroyBuffer.Add(entity);
        }
        public void Clear()
        {
            entities.Clear();
            spawnBuffer.Clear();
            destroyBuffer.Clear();
        }
        public void UpdateAll(float deltaTime)
        {
            foreach (Entity entity in spawnBuffer)
            {
                entities.Add(entity);
            }
            spawnBuffer.Clear();

            foreach (Entity entity in entities)
            {
                entity.Update(this, deltaTime);
            }
            for (int i = 0; i < entities.Count; i++)
            {
                Entity a = entities[i];
                for (int j = i + 1; j < entities.Count; j++)
                {
                    Entity b = entities[j];
                    float dx = a.Position.X - b.Position.X;
                    float dy = a.Position.Y - b.Position.Y;
                    float distance = MathF.Sqrt(dx * dx + dy * dy);
                    if (distance < a.Radius + b.Radius)
                    {
                        a.CollideWith(this, b);
                        b.CollideWith(this, a);
                    }
                }
            }

            foreach (Entity entity in destroyBuffer)
            {
                entities.Remove(entity);
            }
            destroyBuffer.Clear();
        }
        public void RenderAll(RenderTarget target)
        {
            foreach (Entity entity in entities)
            {
                entity.Render(target);
            }
        }
    }
}
