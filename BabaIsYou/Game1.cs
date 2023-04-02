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

        private Systems.Level m_sysLevel;

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

            

            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();

            Systems.LevelReader levelReader = new Systems.LevelReader(this, "Levels/level-1.bbiy");

            this.m_addThese = levelReader.Entities();

            m_sysLevel = levelReader.Level();
            m_sysRenderer = new Systems.Renderer(_spriteBatch, WINDOW_WIDTH, WINDOW_HEIGHT, m_sysLevel);
            m_sysKeyboardInput = new Systems.Input();
            m_sysMovement = new Systems.Movement(m_sysLevel);
            m_sysRule = new Systems.Rule(this, m_sysLevel);
            m_sysAnimatedSprite = new Systems.AnimatedSprite();

            AddAndRemoveEntities();

            m_sysLevel.Start();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        private void AddAndRemoveEntities()
        {
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            m_sysKeyboardInput.Update(gameTime);
            List<Systems.Action> actions = m_sysKeyboardInput.Actions();
            foreach (var action in actions)
            {
                switch (action)
                {
                    case Systems.Action.Undo:
                        m_sysLevel.Undo();
                        m_removeThese = m_sysLevel.RemoveThese();
                        m_addThese = m_sysLevel.AddThese();
                        break;
                }
            }
            if (actions.Count == 0)
            {
                m_addThese = m_sysRule.AddThese();
                m_removeThese = m_sysRule.RemoveThese();
            }

            if (m_addThese.Count != 0 || m_removeThese.Count != 0)
            {
                AddAndRemoveEntities();
                m_sysAnimatedSprite.ForceUpdateEntities(); // Move this to a better spot!
            }
            
            
            m_sysMovement.Update(gameTime);

            if (m_sysMovement.HasUpdated())
            {
                m_sysLevel.ClearTileSet();
                m_sysLevel.FillTileSet();
                m_sysRule.Update(gameTime);
            }
            
            m_sysAnimatedSprite.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

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
            m_sysAnimatedSprite.Add(entity);
            m_sysLevel.Add(entity);
        }

        private void RemoveEntity(Entity entity)
        {
            m_sysRenderer.Remove(entity.Id);
            m_sysKeyboardInput.Remove(entity.Id);
            m_sysMovement.Remove(entity.Id);
            m_sysRule.Remove(entity.Id);
            m_sysAnimatedSprite.Remove(entity.Id);
            m_sysLevel.Remove(entity.Id);
        }
    }
}