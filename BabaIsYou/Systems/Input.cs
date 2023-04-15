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
    public enum Action
    {
        Undo,
        Reset
    }
    class Input : System
    {
        private List<Action> _actions = new();
        private CustomKeyboard _keyboard = new CustomKeyboard();
        private Dictionary<Keys, Direction> _inputMap = new Dictionary<Keys, Direction>();
        private Dictionary<Keys, Action> _actionMap = new Dictionary<Keys, Action>();
        public Input() //Dictionary<Keys, Direction> inputMap
        {
            _inputMap = new Dictionary<Keys, Direction> { { Keys.Up, Direction.Up }, { Keys.Right, Direction.Right }, { Keys.Down, Direction.Down }, { Keys.Left, Direction.Left } };
            _actionMap = new Dictionary<Keys, Action>() { { Keys.Z, Action.Undo}, { Keys.R, Action.Reset } };
        }

        public override void Update(GameTime gameTime)
        {
            _actions.Clear();
            _keyboard.GetKeyboardState();
            foreach (var entity in m_entities.Values)
            {
                UpdateEntity(entity);
            }

            foreach (var key in Keyboard.GetState().GetPressedKeys())
            {
                if (_actionMap.ContainsKey(key) && _keyboard.JustPressed(key))
                {
                    _actions.Add(_actionMap[key]);
                }
            }
        }
        public List<Action> Actions()
        {
            return _actions;
        }
        public void UpdateEntity(Entity entity)
        {
            var movable = entity.GetComponent<Position>();

            if (entity.HasComponent<Property>())
            {
                var property = entity.GetComponent<Property>();
                if (property.HasPropertyType(PropertyType.You))
                {

                    foreach (var key in Keyboard.GetState().GetPressedKeys())
                    {
                        if (_inputMap.ContainsKey(key) && _keyboard.JustPressed(key))
                        {
                            movable.direction = _inputMap[key];
                            return;
                        }
                    }

                }
            }

            movable.direction = Direction.Stopped;

        }

    }
}
