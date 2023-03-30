﻿using BabaIsYou.Components;
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
    public class WallET : EntityType
    {
        public new string fileLetter = "w";
        public new NounType nounType = NounType.Wall;
        override public Entity CreateEntity(Game1 game, int x, int y)
        {
            Entity e = new Entity();
            e.Add(new Position(x, y));
            e.Add(new Sprite(game.Content.Load<Texture2D>("Things/wall"), Color.Gray, 3));
            e.Add(new Property(PropertyType.Pushable));
            e.Add(new Noun(NounType.Wall));

            return e;

        }
    }
}
