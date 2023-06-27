
using Microsoft.AspNetCore.Mvc;
using MonolineInfraestructure.Interfaces;
using MonolineInfraestructure.models;

namespace MonoLineAPI.Controllers
{
    
    
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        
        //Inyection
        private IClientInfraestructure _clientInfraestructure;
        
        public ClientController(IClientInfraestructure clientInfraestructure){
            _clientInfraestructure = clientInfraestructure;
            
        }
        
        
        // GET: api/Client
        [HttpGet]
        public List<Client> Get()
        {
            return _clientInfraestructure.GetAll();
        }

        // GET: api/Client/5
        [HttpGet("{dni}", Name = "GetClientById")]
        public Client Get(int dni)
        {
            return _clientInfraestructure.GetObject(dni);
        }

        // POST: api/Client
        [HttpPost]
        public Client Post([FromBody] Client client)
        {
            return _clientInfraestructure.save(client);
            
        }

        // PUT: api/Client/5
        [HttpPut("{dni}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
