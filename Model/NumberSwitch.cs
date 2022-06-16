namespace Model;

public class NumberSwitch
{
    public readonly int OldNumber;
    public readonly int NewNumber;
    
    public NumberSwitch(int oldNumber, int newNumber)
    {
        OldNumber = oldNumber;
        NewNumber = newNumber;
    }
}