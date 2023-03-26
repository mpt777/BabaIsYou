using BabaIsYou.Components;
using BabaIsYou.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Systems
{
    public class Sentence
    {
        public List<Text> words = new();
        public Sentence() 
        {
        }

        public void AddWord(Text word)
        {
            words.Add(word);
        }

    }
}
