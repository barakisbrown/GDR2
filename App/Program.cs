using Core.Dice;
using Core.Dice.Bad;
using Core.Dice.Good;

if (args.Length > 0)
{
    var cmd = new CommandLine.CommandLine(args);
}
else
{
    var app = new Interactive.Interactive();
}















void APP()
{
    Console.WriteLine("Application launching .. it has no arguments");
}

void testDicePool()
{
    var pool = new Pool();

    pool.Add(new Ability(), 8);
    pool.Add(new Difficulty(), 3);
    pool.Add(new Challenge(), 5);
    // Roll and Show Results
    pool.Roll();

    // GOOD
    int success = pool.SUCCESSES;
    int advantage = pool.ADVANTAGES;
    int triumps = pool.TRIUMPS;
    // BAD
    int failure = pool.Failures;
    int threats = pool.THREATS;
    int despair = pool.DESPAIR;


    // DESIGNED ONLY TO SPIT THEM OUT TO THE CONSOLE
    Console.WriteLine("Printing Results");
    Console.WriteLine($"Dice Actually Rolled => {pool.PRINTEDSTR}");
    Console.WriteLine("Detailed Output:");
    Console.WriteLine("---------------------------------------------------------------------------");
    Console.WriteLine($"Successes = {success}   Advantages = {advantage}  Triumps = {triumps}");
    Console.WriteLine($"Failures = {failure}    Threats = {threats}       Dispair = {despair}");
    Console.WriteLine($"NetSuccess = {pool.NETSUCCESSES} NetAdvantages = {pool.NETADVANTAGES}");
    Console.WriteLine("---------------------------------------------------------------------------");
    // INTERPET WHAT ACTUALLY HAPPENED
    Console.Write("Did the task PASS or FAIL? ");
    // NET SUCCESS
    if (pool.NETSUCCESSES > 0)
        Console.WriteLine("TASK PASSED");
    else
        Console.WriteLine("TASK FAILED");
    // NET ADVANTAGES
    if (pool.NETADVANTAGES > 0)
        Console.WriteLine($"You have a net total of {pool.NETADVANTAGES} advantage to use");
    else if (pool.NETADVANTAGES < 0)
        Console.WriteLine($"Ok. Sorry to say that you have {-pool.NETADVANTAGES} threats to use against you");
    //
    Console.ReadKey();
}