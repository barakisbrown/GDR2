namespace Core.Dice.Bad
{
    public class Setback : Dice,IBad
    {
        private int _failures = 0;
        private int _threats = 0;

        public Setback()
        {
            Sides = 6;
            Name = "Setback";
            Color = Colors.BLACK;
        }

        public override void Interpret()
        {
            var resultList = LookupTable.Setback[NumRolled - 1];
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
            }
        }

        int IBad.Failures { get => _failures; set => _failures = value; }
        int IBad.THREATS { get => _threats; set => _threats = value; }
        int IBad.DESPAIR { get => 0; set => _ = 0; }
        }
}