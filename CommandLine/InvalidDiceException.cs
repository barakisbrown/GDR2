using Spectre.Console;

namespace CommandLine
{
    public class InvalidDiceException : Exception
    {
        public InvalidDiceException() 
        {
            AnsiConsole.MarkupLine("[RED]ERROR[/] has occured. Wrong Color and Illegal Charater was entered.");
            AnsiConsole.WriteLine("Please check that you entered is legal.");
            CommandLine.DisplayColors();
        }

        public InvalidDiceException(string message) : base(message)
        {
        }

        public InvalidDiceException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
