using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabaIsYou.Entities;
using BabaIsYou.Systems;
using Microsoft.Xna.Framework;

namespace BabaIsYou.Systems
{
    public class Tileset : System
    {
        private int width;
        private int height;
        public int tileSize;
        public int tilesW;
        public int tilesH;

        public List<Entity>[,] tiles;

        public Tileset(int tilesW, int tilesH, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.tilesW = tilesW;
            this.tilesH = tilesH;
            this.tileSize = Math.Min(width / tilesW, height / tilesH);

            this.BuildTileSet();
        }

        public void BuildTileSet()
        {
            tiles = new List<Entity>[tilesW, tilesH];
            for (int i = 0; i < tilesW; i++)
            {
                for (int j = 0; j < tilesH; j++)
                {
                    tiles[i, j] = new List<Entity>();
                }
            }
        }

        public List<Entity> TileAt(int x, int y)
        {
            if (x < 0 || x >= tiles.GetLength(0) || y < 0 || y >= tiles.GetLength(1))
            {
                return new List<Entity>();
            }

            return tiles[x, y];
        }

        public void FillTileSet()
        {
            foreach(Entity entity in m_entities.Values)
            {
                var position = entity.GetComponent<Components.Position>();
                tiles[position.x, position.y].Add(entity);
            }
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
