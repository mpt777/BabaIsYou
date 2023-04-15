using BabaIsYou;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Views
{
    public class GameView
    {
        protected Game1 game;
        public Vector2 position = new Vector2(0, 0);
        public Vector2 dimensions = new Vector2(0, 0);
        public bool active = true;
        public bool paused = false;

        public GameView(Game1 game)
        {
            this.dimensions = new Vector2(game.WINDOW_WIDTH, game.WINDOW_HEIGHT);
            this.game = game;
        }
        public GameView(Game1 game, int width, int height)
        {
            this.dimensions = new Vector2(width, height);
            this.game = game;
        }
        public GameView(Game1 game, int width, int height, int posX, int posY)
        {
            this.dimensions = new Vector2(width, height);
            this.position = new Vector2(posX, posY);
            this.game = game;
        }
        virtual protected void LoadContent()
        {

        }
        virtual protected void Initialize()
        {

        }
        virtual public void Update(GameTime gameTime)
        {

        }
        virtual public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
