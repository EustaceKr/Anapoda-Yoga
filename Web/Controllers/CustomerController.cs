using Application.DTOs.CustomerDTOs;
using Application.Services.CustomerImplementation;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet("api/customers/{id:Guid}", Name = "GetCustomerById")]
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

            var customerReadDto = _mapper.Map<CustomerReadDTO>(customerModel);

            return CreatedAtRoute(nameof(GetCustomerById), new { Id = customerReadDto.Id }, customerReadDto);
        }

        //PUT api/customer/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(Guid id, CustomerUpdateDTO customerUpdateDto)
        {
            var customer = await _service.GetCustomer(id);
            if(customer == null) return NotFound();

            _mapper.Map(customerUpdateDto, customer);
            _service.UpdateCustomer(customer);
            await _service.Complete();
            return NoContent();
        }

        //PATCH api/customers/{id}
        //"op":"replace",
        //"path":"/firstName",
        //"value":"Lalala"
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialCustomerUpdate(Guid id, JsonPatchDocument<CustomerUpdateDTO> patchDoc)
        {
            var customer = await _service.GetCustomer(id);
            if (customer == null) return NotFound();

            var customerToPatch = _mapper.Map<CustomerUpdateDTO>(customer);
            patchDoc.ApplyTo(customerToPatch, ModelState);
            if (!TryValidateModel(customerToPatch)) return ValidationProblem(ModelState);

            _mapper.Map(customerToPatch, customer);
            _service.UpdateCustomer(customer);
            await _service.Complete();
            return NoContent();
        }

        //DELETE api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _service.GetCustomer(id);
            if (customer == null) return NotFound();

            _service.DeleteCustomer(customer);
            await _service.Complete();
            return NoContent();
        }
    }
}
