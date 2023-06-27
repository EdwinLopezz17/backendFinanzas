using MonolineInfraestructure.context;
using MonolineInfraestructure.Interfaces;
using MonolineInfraestructure.models;

namespace MonolineInfraestructure;

public class UserMySQLInfraestructure: IUserInfreaestructure
{
    
    private MonolineDBContext _monolineDbContext;

    public UserMySQLInfraestructure(MonolineDBContext monolineDbContext)
    {
        _monolineDbContext = monolineDbContext;
    }
    
    public List<User> GetAll()
    {
        return _monolineDbContext.Users.ToList();
    }

    public User GetById(int id)
    {
        return _monolineDbContext.Users.Find(id);
    }

    public bool save(User user)
    {
        _monolineDbContext.Users.Add(user);
        _monolineDbContext.SaveChanges();
        return true;
    }
    
    public bool delete(int id)
    {
        throw new NotImplementedException();
    }
}