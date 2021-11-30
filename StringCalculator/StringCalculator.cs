using System;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator
    {
        private string _sequence;

        public StringCalculator(string sequence)
            => _sequence = sequence;

        public int Add()
        {
            if (string.IsNullOrEmpty(_sequence))
                return 0;
            
            return _sequence.Split(new char[] { '\n', ',' }).Sum(x => Int32.Parse(x));
        }
    } 
}