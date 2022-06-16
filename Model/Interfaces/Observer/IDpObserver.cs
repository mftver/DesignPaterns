namespace Model.Interfaces.Observer;

public interface IDpObserver<T>
{
    void UpdatePossibleNumbers(T data);
}