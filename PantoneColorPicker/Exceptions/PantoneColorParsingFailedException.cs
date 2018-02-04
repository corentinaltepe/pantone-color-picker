using System;
namespace PantoneColorPicker.Exceptions
{
    class PantoneColorParsingFailedException : Exception
    {
        public PantoneColorParsingFailedException() { }
        public PantoneColorParsingFailedException(string message) : base(message) { }
        public PantoneColorParsingFailedException(string message, Exception inner) : base(message, inner) { }
    }
}
