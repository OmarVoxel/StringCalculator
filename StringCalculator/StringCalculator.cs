using System;

namespace StringCalculator
{
    public class StringCalculator
    {
        private string _sequence;

        public StringCalculator(string sequence)
            => _sequence = sequence;

        public int Add()
        {
            if(!string.IsNullOrEmpty(_sequence))
                return Int32.Parse(_sequence);

            return 0;
        }
    } 
}