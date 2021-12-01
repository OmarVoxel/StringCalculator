using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Xunit;

namespace StringCalculator.Tests
{
    public class StringCalculatorTests
    {
        [Fact]
        public void EmtpySequenceReturns0()
        {
            StringCalculator stringCalculator = new ("");
            stringCalculator.Add().Should().Be(0);
        }
        
        [Theory]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        [InlineData("3", 3)]
        [InlineData("4", 4)]
        public void SequenceReturns(string input, int output)
        {
            StringCalculator stringCalculator = new (input);
            stringCalculator.Add().Should().Be(output, input);
        }
        
        [Theory]
        [InlineData("1,1", 2)]
        [InlineData("2,3", 5)]
        [InlineData("11,3", 14)]
        [InlineData("1,22", 23)]
        [InlineData("11,22", 33)]
        [InlineData("11,22,11", 44)]
        public void SequenceReturnsTheSumBetweenThemIgnoringComma(string input, int output)
        {
            StringCalculator stringCalculator = new (input);
            stringCalculator.Add().Should().Be(output, input);
        }
        
        [Theory]
        [InlineData("1\n2,0", 3)]
        [InlineData("2,3\n3,0,1\n3", 12)]
        public void SequenceReturnsTheSumBetweenThemIgnoringCommaAndNewLines(string input, int output)
        {
            StringCalculator stringCalculator = new (input);
            stringCalculator.Add().Should().Be(output, input);
        }
        
        [Theory]
        [InlineData("//;1;1", 2)]
        [InlineData("//.2.3", 5)]
        [InlineData("//;1\n2;0", 3)]
        [InlineData("//+2+3\n3+0+1\n3", 12)]
        public void SequenceReturnsTheSumBetweenThemIgnoringTheCustomSeparator(string input, int output)
        {
            StringCalculator stringCalculator = new (input);
            stringCalculator.Add().Should().Be(output, input);
        }
        
        [Theory]
        [InlineData("//;-1;-1;3;1;34;-1", "negatives not allowed -1,-1,-1")]
        [InlineData("//.-2.3.-0", "negatives not allowed -2,-0")]
        [InlineData("//;1\n-2;0", "negatives not allowed -2")]
        //[InlineData("//--1\n--2;-0", "negatives not allowed -1,-2")] this test need to be fixed yet.
        
        public void NegativeNotAllowed(string input, string message)
        {
            StringCalculator stringCalculator = new (input);
            
            Action foo = () => stringCalculator.Add();
            foo.Should().Throw<ArgumentException>().WithMessage(message);
        }
        
    }
}