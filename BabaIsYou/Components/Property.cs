using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Components
{
    public enum PropertyType
    {
        Unset = 0,
        Pushable = 1,
        You = 2,
        Push = 4,
        Move = 8,
        Win = 16,
        Kill = 32,
        Stop = 64,
        Sink = 128,
    }
    public class Property : Component
    {
        private PropertyType propertyType;
        public bool isDefault = false; 
        public Property(PropertyType propertyType)
        {
            this.Init(propertyType, false);
        }
        public Property(PropertyType propertyType, bool isDefault)
        {
            this.Init(propertyType, isDefault);
        }
        private void Init(PropertyType propertyType, bool isDefault)
        {
            this.propertyType = propertyType;
            this.isDefault = isDefault;
        }

        public void Clear()
        {
            this.propertyType = PropertyType.Unset;
        }
        public void AddPropertyType(PropertyType pt)
        {
            if (propertyType == PropertyType.Unset)
            {
                this.propertyType = pt;
            }
            else
            {
                this.propertyType |= pt;
            }
            
        }
        public bool HasPropertyType(PropertyType pt)
        {
            return this.propertyType.HasFlag(pt);
        }
        public override Component DeepClone()
        {
            return (Component)new Property(this.propertyType, this.isDefault);
        }

    }
}
