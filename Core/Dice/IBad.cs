namespace Core.Dice
{
    public interface IBad
    {
        int Failures { get; set; }
        int THREATS { get; set; }
        int DESPAIR { get; set; }
    }
}
