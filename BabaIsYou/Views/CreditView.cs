using BabaIsYou.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using BabaIsYou;

namespace BabaIsYou.Views
{
    public class CreditView : GameView
    {
        public List<TextDisplay> textDisplays = new List<TextDisplay>();
        private Texture2D _background;
        public CreditView(Game1 game) : base(game)
        {
            this.textDisplays.Add(new TextDisplay(this.game, "-- Credits --", new Vector2(this.dimensions.X / 2, 100), Color.White));
            this.textDisplays.Add(new TextDisplay(this.game, "Created by: Marshal Taylor", new Vector2(this.dimensions.X / 2, 200), Color.White));
            for (int i = 0; i < this.textDisplays.Count; i++)
            {
                this.textDisplays[i].Center();
            }
            //this.LoadContent();
        }
        protected override void LoadContent()
        {
            //this._background = this.game.Content.Load<Texture2D>("Images/sunset");
        }

        public override void Update(GameTime gameTime)
        {
            if (this.game.keyboard.JustPressed(Keys.Escape))
            {
                this.game.RemoveFrame();
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(this._background, new Rectangle(0, 0, (int)this.dimensions.X, (int)this.dimensions.Y), Color.White);
            for (int i = 0; i < this.textDisplays.Count; i++)
            {
                this.textDisplays[i].Draw(spriteBatch);
            }
        }
    }
}
