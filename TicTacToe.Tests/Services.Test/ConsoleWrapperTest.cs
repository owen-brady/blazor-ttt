using System;
using System.IO;
using TicTacToe.Services;
using Xunit;

namespace TicTacToe.Tests.Services.Test
{
    public class ConsoleWrapperTest
    {
        private readonly ConsoleWrapper _consoleWrapper;

        public ConsoleWrapperTest()
        {
            _consoleWrapper = new ConsoleWrapper();
        }

        [Fact]
        public void WriteLine_WritesLineWithOutput()
        {
            const string output = "test output";
            
            // Write console output to string writer for test comparison
            var sw = new StringWriter();
            Console.SetOut(sw);
                
            _consoleWrapper.WriteLine(output);

            var expected = $"{output}{Environment.NewLine}";
            
            Assert.Equal(expected, sw.ToString());
        }
        
        [Fact]
        public void Write_WritesLineWithOutput()
        {
            const string output = "test output";
            
            // Write console output to string writer for test comparison
            var sw = new StringWriter();
            Console.SetOut(sw);
                
            _consoleWrapper.Write(output);

            Assert.Equal(output, sw.ToString());
        }

        [Fact]
        public void GetInput_GetsExpectedInput()
        {
            const string input = "test input";
            
            // Supply console with input
            var sr = new StringReader(input);
            Console.SetIn(sr);

            var actual = _consoleWrapper.GetInput();
            
            Assert.Equal(input, actual);
        }
    }
}