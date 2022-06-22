using Application.Services.CustomerImplementation;
using Data.Entities;
using Data.Repositories.CustomerDAO;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : Controller
    {
        private ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service; 
        }

        //GET api/customers
        [HttpGet("api/customers")]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = _service.GetCustomers();
            return Ok(customers);
        }

        //GET api/customers/{id}
        [HttpGet("api/customers/{id:int}")]
        public ActionResult<Customer> GetCustomerById(int id)
        {
            var customer = _service.GetCustomer(id);
            return Ok(customer);
        }
    }
}
