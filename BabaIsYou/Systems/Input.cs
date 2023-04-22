using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabaIsYou.Utils;
using BabaIsYou.Components;
using BabaIsYou.Entities;

namespace BabaIsYou.Systems
{
    public enum Action
    {
        Undo,
        Reset,
        Up,
        Right,
        Down,
        Left,
    }
    class Input : System
    {
        private List<Action> _actions = new();
        private CustomKeyboard _keyboard = new CustomKeyboard();
        //private Dictionary<Action, Direction> _inputMap = new Dictionary<Action, Direction>();
        private Dictionary<Action, Direction> _directionMap = new Dictionary<Action, Direction>();
        private Dictionary<Action, Keys> _actionMap = new Dictionary<Action, Keys>();
        public Input(Dictionary<Action, Keys> actionMap)
        {
            _directionMap = new Dictionary<Action, Direction> { { Action.Up, Direction.Up }, { Action.Right, Direction.Right }, { Action.Down, Direction.Down }, { Action.Left, Direction.Left } };
            this._actionMap = actionMap;
        }

        public override void Update(GameTime gameTime)
        {
            _actions.Clear();
            _keyboard.GetKeyboardState();

            //foreach (var key in Keyboard.GetState().GetPressedKeys())
            //{
            //    if (_actionMap.ContainsValue(key) && _keyboard.JustPressed(key))
            //    {
            //        _actions.Add(_actionMap[key]);
            //    }
            //}

            foreach (var key in Keyboard.GetState().GetPressedKeys())
            {
                if (_actionMap.ContainsValue(key) && _keyboard.JustPressed(key))
                {
                    Action[] actions = _actionMap.Where(i => i.Value == key).ToDictionary(it => it.Key, it => it.Value).Keys.ToArray();
                    foreach (Action action in actions)
                    {
                        _actions.Add(action);
                    }
                    
                }
            }

            foreach (var entity in m_entities.Values)
            {
                UpdateEntity(entity);
            }
        }
        public List<Action> Actions()
        {
            return _actions;
        }
        public void UpdateEntity(Entity entity)
        {
            var movable = entity.GetComponent<Position>();


            foreach (Action action in _actions)
            {
                if (_directionMap.ContainsKey(action))
                {
                    if (entity.HasComponent<Property>())
                    {
                        var property = entity.GetComponent<Property>();
                        if (property.HasPropertyType(PropertyType.You))
                        {

                            movable.direction = _directionMap[action];
                            return;
                        }
                    }
                }
            }

            movable.direction = Direction.Stopped;

        }

    }
}
