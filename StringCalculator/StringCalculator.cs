using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator
    {
        private string _sequence;
        private readonly List<string> _separator = new List<string>() { "\n" , ","} ;

        public StringCalculator(string sequence)
            => _sequence = sequence;

        public int Add()
        {
            if (_sequence.IndexOf(",\n", StringComparison.Ordinal) != -1 || 
                _sequence.IndexOf("\n,", StringComparison.Ordinal) != -1)
                throw new SequenceNotValid("Sequence not valid");
            
            if (string.IsNullOrEmpty(_sequence))
                return 0;

            bool isCustomSeparator = Regex.IsMatch(_sequence, @"^\/\/.\d");
            
            if (isCustomSeparator) {
                _separator.Add(_sequence[2].ToString());
                _sequence = _sequence[3..];
            }
            
            Regex containsDelimitersInBrackets = new Regex(@"\[(.*?)\]");
            bool isLongSeparator = containsDelimitersInBrackets.IsMatch(_sequence);
            
            if (isLongSeparator)
            {
                _separator.AddRange(containsDelimitersInBrackets
                    .Matches(_sequence)
                    .Cast<Match>()
                    .Select(x => x.Groups[1].Value)
                );
                
                _sequence = _sequence[_sequence.IndexOf("\n", StringComparison.Ordinal)..];
            }
            
            Regex negativeNumbers = new Regex(@"-\d+");
            bool containsNegativeNumbers = negativeNumbers.IsMatch(_sequence);
            
            if (containsNegativeNumbers)
            {
                string message = negativeNumbers.Matches(_sequence)
                    .Select(match => match.Value)
                    .Aggregate(( a, b ) => a + "," + b);
                    
                throw new NegativeNotAllowed($"negatives not allowed {message}");
            }
            
            return _sequence.Split(_separator.ToArray(), StringSplitOptions.RemoveEmptyEntries).
                Where(x => Int32.Parse(x) <= 1000 ).
                Sum(x => Int32.Parse(x));
        }
    } 
}