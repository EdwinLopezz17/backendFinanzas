using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MonolineInfraestructure.context;
using MonolineInfraestructure.Interfaces;
using Property = MonolineInfraestructure.models.Property;

namespace MonolineInfraestructure;

public class PropertyMySQLInfraestructure : IPropertyInfraestructure
{
    private MonolineDBContext _monolineDbContext;

    public PropertyMySQLInfraestructure(MonolineDBContext monolineDbContext)
    {
        _monolineDbContext = monolineDbContext;
    }


    public List<Property> GetAll()
    {
        return _monolineDbContext.Properties.ToList();
    }

    public Property GetObject(int id)
    {
        return _monolineDbContext.Properties.Find(id);
    }

    public bool save(Property property)
    {

        _monolineDbContext.Properties.Add(property);

        _monolineDbContext.SaveChanges();

        return true;
    }
    
}