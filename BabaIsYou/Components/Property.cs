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
        public bool isDefault = false; 
        public Property(PropertyType? propertyType)
        {
            this.Init(propertyType, false);
        }
        public Property(PropertyType? propertyType, bool isDefault)
        {
            this.Init(propertyType, isDefault);
        }
        private void Init(PropertyType? propertyType, bool isDefault)
        {
            this.propertyType = propertyType;
            this.isDefault = isDefault;
        }

        public void Clear()
        {
            this.propertyType = null;
        }
        public override Component DeepClone()
        {
            return (Component)new Property(this.propertyType, this.isDefault);
        }

    }
}
