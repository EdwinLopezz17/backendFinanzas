using System.Net;
using MonolineInfraestructure.context;
using MonolineInfraestructure.Interfaces;
using MonolineInfraestructure.models;

namespace MonolineInfraestructure;

public class CreditMySQLInfraestructure : ICreditInfraestructure
{
    private MonolineDBContext _monolineDbContext;

    public CreditMySQLInfraestructure(MonolineDBContext monolineDbContext)
    {
        _monolineDbContext = monolineDbContext;
    }
    
    
    
    
    public List<Credit> GetAll()
    {
        return _monolineDbContext.Credits.ToList();
    }

    public Credit GetObject(int id)
    {
        return _monolineDbContext.Credits.Find(id);
    }
    

    public Boolean save(Credit credit)
    {
        _monolineDbContext.Credits.Add(credit);
        _monolineDbContext.SaveChanges();

        return true;
        // Recupera el crédito agregado con el ID actualizado
        //_monolineDbContext.Entry(credit).Reload();

        //return credit;
    }
}