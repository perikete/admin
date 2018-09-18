using System;
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
            var customerRepoMock = Fixture.Freeze<Mock<IRepository<Customer>>> ();
            var sut = GetSut ();
            var result = await sut.AddCustomer (customerModel) as OkResult;

            customerRepoMock.Verify (o => o.AddAsync (It.Is<Customer> (c => c.Address == customerModel.Address &&
                c.Email == customerModel.Email &&
                c.Fullname == customerModel.Fullname &&
                c.Phone == customerModel.Phone &&
                c.Status == StatusEnum.Current)));

            Assert.IsType<OkResult> (result);
        }

    }
}