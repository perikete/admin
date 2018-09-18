using System;
using System.Threading.Tasks;
using Admin.Api.Data.Entities;
using Admin.Api.Data.Repositories;
using Admin.Api.Models.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers
{
    [Route ("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerController (IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost("/")]
        public async Task<IActionResult> AddCustomer (CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                var newCustomer = new Customer
                {
                    Address = customerModel.Address,
                    Email = customerModel.Email,
                    Fullname = customerModel.Fullname,
                    Phone = customerModel.Phone,
                    Status = StatusEnum.Current,
                    CreationDate = DateTime.Now
                };

                await _customerRepository.AddAsync (newCustomer);

                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}