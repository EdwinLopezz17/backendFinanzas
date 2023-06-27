using MonolineInfraestructure.models;

namespace MonolineInfraestructure.Interfaces;

public interface IClientInfraestructure
{
    public Client GetObject(int dni);
    public Client save(Client client);
    public List<Client> GetAll();
}