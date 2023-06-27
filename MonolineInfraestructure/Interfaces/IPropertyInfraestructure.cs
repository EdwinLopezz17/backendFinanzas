using MonolineInfraestructure.models;

namespace MonolineInfraestructure.Interfaces;

public interface IPropertyInfraestructure
{
    public List<Property> GetAll();
    public Property GetObject(int id);
    public bool save(Property property);
}