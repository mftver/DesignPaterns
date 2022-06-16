namespace Model.Interfaces.Observer;

public interface IDpObservable<T>
{
    void Subscribe(IDpObserver<T> observer);
    void OnNext(T data);
}