using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Systems
{
    public class LevelReader
    {
        private String _fileName;
        public LevelReader(String fileName) 
        {
            this._fileName = fileName;
            this.ReadFile();
        }

        private void ReadFile()
        {
            var fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    Debug.Print(line);
                }
            }
        }
    }
}
