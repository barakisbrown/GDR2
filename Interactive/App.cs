

using Core.Dice;
using Core.Dice.Good;
using Spectre.Console;

namespace Interactive
{
    public class App
    {
        private Pool _Dice = new Pool();
        public App()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLineInterpolated($"[underline on Red]{Questions.ABOUT} [/]");
            AnsiConsole.MarkupLineInterpolated($"[red]{Questions.COPYRIGHT}[/]");

            int boost = askQuestion(Questions.BOOST);
            int setback = askQuestion(Questions.SETBACK);
            int ability = askQuestion(Questions.ABILITY);
            int difficulty = askQuestion(Questions.DIFFICULTY);
            int proficency = askQuestion(Questions.PROFICENCY);
            int challenge = askQuestion(Questions.CHALLENGE);


            // ADD DICE TO DICE POOL

        }
        
        int askQuestion(string question)
        {
            return AnsiConsole.Ask<int>(question,0);
        }
    }
}
