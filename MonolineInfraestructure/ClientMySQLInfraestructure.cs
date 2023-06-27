using MonolineInfraestructure.context;
using MonolineInfraestructure.Interfaces;
using MonolineInfraestructure.models;

namespace MonolineInfraestructure;

public class ClientMySQLInfraestructure : IClientInfraestructure
{
    
    private MonolineDBContext _monolineDbContext;

    public ClientMySQLInfraestructure(MonolineDBContext monolineDbContext)
    {
       _monolineDbContext = monolineDbContext;
    }

    public Client GetObject(int dni)
    {
        return _monolineDbContext.Clients.Find(dni);
    }

    public Client save(Client client)
    {
        _monolineDbContext.Clients.Add(client);
        _monolineDbContext.SaveChanges();
        _monolineDbContext.Entry(client).Reload();
        return client;

    }

    public List<Client> GetAll()
    {
        return _monolineDbContext.Clients.ToList();
    }
}