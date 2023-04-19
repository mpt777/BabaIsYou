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
    public class LevelSelectView : GameView
    {
        public List<TextDisplay> textDisplays = new List<TextDisplay>();
        public LevelSelectView(Game1 game, List<String> levelNames) : base(game)
        {
            this.textDisplays.Add(new TextDisplay(this.game, "Levels", new Vector2(this.dimensions.X / 2, 100), Color.White));

            for(int i=0; i<levelNames.Count; i++)
            {
                this.textDisplays.Add(new TextDisplay(this.game, levelNames[i], new Vector2(this.dimensions.X / 2, 200 + (50 * i)), "arial", Color.White, Color.Orange));
            }
            
            for (int i = 0; i < this.textDisplays.Count; i++)
            {
                this.textDisplays[i].Center();
            }
            //this.LoadContent();
        }
        public void StartGame(String levelName)
        {
            this.game.framesToAdd.Push(new GameLevel(this.game, levelName));
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

            for (int i = 1; i < this.textDisplays.Count; i++)
            {
                if (this.game.keyboard.IsOver(this.textDisplays[i].bounds))
                {
                    if (this.game.keyboard.JustLeftMouseDown())
                    {
                        this.StartGame(this.textDisplays[i].GetString());
                        this.active = false;
                    }
                }
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
