using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Components
{
    public enum PropertyType
    {
        Pushable,
        You,
        Push,
        Move,
        Win,
        Kill,
        Stop,
        Sink,
    }
    public class Property : Component
    {
        public PropertyType? propertyType;
        public Property(PropertyType propertyType)
        {
            this.propertyType = propertyType;
        }

        public void Clear()
        {
            this.propertyType = null;
        }

    }
}
