using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Tiles
{
    public interface ITile
    {
        Vector2 _postion { get; set; }
        virtual protected void LoadContent() { }
        virtual protected void ProcessInput() { }
        public virtual void Update(GameTime gameTime) { }
        virtual public void Draw(SpriteBatch spriteBatch) { }
    }
}
