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
    public class BabaET : EntityType
    {
        public new string fileLetter = "b";
        public new NounType nounType = NounType.BigBlue;
        override public Entity CreateEntity(Game1 game, int x, int y)
        {
            Entity e = new Entity();
            e.Add(new Position(0, 0));
            e.Add(new Sprite(game.Content.Load<Texture2D>("Things/Baba"), Color.White, 1));
            e.Add(new Input(new Dictionary<Keys, Direction> { { Keys.Up, Direction.Up }, { Keys.Right, Direction.Right }, { Keys.Down, Direction.Down }, { Keys.Left, Direction.Left } }));
            e.Add(new Property(PropertyType.You));
            e.Add(new Noun(NounType.BigBlue));

            return e;

        }
    }
}
