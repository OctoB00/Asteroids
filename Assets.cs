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
    public class Assets
    {
        private Dictionary<string, Texture> textures;
        
        public Assets()
        {
            textures = new Dictionary<string, Texture>();
        }
        public Texture GetTexture(string name)
        {
            foreach (KeyValuePair<string, Texture> pair in textures)
            {
                if (name == pair.Key)
                {
                    return pair.Value;
                }
            }
            Texture texture = new Texture(name);
            textures.Add(name, texture);
            return texture;
        }
    }
}
