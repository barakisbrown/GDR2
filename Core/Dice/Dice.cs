namespace Core.Dice
{
    public abstract class Dice
    {
        private Colors _color;
        private int _sides;
        private string _name;
        private int _numRolled;
        private string _rolledStr;

        public int Sides
        {
            get { return _sides; }
            set { _sides = value; }
        }

        public string Name
        {
            get => _name;
            set { _name = value; }
        }

        public int NumRolled
        {
            get { return _numRolled; }
            set { _numRolled = value; }
        }

        public Colors Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public string RolledString
        {
            get { return _rolledStr; }
            set { _rolledStr = value; }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
                return false;
            else
                return this.Color == ((Dice)obj).Color;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return this.Name.GetHashCode();           
        }

        public abstract void Interpret();
    }
}
