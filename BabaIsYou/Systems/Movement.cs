using BabaIsYou.Components;
using BabaIsYou.Entities;
using Breakout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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

        public Movement(Level level) 
        {
            this._level = level;
        }
        public override void Update(GameTime gameTime)
        {
            keyboard.GetKeyboardState();
            this._hasUpdated = false;
            foreach (var entity in m_entities.Values)
            {
                MoveEntity(entity, gameTime);
            }
        }
        public bool HasUpdated()
        {
            return this._hasUpdated;
        }

        private void MoveEntity(Entity entity, GameTime gameTime)
        {
            var movable = entity.GetComponent<Components.Position>();

            //movable.elapsedInterval -= movable.moveInterval;
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
                    return true;
                }

                if (otherProperty.HasPropertyType(PropertyType.Kill) && property.HasPropertyType(PropertyType.You))
                {
                    Debug.Print("Defeat!");
                    _removeThese.Add(entity);
                    this.levelState = LevelState.Defeat;
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
                    return true;
                }

            }


            return true;
        }
    }
}
