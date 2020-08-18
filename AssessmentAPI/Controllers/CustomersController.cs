using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataService.Models;
using DataService;

namespace AssessmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AssessmentDBContext _context;
        private IDataRepository _repository;

        public CustomersController(IDataRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Customers
        [HttpGet]
        public ActionResult<List<Customers>> GetCustomers()
        {
            return _repository.GetAllCustomers();
        }

        // POST: api/Customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult PostCustomers(List<Customers> customers)
        {
            _repository.AddCustomer(customers);

            return NoContent();
        }
    }
}
