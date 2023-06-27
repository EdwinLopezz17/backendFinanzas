
using Microsoft.AspNetCore.Mvc;
using MonolineDomain;
using MonolineInfraestructure.Interfaces;
using MonolineInfraestructure.models;

namespace MonoLineAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        //Inyection
        private IUserInfreaestructure _userInfreaestructure;
        
        public UserController(IUserInfreaestructure userInfreaestructure)
        {
            _userInfreaestructure = userInfreaestructure;
            
        }
        
        
        // GET: api/User
        [HttpGet]
        public List<User> Get()
        {
            var users = _userInfreaestructure.GetAll();
            return users;

        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUserById")]
        public User Get(int id)
        {
            User user = _userInfreaestructure.GetById(id);
            return user;
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _userInfreaestructure.save(user);
            return Ok();

        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
