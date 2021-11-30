using System;
using FluentAssertions;
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
        public void SequenceReturnsTheSumBetweenThemIgnoringComma(string input, int output)
        {
            StringCalculator stringCalculator = new (input);
            stringCalculator.Add().Should().Be(output, input);
        }
        
        
    }
}