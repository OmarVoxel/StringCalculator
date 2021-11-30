using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator
    {
        private string _sequence;

        public StringCalculator(string sequence)
            => _sequence = sequence;

        public int Add()
        {
            if (_sequence.IndexOf(",\n") != -1 || _sequence.IndexOf("\n,") != -1)
                throw new Exception("Sequence not valid");

            if (string.IsNullOrEmpty(_sequence))
                return 0;

            bool separatorExists = Regex.IsMatch(_sequence, @"^\/\/.\d");
            char separator = ',';

            if (separatorExists) {
                separator = _sequence[2];
                _sequence = _sequence.Substring(3);
            }
            
            return _sequence.Split(new char[] { '\n', separator }).Sum(x => Int32.Parse(x));
        }
    } 
}