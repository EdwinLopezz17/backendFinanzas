using MonolineInfraestructure.models;

namespace MonolineInfraestructure.Interfaces;



public interface ICreditInfraestructure 
{
    public List<Credit> GetAll();
    public Credit GetObject(int id);
    public Boolean save (Credit credit);
}
    
