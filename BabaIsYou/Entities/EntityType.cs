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
        public EntityType() { }
        virtual public Entity CreateEntity(Game1 game, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
