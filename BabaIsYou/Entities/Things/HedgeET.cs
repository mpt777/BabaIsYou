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
    public class HedgeET : EntityType
    {
        public new string fileLetter = "h";
        public new NounType nounType = NounType.Hedge;

        public HedgeET(Game1 game) : base(game) { }
        override public Entity CreateEntity(int x, int y)
        {
            Entity e = new Entity();
            e.Add(new Position(x, y));
            e.Add(new Sprite(_game.Content.Load<Texture2D>("Things/hedge"), Color.Orange, 3, 0.8f));
            e.Add(new Noun(NounType.Hedge));
            e.Add(new Property(PropertyType.Stop));

            return e;
        }
    }
}
