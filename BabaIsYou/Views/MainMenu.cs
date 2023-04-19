using BabaIsYou.Views;
using BabaIsYou.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Views
{
    public class MainMenu : GameView
    {
        private TextDisplay startGame;
        private TextDisplay viewCredit;
        private TextDisplay viewControls;
        private TextDisplay title;
        private TextDisplay quit;
        public MainMenu(Game1 game) : base(game)
        {
            this.Initialize();
        }
        public MainMenu(Game1 game, int width, int height) : base(game, width, height)
        {
            this.Initialize();
        }
        public MainMenu(Game1 game, int width, int height, int posX, int posY) : base(game, width, height, posX, posY)
        {
            this.Initialize();
        }
        protected override void Initialize()
        {
            this.title = new TextDisplay(this.game, "Baba Is You", new Vector2(this.dimensions.X / 2, 100));
            this.startGame = new TextDisplay(this.game, "New Game", new Vector2(this.dimensions.X / 2, 250), "arial", Color.White, Color.Orange);
            this.viewControls = new TextDisplay(this.game, "Controls", new Vector2(this.dimensions.X / 2, 300), "arial", Color.White, Color.Orange);
            this.viewCredit = new TextDisplay(this.game, "Credits", new Vector2(this.dimensions.X / 2, 350), "arial", Color.White, Color.Orange);
            this.quit = new TextDisplay(this.game, "Quit", new Vector2(this.dimensions.X / 2, 450), "arial", Color.White, Color.Orange);

            this.title.Center();
            this.startGame.Center();
            this.viewControls.Center();
            this.viewCredit.Center();
            this.quit.Center();
        }
        public override void Update(GameTime gameTime)
        {
            if (this.game.keyboard.JustPressed(Keys.Escape))
            {
                this.game.Exit();
            }

            if (this.game.keyboard.JustLeftMouseDown())
            {
                if (this.game.keyboard.IsOver(this.startGame.bounds))
                {
                    this.game.AddFrame(new LevelSelectView(this.game, this.game.levelReader.ReadLevelSelect()));
                    this.active = false;
                }
                if (this.game.keyboard.IsOver(this.viewControls.bounds))
                {
                    this.game.AddFrame(new ControlView(this.game));
                    this.active = false;
                }
                if (this.game.keyboard.IsOver(this.viewCredit.bounds))
                {
                    this.game.AddFrame(new CreditView(this.game));
                    this.active = false;
                }
                if (this.game.keyboard.IsOver(this.quit.bounds))
                {
                    this.game.Exit();
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            this.title.Draw(spriteBatch);
            this.startGame.Draw(spriteBatch);
            this.viewControls.Draw(spriteBatch);
            this.viewCredit.Draw(spriteBatch);
            this.quit.Draw(spriteBatch);
        }
    }
}
