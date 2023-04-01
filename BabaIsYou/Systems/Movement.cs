using BabaIsYou.Components;
using BabaIsYou.Entities;
using Breakout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Systems
{
    public class Movement : System
    {
        private Level _level;
        private bool _hasUpdated = false;
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

        private bool ProcessMovement(Entity entity, int xIncrement, int yIncrement)
        {
            var position = entity.GetComponent<Components.Position>();
            position.direction = Components.Direction.Stopped;

            Increment(position, xIncrement, yIncrement);

            if (position.x < 0 || position.y < 0 || position.x >= _level.Width() || position.y >= _level.Height())
            {
                Increment(position, -xIncrement, -yIncrement);
                return false;
            }

            foreach (var otherEntity in m_entities.Values)
            {
                if (otherEntity == entity)
                {
                    continue;
                }
                if (!otherEntity.HasComponent<Components.Property>())
                {
                    continue;
                }
                var otherPosition = otherEntity.GetComponent<Components.Position>();

                if (otherPosition.x != position.x || otherPosition.y != position.y)
                {
                    continue;
                }

                var property = otherEntity.GetComponent<Components.Property>();
                if (property.propertyType == PropertyType.Pushable)
                {
                    if (!ProcessMovement(otherEntity, xIncrement, yIncrement))
                    {
                        Increment(position, -xIncrement, -yIncrement);
                        return false;
                    }
                }

                if (property.propertyType == PropertyType.Stop)
                {
                    Increment(position, -xIncrement, -yIncrement);
                    return false;
                }

            }
            return true;
        }
    }
}
