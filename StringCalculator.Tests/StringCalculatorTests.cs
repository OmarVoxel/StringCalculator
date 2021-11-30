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
        
        [Fact]
        public void SequenceReturns1()
        {
            StringCalculator stringCalculator = new ("1");
            stringCalculator.Add().Should().Be(1);
        }
        
    }
}