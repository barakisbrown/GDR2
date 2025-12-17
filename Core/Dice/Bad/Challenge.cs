
namespace Core.Dice.Bad
{
    public class Challenge : Dice, IBad
    {
        private int _failures = 0;
        private int _threats = 0;
        private int _despair = 0;

        public Challenge()
        {
            Sides = 12;
            Name = "Challenge";
            Color = Colors.RED;
        }

        int IBad.Failures { get => _failures; set => _failures = value; }
        int IBad.THREATS { get => _threats; set => _threats = value; }
        int IBad.DESPAIR { get => _despair; set => _despair = value; }

        public override void Interpret()
        {
            var resultList = LookupTable.Challenge[NumRolled - 1];
            RolledString = resultList;
            switch (resultList)
            {
                case "":
                    break;
                case "F":
                    _failures++;
                    break;
                case "T":
                    _threats++;
                    break;
                case "FF":
                    _failures += 2;
                    break;
                case "TT":
                    _threats += 2;
                    break;
                case "FT":
                    _failures++;
                    _threats++;
                    break;
                case "DR":
                    _despair++;
                    break;
            }
        }
    }
}
