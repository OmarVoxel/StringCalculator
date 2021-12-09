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
            stringCalculator.Add().Should().Be(output);
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
            stringCalculator.Add().Should().Be(output);
        }
        
        [Theory]
        [InlineData("1\n2,0", 3)]
        [InlineData("2,3\n3,0,1\n3", 12)]
        public void SequenceReturnsTheSumBetweenThemIgnoringCommaAndNewLines(string input, int output)
        {
            StringCalculator stringCalculator = new (input);
            stringCalculator.Add().Should().Be(output);
        }
        
        [Theory]
        [InlineData("//;1;1", 2)]
        [InlineData("//.2.3", 5)]
        [InlineData("//;1\n2;0", 3)]
        [InlineData("//+2+3\n3+0+1\n3", 12)]
        public void SequenceReturnsTheSumBetweenThemIgnoringTheCustomSeparator(string input, int output)
        {
            StringCalculator stringCalculator = new (input);
            stringCalculator.Add().Should().Be(output);
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
            foo.Should().Throw<NegativeNotAllowed>().WithMessage(message);
        }

        [Theory]
        [InlineData("//;1;1;3;1;34;1001;2000", 40)]
        [InlineData("//;1;1;3;10000;34;1001;2000;3000;44000", 39)]
        [InlineData("//!1!2000!3!5!6\n8000!9000!1000", 1015)]
        public void IgnoringNumbersGreaterThan1000(string input, int outpout)
        {
            StringCalculator stringCalculator = new (input);
            stringCalculator.Add().Should().Be(outpout);
        }
        
        [Theory]
        [InlineData("//[!!!]\n1!!!1!!!3!!!1!!!34!!!1001!!!2000", 40)]
        [InlineData("//[ss]\n1ss1ss3ss1ss34ss1001ss2000", 40)]
        [InlineData("//[ss124!1]\n1ss124!11ss124!13ss124!11ss124!134ss124!11001ss124!12000", 40)]
        public void AllowSeparatorWithMoreThanOneCharacter(string input, int output)
        {
            StringCalculator stringCalculator = new (input);
            stringCalculator.Add().Should().Be(output);
        }
    }
}