using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topia;

namespace Topia
{
    public class Entity
    {
        public Utils.Int2 Position;
        public Utils.Int2 Velocity;
        public Utils.Int4 Hitbox;
        public Utils.Int4 Frame;
        public Texture2D Texture;
        public Color color;

        public virtual void Draw(SpriteBatch sb) { }
        public virtual void Update() { }
    }
}
