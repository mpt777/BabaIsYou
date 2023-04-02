using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Components
{
    public enum Direction
    {
        Stopped,
        Up,
        Down,
        Left,
        Right
    }
    public class Position : Component
    {
        public int x;
        public int y;
        public Direction direction;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override Component DeepClone()
        {
            return (Component)new Position(this.x, this.y);
        }
    }

}
