using BabaIsYou.Components;
using BabaIsYou.Entities;
using BabaIsYou.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Systems
{
    public enum LevelState
    {
        None,
        Win,
        Defeat
    }
    public class Movement : System
    {
        private Level _level;
        private bool _hasUpdated = false;
        public LevelState levelState { get; private set; }
        CustomKeyboard keyboard = new CustomKeyboard();
        ContentManager contentManager;

        private SoundEffect _moveEffect;
        
        private SoundEffect _winEffect;
        private bool _hasYouMoved;

        private SoundEffect _newWinEffect;

        private HashSet<NounType> _isWin = new();
        private HashSet<NounType> _previousIsWin = new();
        private ParticleSystem _particleSystem;

        public Movement(Level level, ContentManager content, ParticleSystem particleSystem)
        {
            this._level = level;
            this.contentManager = content;
            this._particleSystem = particleSystem;
            this.LoadContent();
        }
        private void LoadContent()
        {
            this._moveEffect = contentManager.Load<SoundEffect>("Sounds/move");
            this._newWinEffect = contentManager.Load<SoundEffect>("Sounds/new_win");
            this._winEffect = contentManager.Load<SoundEffect>("Sounds/win");
        }
        public override void Update(GameTime gameTime)
        {
            keyboard.GetKeyboardState();
            this._hasUpdated = false;
            this._hasYouMoved = false;
            foreach (var entity in m_entities.Values)
            {
                MoveEntity(entity, gameTime);
            }

            if (this._hasYouMoved)
            {
                this._moveEffect.Play();
            }
            //ProcessIsWin();
        }
        public bool HasUpdated()
        {
            return this._hasUpdated;
        }

        private void CheckYouMoved(Entity entity)
        {
            if (this._hasYouMoved) { return; }
            if (!entity.HasComponent<Components.Property>()) { return; }
            var property = entity.GetComponent<Components.Property>();
            if (property == null) { return; }
            if (!property.HasPropertyType(PropertyType.You)) { return; }
            this._hasYouMoved = true;
        }


        private void MoveEntity(Entity entity, GameTime gameTime)
        {
            var movable = entity.GetComponent<Components.Position>();

            switch (movable.direction)
            {
                case Components.Direction.Up:
                    Move(entity, 0, -1);
                    break;
                case Components.Direction.Down:
                    Move(entity, 0, 1);
                    break;
                case Components.Direction.Left:
                    Move(entity, -1, 0);
                    break;
                case Components.Direction.Right:
                    Move(entity, 1, 0);
                    break;
            }
        }

        private void Move(Entity entity, int xIncrement, int yIncrement)
        {
            this._hasUpdated = true;
            CheckYouMoved(entity);
            var position = entity.GetComponent<Components.Position>();
            position.direction = Components.Direction.Stopped;

            ProcessMovement(entity, xIncrement, yIncrement);
        }

        private void Increment(Components.Position position, int xIncrement, int yIncrement)
        {
            position.y += yIncrement;
            position.x += xIncrement;
        }

        private bool isValidMovement(Entity entity, Entity otherEntity)
        {
            var position = entity.GetComponent<Components.Position>();
            if (otherEntity == entity)
            {
                return false;
            }
            if (!otherEntity.HasComponent<Components.Property>())
            {
                return false;
            }
            var otherPosition = otherEntity.GetComponent<Components.Position>();

            if (otherPosition.x != position.x || otherPosition.y != position.y)
            {
                return false;
            }
            return true;
        }

        private bool ProcessMovement(Entity entity, int xIncrement, int yIncrement)
        {
            var position = entity.GetComponent<Components.Position>();
            var property = entity.GetComponent<Components.Property>();
            var noun = entity.GetComponent<Components.Noun>();
            position.direction = Components.Direction.Stopped;

            Increment(position, xIncrement, yIncrement);

            if (position.x < 0 || position.y < 0 || position.x >= _level.Width() || position.y >= _level.Height())
            {
                Increment(position, -xIncrement, -yIncrement);
                return false;
            }

            // not sink
            foreach (var otherEntity in m_entities.Values)
            {
                if (!isValidMovement(entity, otherEntity))
                {
                    continue;
                }

                var otherProperty = otherEntity.GetComponent<Components.Property>();
                if (otherProperty.HasPropertyType(PropertyType.Pushable))
                {
                    if (!ProcessMovement(otherEntity, xIncrement, yIncrement))
                    {
                        Increment(position, -xIncrement, -yIncrement);
                        return false;
                    }
                }

                if (otherProperty.HasPropertyType(PropertyType.Stop))
                {
                    Increment(position, -xIncrement, -yIncrement);
                    return false;
                }

                if (otherProperty.HasPropertyType(PropertyType.Win) && property.HasPropertyType(PropertyType.You))
                {
                    this.levelState = LevelState.Win;
                    this._winEffect.Play();
                    return true;
                }

                if (otherProperty.HasPropertyType(PropertyType.Kill) && property.HasPropertyType(PropertyType.You))
                {
                    Debug.Print("Defeat!");
                    _removeThese.Add(entity);
                    this.levelState = LevelState.Defeat;
                    _particleSystem.ObjectDeath(entity);
                    return true;
                }

            }

            // sink
            foreach (var otherEntity in m_entities.Values)
            {
                if (!isValidMovement(entity, otherEntity))
                {
                    continue;
                }

                var otherProperty = otherEntity.GetComponent<Components.Property>();

                if (otherProperty.HasPropertyType(PropertyType.Sink) && noun.nounType != NounType.Text)
                {
                    Debug.Print("Sink!");
                    _removeThese.Add(entity);
                    _removeThese.Add(otherEntity);
                    _particleSystem.ObjectDeath(entity);
                    _particleSystem.ObjectDeath(otherEntity);
                    return true;
                }

            }


            return true;
        }
    }
}
