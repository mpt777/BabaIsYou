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
    public class WordRockET : EntityType
    {
        public new string fileLetter = "R";
        public new NounType nounType = NounType.Text;
        override public Entity CreateEntity(Game1 game, int x, int y)
        {
            Entity e = new Entity();
            e.Add(new Position(x, y));
            e.Add(new Sprite(game.Content.Load<Texture2D>("Words/word-rock"), Color.Tan, 3));
            e.Add(new Property(PropertyType.Pushable));
            e.Add(new Text(TextType.Noun, NounType.Rock));
            e.Add(new Noun(NounType.Text));

            return e;

        }
    }
}
