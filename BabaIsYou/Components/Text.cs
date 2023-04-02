using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Components
{
    public enum TextType
    {
        Noun,
        Adjective,
        Verb,
    }

    public enum VerbType
    {
        Is,
    }

    public class Text : Component
    {
        public TextType textType;
        public PropertyType? propertyType = null;
        public NounType? nounType = null;
        public VerbType? verbType = null;
        public Text(TextType textType, VerbType verbType)
        {
            this.textType = textType;
            this.verbType = verbType;
        }
        public Text(TextType textType, PropertyType propertyType) 
        {
            this.textType = textType;
            this.propertyType = propertyType;
        }
        public Text(TextType textType, NounType nounType)
        {
            this.textType = textType;
            this.nounType = nounType;
        }
        public override Component DeepClone()
        {
            if (this.verbType != null)
            {
                return (Component)new Text(this.textType, (VerbType)this.verbType);
            }
            if (this.nounType != null)
            {
                return (Component)new Text(this.textType, (NounType)this.nounType);
            }
            if (this.propertyType != null)
            {
                return (Component)new Text(this.textType, (PropertyType)this.propertyType);
            }
            throw new NotImplementedException();
            
        }
    }
}
