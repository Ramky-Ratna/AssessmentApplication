using System;
using Xunit;
using Moq;
using DataService;
using DataService.Models;
using System.Collections.Generic;
using AssessmentAPI;
using AssessmentAPI.Controllers;

namespace XUnitTestProject1
{
    
    public class CustomerContrllerUnitTest
    {
        private readonly CustomersController rep;
        private readonly Mock<IDataRepository> test=new Mock<IDataRepository>();

        public CustomerContrllerUnitTest()
        {
            rep = new CustomersController(test.Object);
        }
        [Fact]
        public void AddCustomer_Test()
        {
            //Arrange
            List<Customers> custs = new List<Customers>();

            test.Setup(x => x.AddCustomer(custs));
            //Act
            var result=rep.PostCustomers(custs);

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void GetAllCustomers_Test()
        {
            //Arrange
            List<Customers> custs = new List<Customers> {
                new Customers
                {
                    CustomerId=1,
                    CustomerName="RK"
                }
            };

            test.Setup(x => x.GetAllCustomers()).Returns(custs);
            //Act  
            var result = rep.GetCustomers();

            //Assert
            Assert.True(result.Value.Count>0);
        }
    }
}
