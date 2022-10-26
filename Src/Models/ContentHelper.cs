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
        /// 
        /// </summary>
        /// <returns></returns>
        public int? GetTail()
        {
            return TailID;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ContentHelper? GetNext()
        {
            return Next;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tail"></param>
        public void SetTail( ContentHelper tail )
        {
            Next = tail;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetContent()
        {
            throw new TBD();
        }

    }
}
