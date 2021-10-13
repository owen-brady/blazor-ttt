using System;

namespace TicTacToe.Services
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }

        public void Write(string output)
        {
            Console.Write(output);
        }

        public string GetInput()
        {
            return Console.ReadLine();
        }
    }
}