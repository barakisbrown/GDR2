namespace Core.Dice
{
    public class Pool : IGood, IBad
    {
        private int _success = 0;
        private int _failure = 0;
        private int _advantage = 0;
        private int _threats = 0;
        private int _triump = 0;
        private int _despair = 0;
        private int _netSuccess = 0;
        private int _netAdvantage = 0;

        private readonly List<Dice> _pool = new List<Dice>();
        private string _printedStr = " ";

        public Pool() { }


        public int Remove(Colors _diceColor,int count = 1)
        {
            if (count <= 0 || count > _pool.Count) return -1;
            int removed = 0;
            for (int i = _pool.Count - 1;i >=0 && removed < count;i--)
            {
                if (_pool[i].Color == _diceColor)
                {
                    _pool.RemoveAt(i);
                    removed++;
                }   
            }

            return removed;
        }

        public void Add(Dice dice)
        {
            _pool.Add(dice);
        }

        public void Add(Dice dice, int amount)
        {
            if (amount > 0)
            {
                if (amount == 1)
                    Add(dice);
                else
                {
                    for (int i = 0; i < amount; i++)
                    {
                        Add(dice);
                    }
                }
            }
        }
        public int COUNT => _pool.Count;

        public bool Clear()
        {
            ClearResults();
            _pool.Clear();
            return _pool.Count == 0;
        }

        private void ClearResults()
        {
            _success = 0;
            _failure = 0;
            _advantage = 0;
            _threats = 0;
            _triump = 0;
            _despair = 0;
            _netSuccess = 0;
            _netAdvantage = 0;
        }

        public void Roll()
        {
            string currentName = string.Empty;
            /*
             * S = Success / A = Advantage / TR = TRIUMP
             * F = Failure / TH = Threats / D = Despair
             */            
            int s = 0, a = 0, tr = 0;
            int f = 0, th = 0, d = 0;
            var rand = new Random(Guid.NewGuid().GetHashCode());

            for(int loop = 0;loop < _pool.Count; loop++)
            {
                var D = _pool[loop];
                if (D.Name != currentName)
                {
                    _success += s;
                    _advantage += a;
                    _triump += tr;
                    _failure += f;
                    _threats += th;
                    _despair += d;
                    s = a = tr = f = th = d = 0; 
                }
                currentName = D.Name;
                
                D.NumRolled = rand.Next(1, D.Sides + 1);
                D.Interpret();
                _printedStr += D.RolledString + " ";
                if (D is IGood G)
                {
                    s = G.SUCCESSES;
                    a = G.ADVANTAGES;
                    tr = G.TRIUMPS;                    
                }
                else if (D is IBad B)
                {
                    f = B.Failures;
                    th = B.THREATS;
                    d = B.DESPAIR;                    
                }
            }
            // ADD 1 MORE TIME SINCE IT DID NOT SEE THE LAST ITERATION
            _success += s;
            _advantage += a;
            _triump += tr;
            _failure += f;
            _threats += th;
            _despair += d;

            // GET NET ROLLED
            _netSuccess = _success - _failure;
            _netAdvantage = _advantage - _threats;
        }
               
        public int NETSUCCESSES => _netSuccess;
        public int NETADVANTAGES => _netAdvantage;

        public int SUCCESSES { get => _success; set => _success = value; }
        public int ADVANTAGES { get => _advantage; set => _advantage = value; }
        public int TRIUMPS { get => _triump; set => _triump = value; }

        public int Failures { get => _failure; set => _failure = value; }
        public int THREATS { get => _threats; set => _threats = value; }
        public int DESPAIR { get => _despair; set => _despair = value; }

        public string PRINTEDSTR => _printedStr;
    }
}
