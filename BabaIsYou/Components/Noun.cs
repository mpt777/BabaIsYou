using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Components
{
    public enum NounType
    {
        Rock,
        BigBlue,
        Wall,
        Text,
        Lava,
        Water,
        Flag,
        Floor,
        Grass,
        Hedge,
    }
    public class Noun : Component
    {
        public NounType nounType;
        public Noun(NounType nounType)
        {
            this.nounType = nounType;
        }
    }
}
