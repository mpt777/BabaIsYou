﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabaIsYou.Entities;

namespace BabaIsYou.Systems
{

    class Renderer : System
    {
        private readonly int GRID_SIZE;
        private readonly int CELL_SIZE;
        private readonly int OFFSET_X;
        private readonly int OFFSET_Y;
        private readonly SpriteBatch m_spriteBatch;
        private readonly Texture2D m_texBackground;

        public Renderer(SpriteBatch spriteBatch, int width, int height, int gridSize) : base(typeof(Components.Position), typeof(Components.Sprite))
        {
            GRID_SIZE = gridSize;
            CELL_SIZE = height / gridSize;
            OFFSET_X = (width - gridSize * CELL_SIZE) / 2;
            OFFSET_Y = (height - gridSize * CELL_SIZE) / 2;
            m_spriteBatch = spriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            //
            // Draw a blue background
            //Rectangle background = new Rectangle(OFFSET_X, OFFSET_Y, GRID_SIZE * CELL_SIZE, GRID_SIZE * CELL_SIZE);
            //m_spriteBatch.Draw(m_texBackground, background, Color.Blue);

            foreach (var entity in m_entities.Values)
            {
                RenderEntity(entity);
            }

            m_spriteBatch.End();
        }

        private void RenderEntity(Entity entity)
        {
            var sprite = entity.GetComponent<Components.Sprite>();
            var position = entity.GetComponent<Components.Position>();
            m_spriteBatch.Draw(sprite.sprite, new Rectangle((position.x * GRID_SIZE) + OFFSET_X, (position.y * GRID_SIZE) + OFFSET_Y, GRID_SIZE, GRID_SIZE), sprite.Bounds(), sprite.fill);
        }
    }
}
