using MonolineInfraestructure.models;

namespace MonolineInfraestructure.Interfaces;

public interface IUserInfreaestructure
{
        public List<User> GetAll();
        public User GetById(int id);
        public bool save(User user);
        public bool delete(int id);
}