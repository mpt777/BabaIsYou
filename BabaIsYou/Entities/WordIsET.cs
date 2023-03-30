using BabaIsYou.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Entities
{
    public class WordIsET : EntityType
    {
        public new string fileLetter = "I";
        public new NounType nounType = NounType.Text;
        override public Entity CreateEntity(Game1 game, int x, int y)
        {
            Entity e = new Entity();
            e.Add(new Position(x, y));
            e.Add(new Sprite(game.Content.Load<Texture2D>("Words/word-is"), Color.White, 3));
            e.Add(new Property(PropertyType.Pushable));
            e.Add(new Text(TextType.Verb, VerbType.Is));
            e.Add(new Noun(NounType.Text));

            return e;
        }
    }
}
