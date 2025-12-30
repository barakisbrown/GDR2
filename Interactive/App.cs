using Core.Dice;
using Core.Dice.Bad;
using Core.Dice.Good;
using Spectre.Console;

namespace Interactive
{
    public class App
    {
        private readonly Pool _dice = new Pool();
        private Dictionary<Colors, int> _scores = new Dictionary<Colors, int>();
        public App()
        {
           RUNNER();
        }

        private void RUNNER()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLineInterpolated($"[underline Red]{Questions.ABOUT} [/]");
                AnsiConsole.MarkupLineInterpolated($"[red]{Questions.COPYRIGHT}[/]");
                AnsiConsole.WriteLine();
                // Set Scores to number of dice rolled if NOT 0
                _scores.Add(Colors.BLUE, AskQuestion(Questions.BOOST));
                _scores.Add(Colors.BLACK, AskQuestion(Questions.SETBACK));
                _scores.Add(Colors.GREEN, AskQuestion(Questions.ABILITY));
                _scores.Add(Colors.PURPLE, AskQuestion(Questions.DIFFICULTY));
                _scores.Add(Colors.YELLOW, AskQuestion(Questions.PROFICENCY));
                _scores.Add(Colors.RED, AskQuestion(Questions.CHALLENGE));
                // ADD Scores to the Dice Pool
                foreach (var score in _scores)
                {
                    if (score.Value != 0)
                    {
                        _dice.Add(ColorToDice(score.Key), score.Value);
                    }
                }

                // ROLL THEM
                AnsiConsole.WriteLine();
                if (_dice.COUNT == 0)
                {
                    AnsiConsole.MarkupLine("[blink red]ERROR : NO DICE ENTERED. ENTER DICE TO SEE RESULTS![/]");
                }
                else
                {
                    _dice.Roll();
                    DisplayResults();
                }

                if (YESNO())
                {
                    _dice.Clear();
                    _scores.Clear();
                    continue;
                }
                break;
            }
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
                _dice.SUCCESSES.ToString(),
                _dice.Failures.ToString(),
                _dice.ADVANTAGES.ToString(),
                _dice.THREATS.ToString(),
                _dice.TRIUMPS.ToString(),
                _dice.DESPAIR.ToString());
            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
            // CHECK TO MAKE SURE NO DICE WAS ROLLED
            var net = _dice.NETSUCCESSES;
            var adv = _dice.NETADVANTAGES;
            AnsiConsole.Write("Did the task that was rolled PASS OR FAIL?  ");
            if (net > 0)
                AnsiConsole.Markup("[BOLD]PASSED[/]");
            else
                AnsiConsole.Markup("[RED]FAILED[/]");

            // ADDITIONAL INFO
            AnsiConsole.Markup("  WITH ");
            if (adv > 0)
                AnsiConsole.MarkupLineInterpolated($"[BOLD]{adv} Advantages for player to use![/]");
            else if (adv == 0)
                AnsiConsole.MarkupLineInterpolated($"[BOLD] NO advantages or Threats rolled[/]");
            else
                AnsiConsole.MarkupLineInterpolated($"[RED]{adv} THREATS for GM to use against you![/]");
            // ADDING LOGIC just in case Despair or Triump has been rolled
            if (_dice.DESPAIR > 0 || _dice.TRIUMPS > 0)
            {
                var despair = (_dice.DESPAIR > 0) ? $"You have {_dice.DESPAIR} DESPAIRS AGAINST AGAINST YOU. EWW SORRY." : string.Empty;
                var triump = (_dice.TRIUMPS > 0) ? $"You have {_dice.TRIUMPS} TRIUMPS FOR YOU .. GOOD JOB" : string.Empty;
                if (despair != string.Empty)
                {
                    AnsiConsole.MarkupLineInterpolated($"[RED]{despair}[/]");
                }
                else if (triump != string.Empty)
                {
                    AnsiConsole.MarkupLineInterpolated($"[BOLD]{triump}[/]");
                }
            }
            AnsiConsole.WriteLine();
        }

        private int AskQuestion(string question)
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

        private bool YESNO()
        {
            return AnsiConsole.Confirm("Do you wish to roll another task(y/n)?");
            
        }
    }
}
