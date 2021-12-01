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
                throw new Exception("Sequence not valid");

            if (string.IsNullOrEmpty(_sequence))
                return 0;

            string[] negativeNumbers = _sequence.Split('-');
            
            if (negativeNumbers.Length > 0)
            {
                string messageException = "";
                foreach (string number in negativeNumbers)
                    messageException = (Int32.Parse(number) * -1).ToString();
                
                throw new Exception($"Negative(s) not allowed: {messageException}");
            }
                

            bool separatorExists = Regex.IsMatch(_sequence, @"^\/\/.\d");

            if (separatorExists) {
                _separator = _sequence[2];
                _sequence = _sequence.Substring(3);
            }
            
            return _sequence.Split(new char[] { '\n', _separator }).Sum(x => Int32.Parse(x));
        }
    } 
}