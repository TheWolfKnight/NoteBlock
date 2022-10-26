using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Src
{

    public class TBD : Exception
    {
        public TBD() : base("To be done") { }
        public TBD(string msg) : base(msg) { }
        public TBD(string msg, Exception inner) : base(msg, inner) { }
    }

    public class UnrechableCode : Exception
    {
        public UnrechableCode() : base("This code should not be accessable") { }
        public UnrechableCode(string msg) : base(msg) { }
        public UnrechableCode(string msg, Exception inner) : base(msg, inner) { }
    }

    public class DublicatedEntryException : Exception
    {
        public DublicatedEntryException() : base() { }
        public DublicatedEntryException(string msg) : base(msg) { }
        public DublicatedEntryException( string msg, Exception inner ) : base(msg, inner) { }
    }

    public class InvalidDataException: Exception
    {
        public InvalidDataException() : base() { }
        public InvalidDataException(string msg) : base(msg) { }
        public InvalidDataException(string msg, Exception inner) : base(msg, inner) { }
    }

    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException() : base() { }
        public ElementNotFoundException(string msg) : base(msg) { }
        public ElementNotFoundException(string msg, Exception inner) : base(msg, inner) { }
    }

}
