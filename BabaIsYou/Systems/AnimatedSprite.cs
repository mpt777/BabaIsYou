using Breakout;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Systems
{
    class AnimatedSprite : System
    {
        public AnimatedSprite()
            : base(typeof(Components.Sprite))
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in m_entities.Values)
            {
                var sprite = entity.GetComponent<Components.Sprite>();
                sprite.Update(gameTime);
            }
        }
    }
}
