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
        public InputMap() 
        {
            this.actionMap = new Dictionary<Action, Keys>() { { Action.Up, Keys.Up }, { Action.Right, Keys.Right }, { Action.Down, Keys.Down }, { Action.Left, Keys.Left }, { Action.Undo, Keys.Z }, { Action.Reset, Keys.R } };
        }

        [DataMember()]
        public Dictionary<Action, Keys> actionMap { get; private set; }

        public void UpdateInputMapping(Action action, Keys newKey)
        {
            actionMap[action] = newKey;
        }

    }
}

