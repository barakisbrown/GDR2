using Spectre.Console;

namespace CommandLine;

/// <summary>
/// CommandLine will just launch a non-interactive version of the application to be run purely from the console.
/// The only valid arguments are as follows:
/// 1 --help [explains how to build a valid dice pool plus show the application name,etc.
/// 2 --valid dice pool string such as [1g] which mean roll 1 Ability dice.
/// </summary>
public class CommandLine
{
    public CommandLine(string[] args)
    {
        if (args.Length == 1)
        {
            if (args.Contains("--help")) DisplayHelp();
            else if (args.Contains("--colors")) DisplayColors();
            else
            {
                ValidateString(args[0]);
            }
        }
    }

    private void DisplayHelp()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[underline bold red]GDR2 -- Genesys Dice Roller version 2.0 -- Copyright @2025[/]");
        AnsiConsole.MarkupLine("[bold]Usage gdr2 --HELP to show this help screen[/]");
        AnsiConsole.MarkupLine("[bold]Usage: gdr2 --colors will show you which colors are used.[/]");
        AnsiConsole.MarkupLine("[bold]GDR2 #L.. where # is the amount of dice plus the letter color used[/]");
        AnsiConsole.MarkupLine("[bold]Example:1G   => 1 Green(Ability) Dice[/]");   
        AnsiConsole.MarkupLine("[bold]Example:3G2P => 3 Green(Ability) PLUS 2 Purple(Difficulty)[/]");
        AnsiConsole.MarkupLine("[bold]Example:bgppr => 1 Blue(Boost) 1 Green(Ability) 1 Purple(Difficulty) 1 Red(Challenge)[/]");
    }

    private void DisplayColors()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[underline bold red]GDR2 -- Genesys Dice Roller version 2.0 -- Copyright @2025[/]");
        AnsiConsole.MarkupLine("[bold]Usage gdr2 --colors to show this help screen[/]");
        AnsiConsole.MarkupLine("[bold]Following Colors represent dice available to be used.[/]");
        AnsiConsole.MarkupLine("[blue]B for Boost(BLUE)[/]");
        AnsiConsole.MarkupLine("[green]g for Ability(GREEN)[/]");
        AnsiConsole.MarkupLine("[purple]p for Difficulty(PURPLE)[/]");
        AnsiConsole.MarkupLine("[yellow]y for Proficiency(YELLOW)[/]");
        AnsiConsole.MarkupLine("[red]r for Challenge(RED)[/]");
    }

    private void ValidateString(string pool)
    {
        // VALID DICE STRING VALIDATED AND THEN ROLLED
        // All characters will be converted to lowercase
        // L = 0 then assume 1
        // Valid Example => 3G2P
        // valid Example => bgppr
        AnsiConsole.WriteLine($"String being validated is [{pool}]");
    }
}