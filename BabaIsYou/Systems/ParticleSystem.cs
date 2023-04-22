using BabaIsYou.Components;
using BabaIsYou.Particles;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using BabaIsYou.Entities;

namespace BabaIsYou.Systems
{
    public class ParticleSystem : System
    {
        private List<ParticleEmitter> particleEmitters = new();
        private Game1 _game;
        private Renderer _render;
        public ParticleSystem(Game1 game, Renderer render) 
        {
            this._game = game;
            this._render = render;
        }
        public void ObjectDeath(Entity entity)
        {
            if (!entity.HasComponent<Position>()) { return; }
            if (!entity.HasComponent<Sprite>()) { return; }

            var positon = entity.GetComponent<Position>();
            var sprite = entity.GetComponent<Sprite>();

            int gridSize = this._render.GridSize();
            int x = this._render.LevelXScreen(positon.x);
            int y = this._render.LevelYScreen(positon.y);

            ParticleEmitter emitter = new ParticleEmitter(
                this._game.Content,
                new TimeSpan(0, 0, 0, 0, 2),
                new Rectangle((int)x, (int)y, gridSize, gridSize),
                5,
                5,
                new TimeSpan(0, 0, 0, 0, 600),
                sprite.sprite,
                new TimeSpan(0, 0, 0, 0, 100)
             ); ;
            emitter.Gravity = new Vector2(0, 0.1f);
            particleEmitters.Add(emitter);
        }
        public override void Update(GameTime gameTime)
        {
            foreach(ParticleEmitter pe in this.particleEmitters)
            {
                pe.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ParticleEmitter pe in this.particleEmitters)
            {
                pe.Draw(spriteBatch);
            }
        }
    }
}
