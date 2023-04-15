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
        private int _tilesW;
        private int _tilesH;
        private string _name;
        private List<Entity> _removeThese = new();
        private List<Entity> _addThese = new();
        private Stack<List<Entity>> undos = new();
        private List<Entity> _previousState = new();
        private List<Entity> _initialState = new();

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
            this.PushUndoLayer();
            this.SetPreviousState(this.m_entities.Values.ToList());
        }
        private List<Entity> CopyEntities(List<Entity> entities)
        {
            List<Entity> newEntities = new List<Entity>();
            foreach (Entity entity in entities)
            {
                newEntities.Add(entity.DeepClone());

            }
            return newEntities;
        }
        public void AddUndoLayer(List<Entity> entities)
        {
            this.undos.Push(this.CopyEntities(entities));
        }
        public void SetInitialState(List<Entity> entities)
        {
            _initialState = this.CopyEntities(entities);
        }
        public void Start()
        {
            this.SetPreviousState(m_entities.Values.ToList());
            SetInitialState(this.m_entities.Values.ToList());
        }
        private void PushUndoLayer()
        {
            this.undos.Push(new List<Entity>(this._previousState));
        }
        private void SetPreviousState(List<Entity> entities)
        {
            _previousState.Clear();
            foreach (Entity entity in entities)
            {
                _previousState.Add(entity.DeepClone());
            }
        }

        public void Undo()
        {
            if (this.undos.Count <= 0) {
                return;
            }
            _removeThese = m_entities.Values.ToList();
            _addThese = this.undos.Pop();
            this.SetPreviousState(_addThese);
        }
        public void Reset()
        {
            this.undos.Clear();
            _removeThese = m_entities.Values.ToList();
            _addThese = this._initialState;
            SetInitialState(this._initialState.ToList());

        }
        public List<Entity> RemoveThese()
        {
            return this._removeThese;
        }
        public List<Entity> AddThese()
        {
            return this._addThese;
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
