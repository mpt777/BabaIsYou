using BabaIsYou.Components;
using BabaIsYou.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Systems
{
    public class Movement : System
    {
        private int _width;
        private int _height;
        private Tileset _tileSet;
        public Movement(Tileset tileSet) 
        {
            this._tileSet = tileSet;
            this._width = _tileSet.tilesW;
            this._height = _tileSet.tilesH;
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var entity in m_entities.Values)
            {
                MoveEntity(entity, gameTime);
            }
        }

        private void MoveEntity(Entity entity, GameTime gameTime)
        {
            var movable = entity.GetComponent<Components.Position>();
            if (entity.HasComponent<Components.Property>())
            {
                var property = entity.GetComponent<Components.Property>();
                if (property.propertyType == PropertyType.You)
                {
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
            }
        }

        private void Move(Entity entity, int xIncrement, int yIncrement)
        {
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

            if (position.x < 0 || position.y < 0 || position.x >= _width || position.y >= _height)
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
                var property = otherEntity.GetComponent<Components.Property>();
                if (property.propertyType == PropertyType.Pushable)
                {
                    var otherPosition = otherEntity.GetComponent<Components.Position>();
                    if (otherPosition.x == position.x && otherPosition.y == position.y)
                    {
                        if (!ProcessMovement(otherEntity, xIncrement, yIncrement))
                        {
                            Increment(position, -xIncrement, -yIncrement);
                            return false;
                        }
                    }
                }

            }
            return true;
        }
    }
}
