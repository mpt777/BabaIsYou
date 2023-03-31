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
        private float _layerDepth = 1f;

        private Rectangle _subframe = new Rectangle(0, 0, 0, 0);

        public Sprite(Texture2D sprite, Color fill)
        {
            this.Initialize(sprite, fill, 1, 0.5f);
        }
        public Sprite(Texture2D sprite, Color fill, int frameCount)
        {
            this.Initialize(sprite, fill, frameCount, 0.5f);
        }
        public Sprite(Texture2D sprite, Color fill, int frameCount, float layerDepth)
        {
            this.Initialize(sprite, fill, frameCount, layerDepth);
        }

        public void Initialize(Texture2D sprite, Color fill, int frameCount, float layerDepth)
        {
            this.sprite = sprite;
            this.fill = fill;
            this.frameCount = frameCount;
            this._layerDepth = layerDepth;
            this.SetSubFrame();
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
        public float LayerDepth()
        {
            return this._layerDepth;
        }
    }
}
