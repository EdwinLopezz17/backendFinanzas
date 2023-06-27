
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonoLineAPI.Request;
using MonolineInfraestructure.Interfaces;
using MonolineInfraestructure.models;

namespace MonoLineAPI.Controllers
{
    [Route("api/credit")]
    [ApiController]
    public class CreditController : ControllerBase
    {

        private ICreditInfraestructure _creditInfraestructure;
        private IPropertyInfraestructure _propertyInfraestructure;
        private IMapper _mapper;
        private IFlowsInfraestructure _flowsInfraestructure;

        public CreditController(ICreditInfraestructure creditInfraestructure, IMapper mapper,
            IPropertyInfraestructure propertyInfraestructure, IFlowsInfraestructure flowsInfraestructure)
        {
            _creditInfraestructure = creditInfraestructure;
            _mapper = mapper;
            _propertyInfraestructure = propertyInfraestructure;
            _flowsInfraestructure = flowsInfraestructure;
        }
        
        
        
        // GET: api/Credit
        [HttpGet]
        public List<Credit> Get()
        {
            return _creditInfraestructure.GetAll();
        }

        // GET: api/Credit/5
        [HttpGet("{id}", Name = "GetCreditById")]
        public Credit Get(int id)
        {
            return _creditInfraestructure.GetObject(id);
        }
        
        // GET: api/credit/{id}/futureflow
        [HttpGet("{id}/futureflow", Name = "GetFutureFlowById")]
        public List<Flows> GetFutureFlow(int id)
        {
            return _flowsInfraestructure._flowsListByCreditId(id);
        }
        

        // POST: api/Credit
        [HttpPost]
        public IActionResult Post([FromBody] Datas datas)
        {
            Credit credit = _mapper.Map<Datas, Credit>(datas);

            credit.FutureFlow = this.obtenerFlujos(datas, _propertyInfraestructure.GetObject(datas.IdProperty));

           _creditInfraestructure.save(credit);
            return Ok();
        }

        
        // PUT: api/Credit/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Credit/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private List<Flows> obtenerFlujos(Datas datas, Property property)

        {
            List<Flows> flowsList = new List<Flows>();
            
            flowsList = InitializeFLows(flowsList, property, datas.InitialFee, datas.InterestRate,
                datas.PartialGracePeriod, datas.FullGracePeriod,datas.NumberOfYears, datas.PaymentFrequency);

            flowsList =CalculateFees(flowsList, datas.NumberOfYears, datas.PaymentFrequency,property);

            return flowsList;
            
        }
        private List<Flows> InitializeFLows(List<Flows> flows, Property property, double initialFee, double interestRate,
            int partialGracePeriod, int fullGracePeriod, int numberOfYears, int paymentFrequency)
        {
            
            
            
            int ContFullGracePeriod = fullGracePeriod;
            int ContpartialGracePeriod = partialGracePeriod;

            double tep = Math.Pow(1 + interestRate, paymentFrequency / 360.0) - 1;
            int amount_fee = ((numberOfYears * 360) / paymentFrequency);


            for (int i = 0; i < amount_fee; i++)
            {
                flows.Add(new Flows());
               // flows[i].credit_id = credit_id;
            }
                
            flows[0].TEP = tep;
            flows[0].InitialBalance = property.Price - initialFee;

            for (int i = 0; i < amount_fee; i++)
            {
                flows[i].Number = i + 1;
                flows[i].TEP = tep;

                if (ContFullGracePeriod != 0)
                {
                    flows[i].GracePeriod = "T";
                    ContFullGracePeriod--;
                }
                else if (ContpartialGracePeriod != 0)
                {
                    flows[i].GracePeriod = "P";
                    ContpartialGracePeriod--;
                }
                else flows[i].GracePeriod = "S";
            }
            return flows;
        }
    
    private List<Flows> CalculateFees(List<Flows> flows,
         int numberOfYears, int paymentFrequency, Property property)
        {
            int amount_fee = ((numberOfYears * 360) / paymentFrequency);
            double SDper = (0.00045 / 30) * paymentFrequency;
            double SRper = (property.Price * 0.004) / (360 / paymentFrequency);

            for (int i = 0; i < amount_fee; i++)
            {
                switch (flows[i].GracePeriod)
                {
                    case "T":
                        if (i == 0)
                        {
                            flows[i].Interest = flows[i].TEP * flows[i].InitialBalance;
                            flows[i].Fee = 0;
                            flows[i].Amortization = 0;
                            flows[i].SD = SDper * flows[i].InitialBalance;
                            flows[i].STR = SRper;
                            flows[i].FinalBalance = flows[i].InitialBalance + flows[i].Interest;
                            flows[i].Flow = flows[i].STR;
                        }
                        else
                        {

                            flows[i].InitialBalance = flows[i - 1].FinalBalance;
                            flows[i].Interest = flows[i].TEP * flows[i].InitialBalance;
                            flows[i].Fee = 0;
                            flows[i].Amortization = 0;
                            flows[i].SD = SDper * flows[i].InitialBalance;
                            flows[i].STR = SRper;
                            flows[i].FinalBalance = flows[i].InitialBalance + flows[i].Interest;
                            flows[i].Flow = flows[i].STR;
                        }

                        break;
                    case "P":
                        if (i == 0)
                        {
                            flows[i].Interest = flows[i].TEP * flows[i].InitialBalance;
                            flows[i].Fee = flows[i].Interest;
                            flows[i].Amortization = 0;
                            flows[i].SD = SDper * flows[i].InitialBalance;
                            flows[i].STR = SRper;
                            flows[i].FinalBalance = flows[i].InitialBalance;
                            flows[i].Flow = flows[i].Fee + flows[i].STR;
                        }
                        else
                        {
                            flows[i].InitialBalance = flows[i - 1].FinalBalance;
                            flows[i].Interest = flows[i].TEP * flows[i].InitialBalance;
                            flows[i].Fee = flows[i].Interest;
                            flows[i].Amortization = 0;
                            flows[i].SD = SDper * flows[i].InitialBalance;
                            flows[i].STR = SRper;
                            flows[i].FinalBalance = flows[i].InitialBalance;
                            flows[i].Flow = flows[i].Fee + flows[i].STR;
                        }

                        break;
                    case "S":
                        if (i == 0)
                        {
                            flows[i].Interest = flows[i].TEP * flows[i].InitialBalance;
                            flows[i].Fee = (flows[i].InitialBalance * (flows[i].TEP + SDper)) / (1 - Math.Pow((1 + (flows[i].TEP + SDper)), -(amount_fee - 0)));
                            flows[i].SD = SDper * flows[i].InitialBalance;
                            flows[i].STR = SRper;
                            flows[i].Amortization = flows[i].Fee - flows[i].Interest - flows[i].SD;
                            flows[i].FinalBalance = flows[i].InitialBalance - flows[i].Amortization;
                            flows[i].Flow = flows[i].Fee + flows[i].STR;
                        }
                        else
                        {
                            flows[i].InitialBalance = flows[i - 1].FinalBalance;
                            flows[i].Interest = flows[i].TEP * flows[i].InitialBalance;
                            flows[i].Fee = (flows[i].InitialBalance * (flows[i].TEP + SDper)) /
                                           (1 - Math.Pow((1 + (flows[i].TEP + SDper)),
                                               -(amount_fee - flows[i - 1].Number)));
                            flows[i].SD = SDper * flows[i].InitialBalance;
                            flows[i].STR = SRper;
                            flows[i].Amortization = flows[i].Fee - flows[i].Interest - flows[i].SD;
                            flows[i].FinalBalance = flows[i].InitialBalance - flows[i].Amortization;
                            flows[i].Flow = flows[i].Fee + flows[i].STR;
                        }

                        break;
                }
            }

            return flows;
        }
    }
}
