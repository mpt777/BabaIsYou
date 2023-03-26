using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Components
{
    class Input : Component
    {
        public Dictionary<Keys, Components.Direction> keys;

        public Input(Dictionary<Keys, Components.Direction> keys)
        {
            this.keys = keys;
        }
    }
}
