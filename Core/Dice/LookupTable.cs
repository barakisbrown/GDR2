namespace Core.Dice
{
    public class LookupTable
    {
        public static string[] Boost = { "", "", "X", "XA", "AA", "A" };

        public static string[] Setback = { "", "", "F", "F", "T", "T" };

        public static string[] Ability = { "", "X", "X", "XX", "A", "A", "XA", "AA" };

        public static string[] Difficulty = { "", "F", "FF", "T", "T", "T", "TT", "FT" };

        public static string[] Proficiency = { "", "X", "X", "XX", "XX", "A", "XA", "XA", "XA", "AA", "AA", "TP" };

        public static string[] Challenge = { "", "F", "F", "FF", "FF", "T", "T", "FT", "FT", "TT", "TT", "DR" };
    }
}
