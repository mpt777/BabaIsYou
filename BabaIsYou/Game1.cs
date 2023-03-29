﻿using BabaIsYou.Components;
using BabaIsYou.Entities;
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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            m_sysTileSet = new Systems.Tileset(15, 10, WINDOW_WIDTH, WINDOW_HEIGHT);

            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();

            m_sysRenderer = new Systems.Renderer(_spriteBatch, WINDOW_WIDTH, WINDOW_HEIGHT, m_sysTileSet.tileSize);
            m_sysKeyboardInput = new Systems.Input();
            m_sysMovement = new Systems.Movement(m_sysTileSet);
            m_sysRule = new Systems.Rule(m_sysTileSet);
            m_sysAnimatedSprite = new Systems.AnimatedSprite();

            Texture2D _sprite = new Texture2D(GraphicsDevice, 1, 1);
            _sprite.SetData(new[] { Color.White });




            Entity block = new Entity();
            block.Add(new Position(1, 1));
            block.Add(new Sprite(Content.Load<Texture2D>("Things/rock"), Color.Tan, 3));
            block.Add(new Property(PropertyType.Pushable));
            block.Add(new Noun(NounType.Rock));
            AddEntity(block);

            Entity block1 = new Entity();
            block1.Add(new Position(2, 1));
            block1.Add(new Sprite(Content.Load<Texture2D>("Words/word-rock"), Color.Tan, 3));
            block1.Add(new Property(PropertyType.Pushable));
            block1.Add(new Text(TextType.Noun, NounType.Rock));
            block1.Add(new Noun(NounType.Text));
            AddEntity(block1);

            Entity block2 = new Entity();
            block2.Add(new Position(3, 1));
            block2.Add(new Sprite(Content.Load<Texture2D>("Words/word-is"), Color.White, 3));
            block2.Add(new Property(PropertyType.Pushable));
            block2.Add(new Text(TextType.Verb, VerbType.Is));
            block2.Add(new Noun(NounType.Text));
            AddEntity(block2);

            Entity block3 = new Entity();
            block3.Add(new Position(4, 1));
            block3.Add(new Sprite(Content.Load<Texture2D>("Words/word-push"), Color.Gray, 3));
            block3.Add(new Property(PropertyType.Pushable));
            block3.Add(new Text(TextType.Adjective, PropertyType.Pushable));
            block3.Add(new Noun(NounType.Text));
            AddEntity(block3);


            Entity block4 = new Entity();
            block4.Add(new Position(5, 4));
            block4.Add(new Sprite(Content.Load<Texture2D>("Words/word-wall"), Color.Gray, 3));
            block4.Add(new Property(PropertyType.Pushable));
            block4.Add(new Text(TextType.Noun, NounType.Wall));
            block4.Add(new Noun(NounType.Text));
            AddEntity(block4);


            Entity block5 = new Entity();
            block5.Add(new Position(6, 4));
            block5.Add(new Sprite(Content.Load<Texture2D>("Words/word-is"), Color.Gray, 3));
            block5.Add(new Property(PropertyType.Pushable));
            block5.Add(new Text(TextType.Verb, VerbType.Is));
            block5.Add(new Noun(NounType.Text));
            AddEntity(block5);


            Entity block6 = new Entity();
            block6.Add(new Position(7, 4));
            block6.Add(new Sprite(Content.Load<Texture2D>("Words/word-stop"), Color.Gray, 3));
            block6.Add(new Property(PropertyType.Pushable));
            block6.Add(new Text(TextType.Adjective, PropertyType.Stop));
            block6.Add(new Noun(NounType.Text));
            AddEntity(block6);

            Entity block7 = new Entity();
            block7.Add(new Position(8, 5));
            block7.Add(new Sprite(Content.Load<Texture2D>("Things/wall"), Color.Gray, 3));
            block7.Add(new Property(PropertyType.Stop));
            block7.Add(new Noun(NounType.Wall));
            AddEntity(block7);

            AddEntity(new BabaET().CreateEntity(this));

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