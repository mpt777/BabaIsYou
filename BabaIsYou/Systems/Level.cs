using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BabaIsYou.Entities;
using Microsoft.Xna.Framework;

namespace BabaIsYou.Systems
{
    public class Level : System
    {
        //private int width;
        //private int height;
        //public int tileSize;
        private int _tilesW;
        private int _tilesH;
        private string _name;

        public List<Entity>[,] tiles;

        public Level() { }

        public void InitializeTileSet(int tilesW, int tilesH)
        {
            _tilesW = tilesW;
            _tilesH = tilesH;

            tiles = new List<Entity>[tilesW, tilesH];
            for (int i = 0; i < tilesW; i++)
            {
                for (int j = 0; j < tilesH; j++)
                {
                    tiles[i, j] = new List<Entity>();
                }
            }
        }
        public void ClearTileSet()
        {

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(0); j++)
                {
                    tiles[i, j].Clear();
                }
            }
        }

        public void SetName(string name)
        {
            _name = name;
        }
        public int Width()
        {
            return _tilesW;
        }
        public int Height()
        {
            return _tilesH;
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
            foreach (Entity entity in m_entities.Values)
            {
                var position = entity.GetComponent<Components.Position>();
                AddEntity(entity, position.x, position.y);
            }
        }

        public void AddEntity(Entity e, int x, int y)
        {
            tiles[x, y].Add(e);
        }
        public Entity RemoveEntity(Entity e, int x, int y)
        {
            Entity foundEntity = tiles[x, y].Find(entity => entity == e);
            tiles[x, y].Remove(foundEntity);
            return foundEntity;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

    }
}
