using MonolineInfraestructure.context;
using MonolineInfraestructure.Interfaces;
using MonolineInfraestructure.models;

namespace MonolineInfraestructure;

public class FlowsMySQLInfraestructure : IFlowsInfraestructure
{
    private MonolineDBContext _monolineDbContext;

    public FlowsMySQLInfraestructure(MonolineDBContext monolineDbContext)
    {
        _monolineDbContext = monolineDbContext;
    }

    
    public List<Flows> _flowsListByCreditId(int credit_id)
    {
        return _monolineDbContext.Flowss.Where(flow => flow.CreditId == credit_id).ToList();
    }
}