using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BabaIsYou.Systems;
using System.Runtime.Serialization;

namespace BabaIsYou.Controls
{
    [DataContract(Name = "InputMap")]
    public class InputMap
    {
        private Persistance _persistance;

        [DataMember()]
        private Dictionary<Action, Keys> _actionMap { get; set; }
        public InputMap()
        {
            this._persistance = new Persistance();
            this._actionMap = new Dictionary<Action, Keys>() { { Action.Up, Keys.W }, { Action.Right, Keys.D }, { Action.Down, Keys.S }, { Action.Left, Keys.A }, { Action.Undo, Keys.Z }, { Action.Reset, Keys.R } };
            this._persistance.LoadGameState();
        }

        public Dictionary<Action, Keys> GetActionMap()
        {
            if (this._persistance.inputMap == null)
            {
                return this._actionMap;
            }
            return this._persistance.inputMap._actionMap;
        }

        public void UpdateInputMapping(Action action, Keys newKey)
        {
            _actionMap[action] = newKey;
            this._persistance.SaveGameState(this);
        }

    }
}

