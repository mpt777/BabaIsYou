using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breakout;
using BabaIsYou.Components;
using BabaIsYou.Entities;

namespace BabaIsYou.Systems
{
    class Input : System
    {

        CustomKeyboard keyboard = new CustomKeyboard();
        Dictionary<Keys, Direction> inputMap = new Dictionary<Keys, Direction>();
        public Input() //Dictionary<Keys, Direction> inputMap
        {
            inputMap = new Dictionary<Keys, Direction> { { Keys.Up, Direction.Up }, { Keys.Right, Direction.Right }, { Keys.Down, Direction.Down }, { Keys.Left, Direction.Left } };
        }

        public override void Update(GameTime gameTime)
        {
            keyboard.GetKeyboardState();
            foreach (var entity in m_entities.Values)
            {
                UpdateEntity(entity);
            }
        }
        public void UpdateEntity(Entity entity)
        {
            var movable = entity.GetComponent<Position>();

            if (entity.HasComponent<Property>())
            {
                var property = entity.GetComponent<Property>();
                if (property.propertyType == PropertyType.You)
                {

                    foreach (var key in Keyboard.GetState().GetPressedKeys())
                    {
                        if (inputMap.ContainsKey(key) && keyboard.JustPressed(key))
                        {
                            movable.direction = inputMap[key];
                            return;
                        }
                    }

                }
            }

            movable.direction = Direction.Stopped;

        }
    }
}
