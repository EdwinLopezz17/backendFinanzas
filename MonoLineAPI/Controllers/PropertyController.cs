using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MonolineInfraestructure.Interfaces;
using Property = MonolineInfraestructure.models.Property;

namespace MonoLineAPI.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        
        //Inyection
        private IPropertyInfraestructure _propertyInfraestructure;
        
        public PropertyController(IPropertyInfraestructure propertyInfraestructure)
        {
            _propertyInfraestructure = propertyInfraestructure;
            
        }
        
        
        
        // GET: api/Property
        [HttpGet]
        public List<Property> Get()
        {
            var properties = _propertyInfraestructure.GetAll();
            return properties;
        }

        // GET: api/Property/5
        [HttpGet("{id}", Name = "GetPropertyById")]
        public Property Get(int id)
        {
            return _propertyInfraestructure.GetObject(id);
        }

        // POST: api/Property
        [HttpPost]
        public IActionResult Post([FromBody] Property property)
        {
            _propertyInfraestructure.save(property);
            return Ok();
        }
    }
}
