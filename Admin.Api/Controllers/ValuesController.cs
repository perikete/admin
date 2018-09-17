using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Api.Data.Repositories;
using Admin.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;

        public ValuesController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post()
        {
            _customerRepository.AddCustomer(new Customer { Address = "dd", CreationDate = DateTime.Now, Email = "pdd", Fullname = "dd", Phone = "dd" });
            var all = _customerRepository.GetAllCustomers();


        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
