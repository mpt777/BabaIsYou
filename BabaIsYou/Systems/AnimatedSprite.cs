using BabaIsYou.Utils;
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
        private float _accumulator;
        private int _frameIndex;
        //private TimeSpan _frameTime = new TimeSpan(0, 0, 0, 0, 200);
        private TimeSpan _frameTime = new TimeSpan(0, 0, 0, 0, 700);

        public AnimatedSprite()
            : base(typeof(Components.Sprite))
        {
        }

        public override void Update(GameTime gameTime)
        {
            this._accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (this._accumulator >= this._frameTime.TotalSeconds)
            {
                this._accumulator -= (float)this._frameTime.TotalSeconds;
                this._frameIndex += 1;
                this.UpdateEntities();
            }
        }

        private void UpdateEntities()
        {
            foreach (var entity in m_entities.Values)
            {
                var sprite = entity.GetComponent<Components.Sprite>();
                sprite.SetFrameIndex(_frameIndex);
            }
        }
        public void ForceUpdateEntities()
        {
            this.UpdateEntities();
        }
    }
}
