using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BabaIsYou.Entities;
using BabaIsYou.Entities.Things;
using BabaIsYou.Entities.Words;
using Microsoft.Xna.Framework;

namespace BabaIsYou.Systems
{
    public class LevelReader : System
    {
        private String _fileName;
        private Game1 _game;
        private Dictionary<String, EntityType> levelLookup;
        private int _tilesW;
        private int _tilesH;
        public LevelReader(Game1 game, String fileName) 
        {
            this._game = game;
            this._fileName = fileName;

            this.levelLookup = new Dictionary<string, EntityType> {
                { "w", new WallET(this._game) },
                { "r", new RockET(this._game) },
                { "f", new FlagET(this._game) },
                { "b", new BabaET(this._game) },
                { "l", new FloorET(this._game) },
                { "g", new GrassET(this._game) },
                { "a", new WaterET(this._game) },
                { "v", new LavaET(this._game) },
                { "h", new HedgeET(this._game) },

                { "W", new WordWallET(this._game) },
                { "R", new WordRockET(this._game) },
                { "F", new WordFlagET(this._game) },
                { "B", new WordBabaET(this._game) },
                { "I", new WordIsET(this._game) },
                { "S", new WordStopET(this._game) },
                { "P", new WordPushET(this._game) },
                { "V", new WordLavaET(this._game) },
                { "A", new WordWaterET(this._game) },
                { "Y", new WordYouET(this._game) },
                { "X", new WordWinET(this._game) },
                { "N", new WordSinkET(this._game) },
                { "K", new WordKillET(this._game) },
            };
        }

        public Level ReadLevel(String levelName)
        {
            List<String> strings = new List<String>();

            var fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
            Level level = new Level();
            int lineNumber = 0;
            bool inLevel = false;
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                strings = File.ReadAllLines(_fileName).ToList();
                for (int i = 0; i < strings.Count; i++)
                {
                    string currentLine = strings[i];
                    string nextLine = i + 1 < strings.Count ? strings[i + 1] : null; 

                    if (inLevel && nextLine != null)
                    {
                        if (Regex.Match(nextLine, @"\d+\s*x\s*\d+").Success)
                        {
                            return level;
                        }
                    }
                    if (strings[i] == levelName)
                    {
                        inLevel = true;
                    }
                    if (inLevel)
                    {
                        ProcessLine(level, strings[i], lineNumber);
                        lineNumber++;
                    }

                }
            }
            return level;
        }

        public List<String> ReadLevelSelect()
        {
            List<String> strings = new List<String>();

            var fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
            List<String> levelNames = new List<String>();

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                strings = File.ReadAllLines(_fileName).ToList();
                for (int i = 0; i < strings.Count; i++)
                {
                    string currentLine = strings[i];
                    string nextLine = i + 1 < strings.Count ? strings[i + 1] : null;

                    if (nextLine != null)
                    {
                        if (Regex.Match(nextLine, @"\d+\s*x\s*\d+").Success)
                        {
                            levelNames.Add(currentLine);
                        }
                    }
                }
                return levelNames;
            }
        }
        private EntityType GetEntityType(String entityString)
        {
            return this.levelLookup[entityString];
        }
        private void ProcessLine(Level level, String line, int lineNumber)
        {
            if (lineNumber == 0)
            {
                level.SetName(line);
                return;
            }
            if (lineNumber == 1)
            {

                string[] words = line.Split(" x ");
                this._tilesW = Int32.Parse(words[0]);
                this._tilesH = Int32.Parse(words[1]);
                level.InitializeTileSet(this._tilesW, this._tilesH);
                return;
            }
            int column = 0;
            foreach (char c in line)
            {
                if (c != ' ')
                {
                    EntityType et = this.GetEntityType(c.ToString());
                    Entity entity = et.CreateEntity(column, (lineNumber - 2) % this._tilesH);

                    level.Add(entity);
                }

                column++;
            }
        }
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
