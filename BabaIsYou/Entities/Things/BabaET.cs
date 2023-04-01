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
    public class BabaET : EntityType
    {
        public new string fileLetter = "b";
        public new NounType nounType = NounType.BigBlue;

        public BabaET(Game1 game) : base(game) { }
        override public Entity CreateEntity(int x, int y)
        {
            Entity e = new Entity();
            e.Add(new Position(x, y));
            e.Add(new Sprite(_game.Content.Load<Texture2D>("Things/Baba"), Color.White, 1, 0f));
            e.Add(new Input(new Dictionary<Keys, Direction> { { Keys.Up, Direction.Up }, { Keys.Right, Direction.Right }, { Keys.Down, Direction.Down }, { Keys.Left, Direction.Left } }));
            e.Add(new Property(PropertyType.You));
            e.Add(new Noun(NounType.BigBlue));

            return e;

        }
    }
}
