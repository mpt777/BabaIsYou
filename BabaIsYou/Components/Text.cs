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
    }
}
