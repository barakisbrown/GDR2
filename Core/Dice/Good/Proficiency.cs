namespace Core.Dice.Good
{
    public class Proficiency : Dice, IGood
    {
        private int _successes = 0;
        private int _advantages = 0;
        private int _triumps = 0;

        public Proficiency()
        {
            Sides = 12;
            Name = "Proficiency";
            Color = Colors.YELLOW;
        }

        public override void Interpret()
        {
            var resultList = LookupTable.Proficiency[NumRolled - 1];
            RolledString = resultList;
            switch (resultList)
            {
                case "":
                    break;
                case "X":
                    _successes++;
                    break;
                case "XX":
                    _successes += 2;
                    break;
                case "A":
                    _advantages++;
                    break;
                case "XA":
                    _successes++;
                    _advantages++;
                    break;
                case "AA":
                    _advantages += 2;
                    break;
                case "TP":
                    _triumps++;
                    break;
            }
        }

        int IGood.SUCCESSES { get => _successes; set => _successes = value; }
        int IGood.ADVANTAGES { get => _advantages; set => _advantages = value; }
        int IGood.TRIUMPS { get => _triumps; set => _triumps = value; }
    }
}
