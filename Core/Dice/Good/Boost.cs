namespace Core.Dice.Good
{
    public class Boost : Dice, IGood
    {
        private int _advantages = 0;
        private int _successes = 0;

        public Boost()
        {

            Sides = 6;
            Name = "BOOST";
            Color = Colors.BLUE;
        }

        public override void Interpret()
        {
            var resultList = LookupTable.Boost[NumRolled - 1];
            RolledString = resultList;
            switch (resultList)
            {
                case "":
                    break;
                case "X":
                    _successes++;
                    break;
                case "XA":
                    _advantages++;
                    _successes++;
                    break;
                case "AA":
                    _advantages += 2;
                    break;
                case "A":
                    _advantages++;
                    break;
            }
        }
        int IGood.SUCCESSES { get => _successes; set => _successes = value; }
        int IGood.ADVANTAGES { get => _advantages; set => _advantages = value; }
        int IGood.TRIUMPS { get => 0; set => _ = 0; }
    }
}
