using BabaIsYou.Particles;
using BabaIsYou.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Systems
{
    public class AnimationSystem : System
    {
        private List<Object> _animations = new();
        private List<Object> _removeAnimations = new();

        public void AddAnimation(Animation<Object> a)
        {
            _animations.Add(a);

        }
        public override void Update(GameTime gameTime)
        {

            foreach (Animation<Object> a in this._animations)
            {
                a.Update(gameTime);
                if (a.ShouldRemove())
                {
                    _removeAnimations.Add(a);
                }
            }
            foreach (Animation<Object> a in this._removeAnimations)
            {
                this._animations.Remove(a);
            }

            _removeAnimations.Clear();
        }
    }
}
