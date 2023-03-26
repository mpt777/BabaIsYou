using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breakout;

namespace BabaIsYou.Systems
{
    class Input : System {

        CustomKeyboard keyboard = new CustomKeyboard();
        public Input()
            : base(typeof(Components.Input))
        {
        }

        public override void Update(GameTime gameTime)
        {
            keyboard.GetKeyboardState();
            foreach (var entity in m_entities.Values)
            {
                var movable = entity.GetComponent<Components.Position>();
                var input = entity.GetComponent<Components.Input>();

                foreach (var key in Keyboard.GetState().GetPressedKeys())
                {
                    if (input.keys.ContainsKey(key) && keyboard.JustPressed(key))
                    {
                        movable.direction = input.keys[key];
                        return;
                    }
                }

                movable.direction = Components.Direction.Stopped;
            }
        }
    }
}
