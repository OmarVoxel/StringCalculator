using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator
    {
        private string _sequence;
        private List<string> _separator = new List<string>() { "\n" , ","} ;

        public StringCalculator(string sequence)
            => _sequence = sequence;

        public int Add()
        {
            if (_sequence.IndexOf(",\n") != -1 || _sequence.IndexOf("\n,") != -1)
                throw new SequenceNotValid("Sequence not valid");
            
            if (string.IsNullOrEmpty(_sequence))
                return 0;

            bool separatorExists = Regex.IsMatch(_sequence, @"^\/\/.\d");
            bool longSeparator = _sequence.IndexOf('[') != -1 && _sequence.IndexOf(']') != -1;

            if (longSeparator)
            {
                Regex regex = new Regex(@"\[(.*?)\]");
                foreach (Match match in regex.Matches(_sequence))
                {
                    _separator.Add(match.Groups[1].Value);
                }

                _sequence = _sequence.Substring(_sequence.IndexOf("\n") );
            }
            
            if (separatorExists) {
                _separator.Add(_sequence[2].ToString());
                _sequence = _sequence.Substring(3);
            }

            if (!_separator.Contains("-"))
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
            
            return _sequence.Split(_separator.ToArray(), StringSplitOptions.RemoveEmptyEntries).
                Where(x => Int32.Parse(x) <= 1000 ).
                Sum(x => Int32.Parse(x));
        }
    } 
}