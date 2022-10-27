using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Src.Models
{
    class TextItemTag
    {
        public bool IsChanged { get; set; }
        public bool IsSaved { get; set; }

        public TextItemTag( bool isChanged, bool isSaved )
        {
            IsChanged = isChanged;
            IsSaved = isSaved;
        }

        
        public void Reset()
        {
            IsSaved = false;
            IsChanged = false;
        }


        public override string ToString()
        {
            return $"IsChange={IsChanged};IsSaved={IsSaved}";
        }

    }
}
