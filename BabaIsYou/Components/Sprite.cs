using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Components
{
    public class Sprite : Component
    {
        public Texture2D sprite;
        public Color fill;
        public Color stroke;

        public Sprite(Texture2D sprite, Color fill)
        {
            this.sprite = sprite;
            this.fill = fill;
        }
    }
}
