using Model.Interfaces;

namespace Model;

public class Group : IValidatable
{
    public Group()
    {
        _validateables = new List<IValidatable>();
    }

    private List<IValidatable> _validateables { get; }

    public bool Validate()
    {
        //validate cells and validate if the whole group contains integers multiple times

        throw new NotImplementedException();
    }

    public void AddValidateable(IValidatable validateable)
    {
        _validateables.Add(validateable);
    }
    
       public void RemoveValidateables(IValidatable validateable)
    {
        _validateables.Remove(validateable);
    }
       
       public List<IValidatable> GetChildren()
    {
        return _validateables;
    }
}