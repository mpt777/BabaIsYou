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
using BabaIsYou.Utils;

namespace BabaIsYou.Systems
{
    public class ParticleSystem : System
    {
        private List<ParticleEmitter> _particleEmitters = new();
        private List<ParticleEmitter> _removeParticleEmitters = new();
        private Game1 _game;
        private Renderer _render;
        private MyRandom _random;
        public ParticleSystem(Game1 game, Renderer render) 
        {
            this._game = game;
            this._render = render;
            this._random = new MyRandom();
        }
        private Texture2D Sprite(Color color)
        {
            Texture2D sprite = new Texture2D(this._game.GraphicsDevice, 1, 1);
            sprite.SetData(new[] { color });
            return sprite;
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
                new TimeSpan(0, 0, 0, 0, 5),
                new Rectangle((int)x, (int)y, gridSize, gridSize),
                3,
                0.5f,
                new TimeSpan(0, 0, 0, 0, 400),
                Sprite(sprite.fill),
                new TimeSpan(0, 0, 0, 0, 100)
             );
            emitter.SetEmittingArea(EmittingArea.Stroke);
            _particleEmitters.Add(emitter);
        }
        public void ObjectIsWin(Entity entity)
        {
            if (!entity.HasComponent<Position>()) { return; }

            var positon = entity.GetComponent<Position>();

            int gridSize = this._render.GridSize();
            int x = this._render.LevelXScreen(positon.x);
            int y = this._render.LevelYScreen(positon.y);

            ParticleEmitter emitter = new ParticleEmitter(
                this._game.Content,
                new TimeSpan(0, 0, 0, 0, 5),
                new Rectangle((int)x, (int)y, gridSize, gridSize),
                3,
                0.2f,
                new TimeSpan(0, 0, 0, 0, 400),
                Sprite(Color.Yellow),
                new TimeSpan(0, 0, 0, 0, 100)
             );
            emitter.SetEmittingArea(EmittingArea.Stroke);
            _particleEmitters.Add(emitter);
        }
        public void ObjectIsYou(Entity entity)
        {
            if (!entity.HasComponent<Position>()) { return; }

            var positon = entity.GetComponent<Position>();

            int gridSize = this._render.GridSize();
            int x = this._render.LevelXScreen(positon.x);
            int y = this._render.LevelYScreen(positon.y);

            ParticleEmitter emitter = new ParticleEmitter(
                this._game.Content,
                new TimeSpan(0, 0, 0, 0, 5),
                new Rectangle((int)x, (int)y, gridSize, gridSize),
                3,
                0.5f,
                new TimeSpan(0, 0, 0, 0, 400),
                Sprite(Color.DeepPink),
                new TimeSpan(0, 0, 0, 0, 100)
             ); 
            emitter.SetEmittingArea(EmittingArea.Stroke);
            _particleEmitters.Add(emitter);
        }
        private Rectangle GetPointInRect(Rectangle rect)
        {
            int x = rect.X + _random.Next(rect.Width);
            int y = rect.Y + _random.Next(rect.Height);

            return new Rectangle(x, y,1,1);
        }
        public void Fireworks(Rectangle rect, Color color)
        { 
            ParticleEmitter emitter = new ParticleEmitter(
                this._game.Content,
                new TimeSpan(0, 0, 0, 0, 5),
                GetPointInRect(rect),
                4,
                5,
                new TimeSpan(0, 0, 0, 0, 800),
                Sprite(color),
                new TimeSpan(0, 0, 0, 0, 100)
             );
            emitter.Gravity = new Vector2(0, 0.1f);
            _particleEmitters.Add(emitter);
        }
        public override void Update(GameTime gameTime)
        {

            foreach (ParticleEmitter pe in this._particleEmitters)
            {
                pe.Update(gameTime);
                if (pe.Finished())
                {
                    _removeParticleEmitters.Add(pe);
                }
            }
            foreach (ParticleEmitter pe in this._removeParticleEmitters)
            {
                this._particleEmitters.Remove(pe);
            }

            _removeParticleEmitters.Clear();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ParticleEmitter pe in this._particleEmitters)
            {
                pe.Draw(spriteBatch);
            }
        }
    }
}
