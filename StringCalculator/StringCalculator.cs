using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator
    {
        private string _sequence;
        private char _separator = ',';

        public StringCalculator(string sequence)
            => _sequence = sequence;

        public int Add()
        {
            if (_sequence.IndexOf(",\n") != -1 || _sequence.IndexOf("\n,") != -1)
                throw new SequenceNotValid("Sequence not valid");
            if (string.IsNullOrEmpty(_sequence))
                return 0;

            bool separatorExists = Regex.IsMatch(_sequence, @"^\/\/.\d");

            if (separatorExists) {
                _separator = _sequence[2];
                _sequence = _sequence.Substring(3);
            }

            if (_separator != '-')
            {
                Regex regex = new Regex(@"-\d+");
                if (regex.IsMatch(_sequence))
                {
                    string message = regex.Matches(_sequence)
                        .Select(match => match.Value)
                        .Aggregate(( a, b ) => a + "," + b);
                    
                    throw new NegativeNotAllowed($"negatives not allowed {message}");
                }
            }
            
            return _sequence.Split(new char[] { '\n', _separator }).
                Where(x => Int32.Parse(x) <= 1000 ).
                Sum(x => Int32.Parse(x));
        }
    } 
}