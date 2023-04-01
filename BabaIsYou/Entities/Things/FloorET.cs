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
    public class FloorET : EntityType
    {
        public new string fileLetter = "l";
        public new NounType nounType = NounType.Floor;

        public FloorET(Game1 game) : base(game) { }
        override public Entity CreateEntity(int x, int y)
        {
            Entity e = new Entity();
            e.Add(new Position(x, y));
            e.Add(new Sprite(_game.Content.Load<Texture2D>("Things/floor"), Color.DimGray, 3, 1f));
            e.Add(new Noun(NounType.Floor));

            return e;
        }
    }
}
