using System;
using System.Collections.Generic;
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
        private readonly ICustomerRepository _customerRepository;

        public CustomerController (ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
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

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customerToDelete = await _customerRepository.GetByIdAsync(id);

            if (customerToDelete == null)
                return BadRequest("Invalid customer");

            await _customerRepository.DeleteAsync(customerToDelete);

            return Ok();
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<IActionResult> AddNote(int customerId, NoteModel note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var customer = await _customerRepository.GetByIdAsync(customerId);
            var newNote = new Note { Text = note.Text, CreationDate = DateTime.Now };
            await _customerRepository.AddNoteAsync(customer, newNote);


            return Ok();
        }
    }
}