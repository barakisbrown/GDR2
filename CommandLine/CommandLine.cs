namespace CommandLine;

using Core.Dice;
using Core.Dice.Bad;
using Core.Dice.Good;
using Spectre.Console;
using System.Text.RegularExpressions;

/// <summary>
/// CommandLine will just launch a non-interactive version of the application to be run purely from the console.
/// The only valid arguments are as follows:
/// 1 --help [explains how to build a valid dice pool plus show the application name,etc.
/// 2 --valid dice pool string such as [1g] which mean roll 1 Ability dice.
/// </summary>
public class CommandLine
{
    private static readonly char[] _Valid = "BKGPYR".ToLower().ToCharArray();
    private List<KeyValuePair<Colors, int>> _Bag = [];
    private Pool _DicePool = new();

    public CommandLine(string[] args)
    {        
        if (args.Length == 1)
        {
            if (args.Contains("--help")) DisplayHelp();
            else if (args.Contains("--colors")) DisplayColors();
            else
            {
                if (ValidateString(args[0]))
                    DisplayResults();                                      
            }
        }
    }

    internal static void DisplayHelp()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[underline bold red]GDR2 -- Genesys Dice Roller version 2.0 -- Copyright @2025[/]");
        AnsiConsole.MarkupLine("[bold]Usage: gdr2 --HELP to show this help screen[/]");
        AnsiConsole.MarkupLine("[bold]Usage: gdr2 --colors will show you which colors are used.[/]");
        AnsiConsole.MarkupLine("[bold]GDR2 <#L OR L> where # is number of dice and L is the color of dice.[/]");
        AnsiConsole.MarkupLine("[bold]Example:1G    => 1 Green(Ability) Dice[/]");   
        AnsiConsole.MarkupLine("[bold]Example:3G2P  => 3 Green(Ability) PLUS 2 Purple(Difficulty)[/]");
        AnsiConsole.MarkupLine("[bold]Example:bgppr => 1 Blue(Boost) 1 Green(Ability) 1 Purple(Difficulty) 1 Red(Challenge)[/]");
    }

    internal static void DisplayColors()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold]Following Colors represent dice available to be used.[/]");
        AnsiConsole.MarkupLine("[blue]B for Boost(BLUE)[/]");
        AnsiConsole.MarkupLine("[green]g for Ability(GREEN)[/]");
        AnsiConsole.MarkupLine("[purple]p for Difficulty(PURPLE)[/]");
        AnsiConsole.MarkupLine("[yellow]y for Proficiency(YELLOW)[/]");
        AnsiConsole.MarkupLine("[red]r for Challenge(RED)[/]");
        AnsiConsole.WriteLine();
    }

    /// <summary>
    // VALID DICE STRING VALIDATED AND THEN ROLLED
    // All characters will be converted to lowercase
    // # = 0 then assume 1
    // Valid Example => 3G2P
    // valid Example => bgppr
    // valid Example => 1 of [bkgpyr]
    /// </summary>
    /// <param name="pool">user specified string that is being rolled</param>
    private bool ValidateString(string pool)
    {
        AnsiConsole.MarkupLine("[underline bold red]GDR2 -- Genesys Dice Roller version 2.0 -- Copyright @2025[/]");
        AnsiConsole.WriteLine($"String being validated is [{pool}]");
        if (pool.Length == 1)
        {
            if (ValidateSingle(pool))
                _Bag.Add(new KeyValuePair<Colors, int>(toColor(pool[0]), 1));
            else
                throw new InvalidDiceException();
        }
        // THERE IS NO NUMBERS IN THE STRING AND COLOR ARE VALID
        if (pool.All(char.IsLetter))
        {
            if (pool.IndexOfAny(_Valid) == -1)
                throw new InvalidDiceException();
            else
                ValidateAllLetters(pool);
        }
        else
        {
            // THERE ARE NUMBERS IN THE STRING
            var pattern = @"\d.";
            var placement = string.Empty;
            string result = Regex.Replace(pool, pattern, placement);
            if (!result.Contains(pool))
            {
                var match = Regex.Matches(pool, pattern, RegexOptions.IgnoreCase);
                var list = match.Cast<Match>().Select(match => match.Value).ToList();
                foreach (var entry in list)
                {
                    // ENTRY IS {0-9}{bkgpyr}
                    var amount = entry[0] - '0';
                    var type = Char.ToLower(entry[1]);
                    if (toColor(type) != Colors.NONE)
                    {
                        _Bag.Add(new KeyValuePair<Colors, int>(toColor(type), amount));
                    }
                    else
                    {
                        AnsiConsole.MarkupLineInterpolated($"Error : [RED]type[/] IS INVALID.");
                        AnsiConsole.WriteLine("Only letters allowed are [b]lue [k]black [g]reen [p]urple [y]ellow [r]ed");
                        return false;
                    }
                }
                // IF RESULT IS EMPTY THEN NOTHING ELSE IS DONE
                if (result != string.Empty)
                    ValidateString(result);
                else
                    return true;
            }
            else
                throw new InvalidDiceException();
        }
        
        // All Dice Validated
        return true;            
    }

    private void DisplayResults()
    {
        // Generate Bag of dice to be rolled
        AnsiConsole.WriteLine();
        for ( int loop = 0; loop < _Bag.Count; loop++)
        {
            var ValuePair = _Bag[loop];
            var color = ValuePair.Key;
            int amount = ValuePair.Value;
            _DicePool.Add(ConvertColorToDice(color), amount);
        }
        // Roll Them
        _DicePool.Roll();
        // Show Results
        AnsiConsole.WriteLine("Printing Results");
        AnsiConsole.WriteLine($"Dice Rolled => {_DicePool.PRINTEDSTR}");
        AnsiConsole.MarkupLine("[BOLD]------------------------------[/]");
        AnsiConsole.WriteLine($"Successes = {_DicePool.SUCCESSES}   Advantages = {_DicePool.ADVANTAGES}  Triumps = {_DicePool.TRIUMPS}");
        AnsiConsole.WriteLine($"Failures = {_DicePool.Failures}    Threats = {_DicePool.THREATS}       Dispair = {_DicePool.DESPAIR}");
        AnsiConsole.WriteLine($"NetSuccess = {_DicePool.NETSUCCESSES} NetAdvantages = {_DicePool.NETADVANTAGES}");
        AnsiConsole.MarkupLine("[BOLD]------------------------------[/]");
        AnsiConsole.Write("Did the take that was rolled PASS OR FAIL? ");
        if (_DicePool.NETSUCCESSES > 0)
            AnsiConsole.MarkupLine("[WHITE]PASSED[/]");
        else
            AnsiConsole.MarkupLine("[RED]FAILED![/]");
        AnsiConsole.Write("Additionally => ");
        if (_DicePool.NETADVANTAGES > 0)
            AnsiConsole.MarkupLineInterpolated($"Total of {_DicePool.NETADVANTAGES} advantages you can use.");
        else
            AnsiConsole.MarkupLineInterpolated($"Total of -{_DicePool.NETADVANTAGES} threats the GM can use.");
    }

    /// <summary>
    /// This assumes that the color being returned is VALID.
    /// </summary>
    /// <param name="dice">letter represents the color returned</param>
    /// <returns>Colors color being returned</returns>
    private Colors toColor(char dice)
    {
        Colors retColor = Colors.NONE;
        switch (dice)
        {
            case 'b': retColor = Colors.BLUE; break;
            case 'k': retColor = Colors.BLACK; break;
            case 'g': retColor = Colors.GREEN; break;
            case 'p': retColor = Colors.PURPLE;break;
            case 'y': retColor = Colors.YELLOW;break;
            case 'r': retColor =  Colors.RED;break;
            default:
                return Colors.NONE;
        }

        return retColor;
    }

    private Dice ConvertColorToDice(Colors color)
    {
        Dice? dice = null;
        switch(color)
        {
            case Colors.BLUE: dice = new Boost();break;
            case Colors.BLACK: dice = new Setback();break;
            case Colors.GREEN: dice = new Ability();break;
            case Colors.PURPLE: dice = new Difficulty();break;
            case Colors.YELLOW: dice = new Proficiency();break;
            case Colors.RED: dice =  new Challenge();break;
        }

        return dice;
    }

    private bool ValidateSingle(string pool)
    {
        if (pool.Length == 1 && pool.IndexOfAny(_Valid) >= 0)
            return true;
        else
            return false;

    }

    private void ValidateAllLetters(string pool)
    {
        pool.ToLower();
        foreach (char x in pool)
        {
            _Bag.Add(new KeyValuePair<Colors, int>(toColor(x), 1));
        }
    }    
}