using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Api.Controllers;
using Admin.Api.Data.Entities;
using Admin.Api.Data.Repositories;
using Admin.Api.Models.Customer;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Admin.Api.Tests.Controllers
{
    public class CustomerControllerFacts : TestFactsBase<CustomerController>
    {
        [Theory]
        [AutoData]
        public async void Can_add_new_customer (CustomerModel customerModel)
        {
            var customerRepoMock = Fixture.Freeze<Mock<ICustomerRepository>> ();
            var sut = GetSut ();
            var result = await sut.AddCustomer (customerModel) as OkResult;

            customerRepoMock.Verify (o => o.AddAsync (It.Is<Customer> (c => c.Address == customerModel.Address &&
                c.Email == customerModel.Email &&
                c.Fullname == customerModel.Fullname &&
                c.Phone == customerModel.Phone &&
                c.Status == StatusEnum.Current)));

            Assert.IsType<OkResult> (result);
        }

        [Theory]
        [AutoData]
        public async void Can_remove_customer (int id, Customer customerToDelete)
        {
            var customerRepoMock = Fixture.Freeze<Mock<ICustomerRepository>> ();
            customerRepoMock.Setup (o => o.GetByIdAsync (id)).ReturnsAsync (customerToDelete);
            var sut = GetSut ();

            var result = await sut.DeleteCustomer (id) as OkResult;

            Assert.IsType<OkResult> (result);
            customerRepoMock.Verify (o => o.DeleteAsync (customerToDelete));
        }

        [Theory]
        [AutoData]
        public async void Should_return_error_when_deleting_invalid_customer (int id)
        {
            var customerRepoMock = Fixture.Freeze<Mock<ICustomerRepository>> ();
            customerRepoMock.Setup (o => o.GetByIdAsync (id)).ReturnsAsync ((Customer) null);
            var sut = GetSut ();

            var result = await sut.DeleteCustomer (id) as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult> (result);
        }

        [Theory]
        [AutoData]
        public async void Can_get_all_customers (IEnumerable<Customer> customers)
        {
            var customerRepoMock = Fixture.Freeze<Mock<ICustomerRepository>> ();
            customerRepoMock.Setup (o => o.GetAllAsync ()).ReturnsAsync (customers);
            var sut = GetSut ();

            var result = await sut.GetCustomers () as IEnumerable<Customer>;

            Assert.True (result.Count () > 0);
        }

        [Theory]
        [AutoData]
        public async void Can_get_customer_by_id (int id)
        {
            var customer = new Customer { Id = id };
            var customerRepoMock = Fixture.Freeze<Mock<ICustomerRepository>> ();

            customerRepoMock.Setup (o => o.GetByIdAsync (id)).ReturnsAsync (customer);
            var sut = GetSut ();

            var result = await sut.GetCustomer (id) as Customer;

            Assert.Equal (customer.Id, id);
        }

        [Theory]
        [AutoData]
        public async void Can_add_notes_to_customer (AddNoteModel model, Customer customer)
        {
            var customerRepoMock = Fixture.Freeze<Mock<ICustomerRepository>> ();
            customerRepoMock.Setup (o => o.GetByIdAsync (model.CustomerId)).ReturnsAsync (customer);
            var sut = GetSut ();

            var result = await sut.AddNote (model) as OkResult;

            customerRepoMock.Verify (o => o.AddNoteAsync (customer, It.Is<Note> (n => n.Text == model.Note.Text)));
            Assert.IsType<OkResult> (result);
        }

        [Theory]
        [AutoData]
        public async void Can_change_status_to_customer (ChangeStatusModel model, Customer customer)
        {
            var customerRepoMock = Fixture.Freeze<Mock<ICustomerRepository>> ();
            customerRepoMock.Setup (o => o.GetByIdAsync (model.CustomerId)).ReturnsAsync (customer);
            var sut = GetSut ();

            var result = await sut.UpdateStatus (model) as OkResult;

            Assert.IsType<OkResult> (result);
            Assert.Equal (model.NewStatus, customer.Status);
            customerRepoMock.Verify (o => o.SaveChangesAsync ());
        }
    }
}