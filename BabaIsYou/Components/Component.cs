using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Components
{
    public abstract class Component
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public abstract Component DeepClone();
    }
}
