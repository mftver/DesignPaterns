using Model.Interfaces;

namespace Model;

public class Group : IValidatable
{
    public Group()
    {
        _validatables = new List<IValidatable>();
    }

    private List<IValidatable> _validatables { get; }

    public bool Validate()
    {
        return _validatables.All(c => c.Validate());
    }

    public void AddValidateable(IValidatable validateable)
    {
        _validatables.Add(validateable);
    }

    public void RemoveValidateables(IValidatable validateable)
    {
        _validatables.Remove(validateable);
    }

    public List<IValidatable> GetChildren()
    {
        return _validatables;
    }
}