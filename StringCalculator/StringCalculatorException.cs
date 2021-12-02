using System;

namespace StringCalculator
{
    public class NegativeNotAllowed : Exception
    {
        public NegativeNotAllowed(string message)
            : base(message) { }
    }
    
    public class SequenceNotValid : Exception
    {
        public SequenceNotValid(string message)
            : base(message) { }
    }

}