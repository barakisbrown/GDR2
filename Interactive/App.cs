using Core.Dice;
using Core.Dice.Bad;
using Core.Dice.Good;
using Spectre.Console;

namespace Interactive
{
    public class App
    {
        private Pool _Dice = new Pool();
        private Dictionary<Colors, int> scores = new Dictionary<Colors, int>();
        public App()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLineInterpolated($"[underline Red]{Questions.ABOUT} [/]");
            AnsiConsole.MarkupLineInterpolated($"[red]{Questions.COPYRIGHT}[/]");
            AnsiConsole.WriteLine();
            // Set Scores to number of dice rolled if NOT 0
            scores.Add(Colors.BLUE, askQuestion(Questions.BOOST));
            scores.Add(Colors.BLACK, askQuestion(Questions.SETBACK));
            scores.Add(Colors.GREEN, askQuestion(Questions.ABILITY));
            scores.Add(Colors.PURPLE, askQuestion(Questions.DIFFICULTY));
            scores.Add(Colors.YELLOW, askQuestion(Questions.PROFICENCY));
            scores.Add(Colors.RED, askQuestion(Questions.CHALLENGE));
            // ADD Scores to the Dice Pool
            foreach(var score in scores)
            {
                if (score.Value != 0)
                {
                    _Dice.Add(ColorToDice(score.Key), score.Value);
                }
            }
            // ROLL THEM
            AnsiConsole.WriteLine();
            _Dice.Roll();
            DisplayResults();
        }

        private void DisplayResults()
        {
            var table = new Table();
            table.RoundedBorder();
            // COLUMNS
            table = addColumn(table, "SUCCESSES");
            table = addColumn(table, "FAILURES");
            table = addColumn(table, "ADVANTAGES");
            table = addColumn(table, "THREATS");
            table = addColumn(table, "TRIUMPS");
            table = addColumn(table, "DESPAIRS");
            // ROWS
            table.AddRow(
                _Dice.SUCCESSES.ToString(),
                _Dice.Failures.ToString(),
                _Dice.ADVANTAGES.ToString(),
                _Dice.THREATS.ToString(),
                _Dice.TRIUMPS.ToString(),
                _Dice.DESPAIR.ToString());
            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
            int net = _Dice.NETSUCCESSES;
            int adv = _Dice.NETADVANTAGES;
            AnsiConsole.Write("Did the take that was rolled PASS OR FAIL?  ");
            if (net > 0)
                AnsiConsole.Markup("[WHITE]PASSED[/]");
            else
                AnsiConsole.Markup("[RED]FAILES[/]");
            // ADDITIONAL INFO
            AnsiConsole.Markup("  WITH ");
            if (adv > 0)
                AnsiConsole.MarkupLineInterpolated($"[WHITE]{adv} Advantages for player to use![/]");
            else if (adv == 0)
                AnsiConsole.MarkupLineInterpolated($"[WHITE] NO advantages or Threats rolled[/]");
            else
                AnsiConsole.MarkupLineInterpolated($"[RED]{adv} THREATS for GM to use against you![/]");
            AnsiConsole.WriteLine();
        }

        private int askQuestion(string question)
        {
            return AnsiConsole.Ask<int>(question,0);
        }

        private Dice ColorToDice(Colors color)
        {
            Dice? dice = null;
            switch (color)
            {
                case Colors.BLUE: dice = new Boost(); break;
                case Colors.BLACK: dice = new Setback(); break;
                case Colors.GREEN: dice = new Ability(); break;
                case Colors.PURPLE: dice = new Difficulty(); break;
                case Colors.YELLOW: dice = new Proficiency(); break;
                case Colors.RED: dice = new Challenge(); break;
            }

            return dice;
        }

        private Table addColumn(Table table,string name)
        {
            return table.AddColumn(name, col => col.Centered());
        }
    }

}
