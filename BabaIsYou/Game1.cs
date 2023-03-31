using BabaIsYou.Components;
using BabaIsYou.Entities;
using BabaIsYou.Entities.Things;
using BabaIsYou.Entities.Words;
using BabaIsYou.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BabaIsYou
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Stack<IGameView> views = new Stack<IGameView>();

        private readonly int WINDOW_WIDTH = 1280;
        private readonly int WINDOW_HEIGHT = 720;

        private List<Entity> m_removeThese = new List<Entity>();
        private List<Entity> m_addThese = new List<Entity>();

        private Systems.Renderer m_sysRenderer;
        private Systems.Input m_sysKeyboardInput;
        private Systems.Movement m_sysMovement;
        private Systems.Rule m_sysRule;
        private Systems.AnimatedSprite m_sysAnimatedSprite;

        private Systems.Tileset m_sysTileSet;

        public Dictionary<NounType, EntityType> nounTypeLookup;        
        public Dictionary<String, EntityType> textLookup;        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            Systems.LevelReader levelReader = new Systems.LevelReader("Levels/level-1.bbiy");

            m_sysTileSet = new Systems.Tileset(15, 10, WINDOW_WIDTH, WINDOW_HEIGHT);

            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();

            m_sysRenderer = new Systems.Renderer(_spriteBatch, WINDOW_WIDTH, WINDOW_HEIGHT, m_sysTileSet);
            m_sysKeyboardInput = new Systems.Input();
            m_sysMovement = new Systems.Movement(m_sysTileSet);
            m_sysRule = new Systems.Rule(this, m_sysTileSet);
            m_sysAnimatedSprite = new Systems.AnimatedSprite();

            Texture2D _sprite = new Texture2D(GraphicsDevice, 1, 1);
            _sprite.SetData(new[] { Color.White });



            AddEntity(new WordPushET(this).CreateEntity(7, 4));
            AddEntity(new WordWallET(this).CreateEntity(5, 4));
            AddEntity(new WordIsET(this).CreateEntity(6, 4));

            AddEntity(new WordIsET(this).CreateEntity(3, 1));
            AddEntity(new WordRockET(this).CreateEntity(3, 2));
            AddEntity(new WordStopET(this).CreateEntity(3, 4));


            AddEntity(nounTypeLookup[NounType.Rock].CreateEntity(3, 3));
            AddEntity(nounTypeLookup[NounType.Wall].CreateEntity(8, 5));
            AddEntity(nounTypeLookup[NounType.BigBlue].CreateEntity(0, 0));
            AddEntity(nounTypeLookup[NounType.Floor].CreateEntity(7, 7));
            AddEntity(nounTypeLookup[NounType.Flag].CreateEntity(8, 8));

            m_sysTileSet.FillTileSet();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            m_addThese = m_sysRule.AddThese();
            m_removeThese = m_sysRule.RemoveThese();

            foreach (var entity in m_removeThese)
            {
                RemoveEntity(entity);
            }
            m_removeThese.Clear();

            foreach (var entity in m_addThese)
            {
                AddEntity(entity);
            }
            m_addThese.Clear();

            m_sysKeyboardInput.Update(gameTime);
            m_sysMovement.Update(gameTime);
            m_sysTileSet.BuildTileSet();
            m_sysTileSet.FillTileSet();

            if (m_sysMovement.HasUpdated())
            {
                m_sysRule.Update(gameTime);
            }
            
            m_sysAnimatedSprite.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            m_sysRenderer.Update(gameTime);

        }

        private void AddEntity(Entity entity)
        {
            m_sysRenderer.Add(entity);
            m_sysKeyboardInput.Add(entity);
            m_sysMovement.Add(entity);
            m_sysRule.Add(entity);
            m_sysTileSet.Add(entity);
            m_sysAnimatedSprite.Add(entity);
        }

        private void RemoveEntity(Entity entity)
        {
            m_sysRenderer.Remove(entity.Id);
            m_sysKeyboardInput.Remove(entity.Id);
            m_sysMovement.Remove(entity.Id);
            m_sysRule.Remove(entity.Id);
            m_sysTileSet.Remove(entity.Id);
            m_sysAnimatedSprite.Remove(entity.Id);
        }
    }
}