namespace Model;

public class NumberSwitch
{
    public readonly int NewNumber;
    public readonly int OldNumber;

    public NumberSwitch(int oldNumber, int newNumber)
    {
        OldNumber = oldNumber;
        NewNumber = newNumber;
    }
}