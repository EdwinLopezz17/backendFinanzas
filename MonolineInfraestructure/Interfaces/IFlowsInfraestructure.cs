using MonolineInfraestructure.models;

namespace MonolineInfraestructure.Interfaces;

public interface IFlowsInfraestructure
{
    List<Flows> _flowsListByCreditId(int credit_id);
}