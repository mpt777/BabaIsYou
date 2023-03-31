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

namespace BabaIsYou.Entities.Words
{
    public class WordWallET : EntityType
    {
        public new string fileLetter = "W";
        public new NounType nounType = NounType.Text;

        public WordWallET(Game1 game) : base(game) { }
        override public Entity CreateEntity(int x, int y)
        {
            Entity e = new Entity();
            e.Add(new Position(x, y));
            e.Add(new Sprite(_game.Content.Load<Texture2D>("Words/word-wall"), Color.Gray, 3, 0.2f));
            e.Add(new Property(PropertyType.Pushable));
            e.Add(new Text(TextType.Noun, NounType.Wall));
            e.Add(new Noun(NounType.Text));

            return e;
        }
    }
}
