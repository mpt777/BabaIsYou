using Microsoft.Xna.Framework.Graphics;
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
        private readonly Texture2D m_texBackground;

        public Renderer(int width, int height, Level level) : base(typeof(Components.Position), typeof(Components.Sprite))
        {
            GRID_SIZE = Math.Min(width / level.Width(), height / level.Height());
            OFFSET_X = (width - (GRID_SIZE * level.Width())) / 2;
            OFFSET_Y = (height - (GRID_SIZE * level.Height())) / 2;
        }

        public override void Update(GameTime gameTime)
        {
            //m_spriteBatch.Begin(SpriteSortMode.BackToFront);

            ////
            //// Draw a blue background
            ////Rectangle background = new Rectangle(OFFSET_X, OFFSET_Y, GRID_SIZE * CELL_SIZE, GRID_SIZE * CELL_SIZE);
            ////m_spriteBatch.Draw(m_texBackground, background, Color.Blue);

            //foreach (var entity in m_entities.Values)
            //{
            //    RenderEntity(entity);
            //}

            //m_spriteBatch.End();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in m_entities.Values)
            {
                RenderEntity(entity, spriteBatch);
            }

        }

        private void RenderEntity(Entity entity, SpriteBatch spriteBatch)
        {
            var sprite = entity.GetComponent<Components.Sprite>();
            var position = entity.GetComponent<Components.Position>();
            spriteBatch.Draw(sprite.sprite, new Rectangle((position.x * GRID_SIZE) + OFFSET_X, (position.y * GRID_SIZE) + OFFSET_Y, GRID_SIZE, GRID_SIZE), sprite.Bounds(), sprite.fill, 0f, new Vector2(0,0), SpriteEffects.None, sprite.LayerDepth());
        }
    }
}
