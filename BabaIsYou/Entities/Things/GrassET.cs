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

namespace BabaIsYou.Entities.Things
{
    public class GrassET : EntityType
    {
        public new string fileLetter = "g";
        public new NounType nounType = NounType.Grass;

        public GrassET(Game1 game) : base(game) { }
        override public Entity CreateEntity(int x, int y)
        {
            Entity e = new Entity();
            e.Add(new Position(x, y));
            e.Add(new Sprite(_game.Content.Load<Texture2D>("Things/grass"), Color.Green, 3, 0.9f));
            e.Add(new Noun(NounType.Grass));

            return e;
        }
    }
}
