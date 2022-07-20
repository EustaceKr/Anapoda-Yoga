using Application.DTOs.ReservationDTOs;
using Application.Services.CustomerImplementation;
using Application.Services.ReservationImplementation;
using Application.UserAuthentication;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _service;
        private readonly ICustomerService _customerService;
        private readonly UserManager<Customer> _userManager;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService service ,UserManager<Customer> userManager, ICustomerService customerService, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationReadDTO>>> GetAllReservationsByUser()
        {
            var user = await _customerService.GetUserIdFromUserName(User.Identity.Name);
            if (user == null) return NotFound();
            var reservations = await _service.GetReservationsByUser(user.Id);
            if (reservations != null) return Ok(_mapper.Map<IEnumerable<ReservationReadDTO>>(reservations));
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ReservationReadDTO>> CreateReservation(ReservationCreateDTO reservationCreateDto)
        {
            var user = await _customerService.GetUserIdFromUserName(User.Identity.Name);
            var reservationModel = _mapper.Map<Reservation>(reservationCreateDto);
            reservationModel.CustomerId = user.Id;
            reservationModel.CreatedAt = DateTime.Now;
            if (await _service.CheckReservation(reservationModel, user))
            {
                _service.CreateReservation(reservationModel);
                if (await _service.Complete())
                {
                    return Ok();
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Reservation creation failed! Please check the details and try again." });
        }
    }
}
