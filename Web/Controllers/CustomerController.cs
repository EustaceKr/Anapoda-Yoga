using Application.DTOs.CustomerDTOs;
using Application.Services.CustomerImplementation;
using AutoMapper;
using Data.Entities;
using Data.Repositories.CustomerDAO;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //GET api/customers
        [HttpGet("api/customers")]
        public async Task<ActionResult<IEnumerable<CustomerReadDTO>>> GetAllCustomers()
        {
            var customers = await _service.GetCustomers();
            return Ok(_mapper.Map<IEnumerable<CustomerReadDTO>>(customers));
        }

        //GET api/customers/{id}
        [HttpGet("api/customers/{id:Guid}")]
        public async Task<ActionResult<CustomerReadDTO>> GetCustomerById(Guid id)
        {
            var customer = await _service.GetCustomer(id);
            if(customer != null) return Ok(_mapper.Map<CustomerReadDTO>(customer));
            return NotFound();
        }

        //POST api/customers
        [HttpPost]
        public async Task<ActionResult<CustomerReadDTO>> CreateCustomer(CustomerCreateDTO customerCreateDto)
        {
            var customerModel = _mapper.Map<Customer>(customerCreateDto);
            _service.CreateCustomer(customerModel);
            await _service.Complete();

            return Ok(_mapper.Map<CustomerReadDTO>(customerModel));
        }
    }
}
