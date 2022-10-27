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

        public string Contents;
        public DateTime CreationDate;
        public DateTime LastChange;

        public uint ID { get; private set; }
        

        /// <summary>
        /// Creates a clean note with no details
        /// </summary>
        public Note() { }

        /// <summary>
        /// Creates a instance of the Note Class, and sets the internal fields to the desired valuse
        /// </summary>
        /// <param name="name"> The name of the note </param>
        /// <param name="copyCount"> The amount of notes with the same name </param>
        /// <param name="contents"> The contents of the note </param>
        /// <param name="creationDate"> The time at which the note was created </param>
        public Note( string name, string contents, DateTime creationDate )
        {
            Name = name;
            Contents = contents;
            CreationDate = creationDate;
            LastChange = creationDate;
        }


        /// <summary>
        /// Creates a new note from a database entry, this uses sets the ID of the note.
        /// </summary>
        /// <param name="id"> The ID given by the sql database </param>
        /// <param name="name"> The name of the note </param>
        /// <param name="copyCount"> The amount of notes with the same name </param>
        /// <param name="contents"> The contents of the note </param>
        /// <param name="creationDate"> The time at which the note was created </param>
        /// <param name="lastChangeDate"> The time at which the note was last updated </param>
        public Note(uint id, string name, string contents, DateTime creationDate, DateTime lastChangeDate )
        {
            ID = id;
            Name = name;
            Contents = contents;
            CreationDate = creationDate;
            LastChange = lastChangeDate;
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
