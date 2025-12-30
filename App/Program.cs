using Core.Dice;
using Core.Dice.Bad;
using Core.Dice.Good;
using Interactive;

if (args.Length > 0)
{
    var cmd = new CommandLine.CommandLine(args);
}
else
{
    var app = new App();
}