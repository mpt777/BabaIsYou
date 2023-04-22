using BabaIsYou.Components;
using BabaIsYou.Entities;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Systems
{
    public class PropertyTypeManager
    {
        private Components.PropertyType _propertyType;
        private SoundEffect _soundEffect;
        private HashSet<NounType> _nounTypes = new();
        private HashSet<NounType> _prevNounTypes = new();
        public delegate void ParticleCallback(Entity entity);

        private ParticleCallback _particleCallback;
        public PropertyTypeManager(Components.PropertyType p, ParticleCallback particleCallback)
        {
            _propertyType = p;
            _particleCallback = particleCallback;
        }
        public PropertyTypeManager(Components.PropertyType p, SoundEffect s, ParticleCallback particleCallback) 
        {
            _propertyType = p;
            _soundEffect = s;
            _particleCallback = particleCallback;
        }
        public PropertyType PropertyType()
        {
            return _propertyType;
        }
        public void PlaySound()
        {
            if (_soundEffect != null)
            {
                _soundEffect.Play();
            }
        }
        public void PlayParticles(Entity entity)
        {
            _particleCallback(entity);
        }
        public void AddNounType(NounType nounType)
        {
            this._nounTypes.Add(nounType);
        }
        public void Start()
        {
            _prevNounTypes.UnionWith(_nounTypes);
            _nounTypes.Clear();
        }
        public void Process(List<Entity> entities)
        {
            if (!_nounTypes.SetEquals(_prevNounTypes))
            {
                PlaySound();
                foreach (Entity entity in entities)
                {
                    if (!entity.HasComponent<Noun>()) { continue; }
                    var noun = entity.GetComponent<Noun>();
                    if (!_nounTypes.Contains(noun.nounType)) { continue; }
                    PlayParticles(entity);
                }
            }
            _prevNounTypes.Clear();
            _prevNounTypes.UnionWith(_nounTypes);
            _nounTypes.Clear();
        }
    }
}
