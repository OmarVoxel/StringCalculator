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
    }
}