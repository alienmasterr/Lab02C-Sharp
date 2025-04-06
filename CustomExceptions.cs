using System;

namespace Lab_CSharp.Models
{
    public class FutureBirthDateException : Exception
    {
        public FutureBirthDateException(string message) : base(message) { }
    }

    public class TooOldPersonException : Exception
    {
        public TooOldPersonException(string message) : base(message) { }
    }

    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message) { }
    }
}

