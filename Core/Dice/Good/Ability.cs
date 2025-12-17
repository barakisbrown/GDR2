namespace Core.Dice.Good
{
    public class Ability : Dice, IGood
    {
        private int _success = 0;
        private int _advantage = 0;

        public Ability()
        {
            Sides = 8;
            Name = "Ability";
            Color = Colors.GREEN;

        }

        int IGood.SUCCESSES { get => _success; set => _success = value; }
        int IGood.ADVANTAGES { get => _advantage; set => _advantage = value; }
        int IGood.TRIUMPS { get => 0; set => _ = 0; }

        public override void Interpret()
        {
            var resultList = LookupTable.Ability[NumRolled - 1];
            RolledString = resultList;
            switch (resultList)
            {
                case "":
                    break;
                case "X":
                    _success++;
                    break;

                case "A":
                    _advantage++;
                    break;
                case "XX":
                    _success += 2;
                    break;
                case "XA":
                    _success++;
                    _advantage++;
                    break;
                case "AA":
                    _advantage += 2;
                    break;
            }
        }
    }
}
