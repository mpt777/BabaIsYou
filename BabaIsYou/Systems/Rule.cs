using BabaIsYou.Components;
using BabaIsYou.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Systems
{
    public class Rule : System
    {
        private Tileset _tileSet; 
        
        public Rule(Tileset tileSet)
        {
            this._tileSet = tileSet;
        }
        public override void Update(GameTime gameTime)
        {
            ClearRules();
            UpdateRules();
        }

        public void UpdateRules()
        {
            for (int i = 0; i < _tileSet.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < _tileSet.tiles.GetLength(1); j++)
                {
                    StartSentence(i, j);
                }
            }
        }
        private void StartSentence(int x, int y)
        {
            List<Entity> entities = _tileSet.tiles[x, y];
            foreach (Entity entity in entities)
            {
                AddToSentence(x, y, 1, 0, new Sentence());
                AddToSentence(x, y, 0, 1, new Sentence());
            }
        }
        private void AddToSentence(int x, int y, int xIncrement, int yIncrement, Sentence sentence)
        {
            List<Entity> entities = _tileSet.TileAt(x, y);
            if (_tileSet.TileAt(x, y).Count > 0)
            {
                foreach (Entity entity in entities)
                {
                    if (!entity.HasComponent<Text>())
                    {
                        continue;
                    }
                    Text text = entity.GetComponent<Text>();
                    sentence.AddWord(text);

                    ProcessSentence(sentence);
                    AddToSentence(x + xIncrement, y + yIncrement, xIncrement, yIncrement, sentence);
                    
                }
            }

        }

        private void ProcessSentence(Sentence sentence)
        {
            if (sentence.words.Count >= 3)
            {
                if (sentence.words[1].verbType == null) { return; }

                if (sentence.words[0].nounType != null && sentence.words[2].propertyType != null)
                {
                    UpdateRule((NounType)sentence.words[0].nounType, (PropertyType)sentence.words[2].propertyType);
                    return;
                }
                if (sentence.words[0].nounType != null && sentence.words[2].nounType != null)
                {
                    UpdateRule((NounType)sentence.words[0].nounType, (PropertyType)sentence.words[2].nounType);
                    return;
                }
            }
        }

        private void ClearRules()
        {
            foreach (Entity entity in m_entities.Values)
            {
                if (entity.HasComponent<Property>() && entity.HasComponent<Noun>()) {
                    if (entity.GetComponent<Noun>().nounType != NounType.BigBlue && entity.GetComponent<Noun>().nounType != NounType.Text)
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
                property.propertyType = propertyType;

            }
        }
        private void UpdateRule(NounType nounType1, NounType nounType2)
        {
            foreach (Entity entity in m_entities.Values)
            {

            }
        }
    }
}
