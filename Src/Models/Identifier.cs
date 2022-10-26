using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Src.Models
{
    public class Identifier
    {

        public string Name;
        public int Count;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        public Identifier( string name, int count )
        {
            Name = name;
            Count = count;
        }

        
        public override string ToString()
        {
            return $"{Name}:{Count}";
        }

    }
}
