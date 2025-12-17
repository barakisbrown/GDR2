namespace Core.Dice.Bad
{
    public class Difficulty : Dice, IBad
    {
        private int _failures = 0;
        private int _threats = 0;

        public Difficulty()
        {
            Sides = 8;
            Name = "Difficulty";
            Color = Colors.PURPLE;
        }

        int IBad.Failures { get => _failures; set => _failures = value; }
        int IBad.THREATS { get => _threats; set => _threats = value; }
        int IBad.DESPAIR { get => 0; set => value = 0; }


        public override void Interpret()
        {
            var resultList = LookupTable.Difficulty[NumRolled - 1];
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
            }
        }
    }
}
