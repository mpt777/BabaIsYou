using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
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
        private Level _level;
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
            this.ReadFile();
            Debug.Print("Here");
        }
        public Level Level()
        {
            return this._level;
        }

        private void ReadFile()
        {
            var fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                int lineNumber = 0;
                this._level = new Level();
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    ProcessLine(line, lineNumber);
                    lineNumber++;

                }
            }
        }
        private EntityType GetEntityType(String entityString)
        {
            return this.levelLookup[entityString];
        }
        private void ProcessLine(String line, int lineNumber)
        {
            if (lineNumber == 0)
            {
                this._level.SetName(line);
                return;
            }
            if (lineNumber == 1)
            {

                string[] words = line.Split(" x ");
                this._tilesW = Int32.Parse(words[0]);
                this._tilesH = Int32.Parse(words[1]);
                this._level.InitializeTileSet(this._tilesW, this._tilesH);
                return;
            }
            int column = 0;
            foreach (char c in line)
            {
                if (c != ' ')
                {
                    EntityType et = this.GetEntityType(c.ToString());
                    Entity entity = et.CreateEntity(column, (lineNumber - 2) % this._tilesH);

                    Add(entity);
                }

                column++;
            }
        }
        public List<Entity> Entities()
        {
            return m_entities.Values.ToList();
        }
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
