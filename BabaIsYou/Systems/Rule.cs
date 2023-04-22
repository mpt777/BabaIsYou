using BabaIsYou.Components;
using BabaIsYou.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Systems
{
    public class Rule : System
    {
        private Level _level;
        private Game1 _game;

        ContentManager contentManager;
        private SoundEffect _newWinEffect;

        private ParticleSystem _particleSystem;

        private List<PropertyTypeManager> _propertyTypeManagers = new();

        public Rule(Game1 game, Level level, ContentManager content, ParticleSystem particleSystem)
        {
            this._level = level;
            this._game = game;
            this.contentManager = content;
            this._particleSystem = particleSystem;
            this.LoadContent();
            _propertyTypeManagers.Add(new PropertyTypeManager(PropertyType.Win, this._newWinEffect, _particleSystem.ObjectIsWin));
            _propertyTypeManagers.Add(new PropertyTypeManager(PropertyType.You, _particleSystem.ObjectIsYou));
        }
        public override void Update(GameTime gameTime)
        {
            ClearRules();
            UpdateRules();

            ProcessNounTypes();
        }
        public void Start() 
        {
            UpdateRules();
            StartNounTypes();
        }
        public void StartNounTypes()
        {
            foreach (PropertyTypeManager p in _propertyTypeManagers)
            {
                p.Start();
            }
        }
        public void ProcessNounTypes()
        {
            foreach(PropertyTypeManager p in _propertyTypeManagers)
            {
                p.Process(m_entities.Values.ToList());
            }
        }
        public void AddNounType(PropertyType propertyType, NounType nounType)
        {
            foreach (PropertyTypeManager p in _propertyTypeManagers)
            {
                if (p.PropertyType() == propertyType)
                {
                    p.AddNounType(nounType);
                }
            }
        }

        private void LoadContent()
        {
            this._newWinEffect = contentManager.Load<SoundEffect>("Sounds/new_win");
        }

        public void UpdateRules()
        {
            for (int i = 0; i < _level.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < _level.tiles.GetLength(1); j++)
                {
                    StartSentence(i, j);
                }
            }
        }
        private void StartSentence(int x, int y)
        {
            List<Entity> entities = _level.tiles[x, y];
            foreach (Entity entity in entities)
            {
                AddToSentence(x, y, 1, 0, new Sentence());
                AddToSentence(x, y, 0, 1, new Sentence());
            }
        }
        private void AddToSentence(int x, int y, int xIncrement, int yIncrement, Sentence sentence)
        {
            List<Entity> entities = _level.TileAt(x, y);
            if (_level.TileAt(x, y).Count > 0)
            {
                foreach (Entity entity in entities)
                {
                    if (!entity.HasComponent<Text>())
                    {
                        continue;
                    }
                    Text text = entity.GetComponent<Text>();
                    sentence.AddWord(text);
                    if (sentence.words.Count == 3)
                    {
                        ProcessSentence(sentence);
                        return;
                    }
                    AddToSentence(x + xIncrement, y + yIncrement, xIncrement, yIncrement, sentence);
                    
                }
            }
            else
            {
                ProcessSentence(sentence);
            }
            
        }

        private void ProcessSentence(Sentence sentence)
        {
            if (sentence.words.Count == 3)
            {
                if (sentence.words[1].verbType == null) { return; }

                if (sentence.words[0].nounType != null && sentence.words[2].propertyType != null)
                {
                    UpdateRule((NounType)sentence.words[0].nounType, (PropertyType)sentence.words[2].propertyType);
                    return;
                }
                if (sentence.words[0].nounType != null && sentence.words[2].nounType != null)
                {
                    UpdateRule((NounType)sentence.words[0].nounType, (NounType)sentence.words[2].nounType);
                    return;
                }
            }
        }

        private void ClearRules()
        {
            foreach (Entity entity in m_entities.Values)
            {
                if (entity.HasComponent<Property>() && entity.HasComponent<Noun>()) {
                    var property = entity.GetComponent<Property>();
                    if (!property.isDefault)
                    {
                        entity.GetComponent<Property>().Clear();
                    }
                }
            }

        }
        
        private void UpdateRule(NounType nounType, PropertyType propertyType)
        {
            foreach (Entity entity in m_entities.Values)
            {
                if (!entity.HasComponent<Noun>()) { continue; }

                if (!entity.HasComponent<Property>()) { continue; }

                var noun = entity.GetComponent<Noun>();
                if (noun.nounType != nounType) { continue; }

                var property = entity.GetComponent<Property>();
                property.AddPropertyType(propertyType);


                AddNounType(propertyType, nounType);


            }
        }
        private void UpdateRule(NounType nounType1, NounType nounType2)
        {
            foreach (Entity entity in m_entities.Values)
            {
                if (!entity.HasComponent<Noun>()) { continue; }
                var noun = entity.GetComponent<Noun>();
                if (noun.nounType != nounType1) { continue; }
                var position = entity.GetComponent<Position>();


                Entity newEntity = this._game.nounTypeLookup[nounType2].CreateEntity(position.x, position.y);
                _addThese.Add(newEntity);
                _removeThese.Add(entity);

            }
        }
    }
}
