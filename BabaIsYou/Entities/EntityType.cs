using BabaIsYou.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Entities
{
    abstract public class EntityType
    {
        public List<Entity> entities;
        public string fileLetter;
        public NounType nounType;
        protected Game1 _game;
        public EntityType(Game1 game) 
        {
            this._game = game;
        }
        virtual public Entity CreateEntity(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
