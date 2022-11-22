using Application.DTOs.CustomerDTOs;
using Application.Services.CustomerImplementation;
using Application.UserAuthentication;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/customers")]
    [ApiController]
    //[Authorize(Roles = CustomUserRoles.Admin)]
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerReadDTO>>> GetAllCustomers()
        {
            var customers = await _service.GetCustomers();
            return Ok(_mapper.Map<IEnumerable<CustomerReadDTO>>(customers));
        }

        [HttpGet("inYogaClass/{yogaClassId:Guid}", Name = "GetCustomersInClass")]
        public async Task<ActionResult<IEnumerable<CustomerReadDTO>>> GetCustomersInClass(string yogaClassId)
        {
            var customers = await _service.GetCustomersInClass(yogaClassId);
            if (customers != null) return Ok(_mapper.Map<IEnumerable<CustomerReadDTO>>(customers));
            return NotFound();
        }
        [HttpGet("notInYogaClass/{yogaClassId:Guid}", Name = "GetCustomersNotInClass")]
        public async Task<ActionResult<IEnumerable<CustomerReadDTO>>> GetCustomersNotInClass(string yogaClassId)
        {
            var customers = await _service.GetCustomersNotInClass(yogaClassId);
            if (customers != null) return Ok(_mapper.Map<IEnumerable<CustomerReadDTO>>(customers));
            return NotFound();
        }

        //GET api/customers/{id}
        [HttpGet("{id:Guid}", Name = "GetCustomerById")]
        public async Task<ActionResult<CustomerReadDTO>> GetCustomerById(string id)
        {
            var customer = await _service.GetCustomer(id);
            if (customer != null) return Ok(_mapper.Map<CustomerReadDTO>(customer));
            return NotFound();
        }

        //POST api/customers
        [HttpPost]
        public async Task<ActionResult<CustomerReadDTO>> CreateCustomer(CustomerCreateDTO customerCreateDto)
        {
            var customerModel = _mapper.Map<Customer>(customerCreateDto);
            var result = await _service.CreateCustomer(customerModel,customerCreateDto.Password);
            if (result.Succeeded) await _service.Complete();
            else return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            
            var customerReadDto = _mapper.Map<CustomerReadDTO>(customerModel);
            return CreatedAtRoute(nameof(GetCustomerById), new { Id = customerReadDto.Id }, customerReadDto);
        }

        //PUT api/customer/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(string id, CustomerUpdateDTO customerUpdateDto)
        {
            var customer = await _service.GetCustomer(id);
            if(customer == null) return NotFound();

            _mapper.Map(customerUpdateDto, customer);
            _service.UpdateCustomer(customer);
            await _service.Complete();
            return Ok();
        }

        //PATCH api/customers/{id}
        //"op":"replace",
        //"path":"/firstName",
        //"value":"Lalala"
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialCustomerUpdate(string id, JsonPatchDocument<CustomerUpdateDTO> patchDoc)
        {
            var customer = await _service.GetCustomer(id);
            if (customer == null) return NotFound();

            var customerToPatch = _mapper.Map<CustomerUpdateDTO>(customer);
            patchDoc.ApplyTo(customerToPatch, ModelState);
            if (!TryValidateModel(customerToPatch)) return ValidationProblem(ModelState);

            _mapper.Map(customerToPatch, customer);
            _service.UpdateCustomer(customer);
            await _service.Complete();
            return Ok();
        }

        //DELETE api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(string id)
        {
            var customer = await _service.GetCustomer(id);
            if (customer == null) return NotFound();

            _service.DeleteCustomer(customer);
            await _service.Complete();
            return Ok();
        }
    }
}
