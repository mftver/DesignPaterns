namespace Model.Interfaces.Observer;

public interface IDpObserver<T>
{
    void Update(T data);
}