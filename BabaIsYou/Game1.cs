using BabaIsYou.Components;
using BabaIsYou.Entities;
using BabaIsYou.Entities.Things;
using BabaIsYou.Entities.Words;
using BabaIsYou.Systems;
using BabaIsYou.Views;
using Breakout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BabaIsYou
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Stack<GameView> views = new Stack<GameView>();

        public readonly int WINDOW_WIDTH = 1280;
        public readonly int WINDOW_HEIGHT = 720;

        public CustomKeyboard keyboard;

        public LevelReader levelReader;

        public Stack<GameView> frames = new Stack<GameView>();
        public Stack<GameView> framesToAdd = new Stack<GameView>();

        private Texture2D _texture;
        public Level level;

        private int _shouldPopFrame = 0;

        //private Systems.Level m_sysLevel;

        public Dictionary<NounType, EntityType> nounTypeLookup;        
        public Dictionary<String, EntityType> textLookup;        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.keyboard = new CustomKeyboard();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.nounTypeLookup = new Dictionary<NounType, EntityType> {
                { NounType.BigBlue, new BabaET(this) },
                { NounType.Wall, new WallET(this) },
                { NounType.Rock, new RockET(this) },
                { NounType.Floor, new FloorET(this) },
                { NounType.Flag, new FlagET(this) },
                { NounType.Hedge, new HedgeET(this) },
                { NounType.Water, new WaterET(this) },
                { NounType.Grass, new GrassET(this) },
                { NounType.Lava, new LavaET(this) },
            };


            base.Initialize();

            this.levelReader = new LevelReader(this, "levels/levels-all.bbiy");

            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();

            this.frames.Push(new MainMenu(this, WINDOW_WIDTH, WINDOW_HEIGHT));

        }
        public SpriteBatch SpriteBatch()
        {
            return this._spriteBatch;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        public void AddFrame(GameView frame)
        {
            this.framesToAdd.Push(frame);
        }
        public void RemoveFrame()
        {
            this._shouldPopFrame += 1;
        }
        private void ProcessFrames(GameTime gameTime)
        {
            while (this._shouldPopFrame > 0)
            {
                this.frames.Pop();
                this.frames.Peek().active = true;
                this.frames.Peek().paused = false;
                this._shouldPopFrame -= 1;

            }

            foreach (GameView frame in framesToAdd)
            {
                this.frames.Push(frame);
            }
            this.framesToAdd.Clear();


            foreach (GameView frame in frames)
            {
                if (frame.active && !frame.paused)
                {
                    frame.Update(gameTime);
                }
            }

        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here
            this.keyboard.GetKeyboardState();
            this.ProcessFrames(gameTime);
            base.Update(gameTime);            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            //m_sysRenderer.Update(gameTime);

            this._spriteBatch.Begin(SpriteSortMode.BackToFront);
            //this._spriteBatch.Draw(this._texture, new Rectangle(0, 0, width, height), Color.White);
            foreach (GameView frame in frames.Reverse())
            {
                if (frame.active)
                {
                    frame.Draw(this._spriteBatch);
                }
            }
            base.Draw(gameTime);

            this._spriteBatch.End();

        }

    }
}