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
        public EntityType() { }
        virtual public Entity CreateEntity(Game1 game)
        {
            throw new NotImplementedException();
        }
    }
}
