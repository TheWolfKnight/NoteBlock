using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Src.Models
{
    public class Note
    {
        public string Name;
        public int CopyCount;

        public string Contents;
        public DateTime CreationDate;
        public DateTime LastChange;

        
        /// <summary>
        /// Creates a instance of the Note Class, and sets the internal fields to the desired valuse
        /// </summary>
        /// <param name="name"> The name of the note </param>
        /// <param name="copyCount"> the amount of notes with the same name </param>
        /// <param name="contents"> The contents of the note </param>
        /// <param name="creationDate"> The time at which the note was created </param>
        public Note( string name, int copyCount, string contents, DateTime creationDate )
        {
            Name = name;
            CopyCount = copyCount;
            Contents = contents;
            CreationDate = creationDate;
            LastChange = creationDate;
        }


        /// <summary>
        /// Submits a change to the note, writing to the notes contents, and setting the LastChange field
        /// to the currenc date and time
        /// </summary>
        /// <param name="contents"> The new contents of the note </param>
        public void MakeChange( string contents )
        {
            LastChange = DateTime.Now;
            Contents = contents;
        }

    }
}
