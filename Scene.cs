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
    public class Scene
    {
        private Assets assets;
        private List<Entity> entities;
        
        public Scene()
        {
            assets = new Assets();
            entities = new List<Entity>();
        }

        public void Spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Load(assets);
        }
        public void UpdateAll(float deltaTime)
        {
            foreach (Entity entity in entities)
            {
                entity.Update(this, deltaTime);
            }
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
