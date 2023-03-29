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
        public int frameCount;
        private int _frameIndex;
        private float _accumulator;
        private TimeSpan _frameTime;

        private Rectangle _subframe = new Rectangle(0, 0, 0, 0);

        public Sprite(Texture2D sprite, Color fill)
        {
            this.Initialize(sprite, fill, 1, new TimeSpan(0, 0, 0, 0, 200));
        }
        public Sprite(Texture2D sprite, Color fill, int frameCount)
        {
            this.Initialize(sprite, fill, frameCount, new TimeSpan(0, 0, 0, 0, 200));
        }
        public Sprite(Texture2D sprite, Color fill, int frameCount, TimeSpan frameTime)
        {
            this.Initialize(sprite, fill, frameCount, frameTime);
        }

        public void Initialize(Texture2D sprite, Color fill, int frameCount, TimeSpan frameTime)
        {
            this.sprite = sprite;
            this.fill = fill;
            this.frameCount = frameCount;
            this._frameTime = frameTime;
            this.SetSubFrame();
        }

        public void Update(GameTime gameTime) 
        {
            if (this.frameCount == 1) { return; }
            this._accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (this._accumulator >= this._frameTime.TotalSeconds)
            {
                this._accumulator -= (float)this._frameTime.TotalSeconds;
                this.UpdateFrame();
            }
        }

        private void SetSubFrame()
        {
            int frameWidth = this.sprite.Bounds.Width / this.frameCount;
            int frameHeight = this.sprite.Bounds.Height;
            this._subframe = new Rectangle(frameWidth * this._frameIndex, this.sprite.Bounds.Y, frameHeight, frameWidth);
        }

        public void UpdateFrame() 
        {
            this._frameIndex = (this._frameIndex + 1) % this.frameCount;
            this.SetSubFrame();
        }

        public Rectangle Bounds()
        {
            return this._subframe;
        }
    }
}
