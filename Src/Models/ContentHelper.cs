using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Src.Models
{
    public class ContentHelper
    {
        #nullable enable
        public int ID;
        // will only containt up to 255 chars
        public string Content;
        public int? TailID;

        public ContentHelper? Next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <param name="tailID"></param>
        public ContentHelper( int id, string content, int? tailID )
        {
            ID = id;
            Content = content;
            TailID = tailID;

            Next = null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <param name="tailID"></param>
        /// <param name="tail"></param>
        public ContentHelper(int id, string content, int? tailID, ContentHelper tail)
        {
            ID = id;
            Content = content;
            TailID = tailID;
            Next = tail;
        }


        /// <summary>
        /// Gets the current tail ID, used when determeining if any data can still be gotten from the database
        /// </summary>
        /// <returns> An integer number representing the tail ID of the current head </returns>
        public int? GetTail()
        {
            return TailID;
        }


        /// <summary>
        /// Returns the current tile for the ContentHelper
        /// </summary>
        /// <returns> Either a ContentHelper instance, if any is avaliable, or null if not </returns>
        public ContentHelper? GetNext()
        {
            return Next;
        }


        /// <summary>
        /// Sets the tail for the current ContentHelper
        /// </summary>
        /// <param name="tail"> The next ContentHelper in line </param>
        public void SetTail( ContentHelper tail )
        {
            Next = tail;
        }


        /// <summary>
        /// Uses recursion to get all the content from the ContentHelper instancec for the current note
        /// </summary>
        /// <returns> A string representation of the current notes content </returns>
        public string GetContent()
        {
            // Sets the result variable to be equal the content of the current helper.
            string r = Content;

            // Get the value from the GetNext method
            ContentHelper? h = GetNext();

            // Checks if the holder variable is equal to null
            // if so, returs the content
            if ( h == null )
                return r;

            // If the holder variable is not null, the content of the next ContentHelper is
            // appended to the result variable and returend to the caller
            r += h.GetContent();
            return r;
        }

    }
}
