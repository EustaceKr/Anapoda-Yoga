using Application.DTOs.ReservationDTOs;
using Application.Services.CustomerImplementation;
using Application.Services.ReservationImplementation;
using Application.Services.YogaClassImplementation;
using Application.Services.YogaClassTypesImplementation;
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
    //[Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _service;
        private readonly ICustomerService _customerService;
        private readonly IYogaClassService _yogaClassService;
        private readonly IYogaClassTypeService _yogaClassTypeService;
        private readonly UserManager<Customer> _userManager;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService service , IYogaClassService yogaClassService, IYogaClassTypeService yogaClassTypeService,UserManager<Customer> userManager, ICustomerService customerService, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
            _customerService = customerService;
            _yogaClassService = yogaClassService;
            _yogaClassTypeService = yogaClassTypeService;
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

            YogaClass yogaClass = await _yogaClassService.GetYogaClass(reservationModel.YogaClassId);
            yogaClass.YogaClassType = await _yogaClassTypeService.GetYogaClassType(yogaClass.YogaClassTypeId);
            yogaClass.Reservations = (await _service.GetReservationsByClassAsync(reservationModel.YogaClassId)).ToList();

            if (await _service.CheckReservationExists(reservationModel, user) && await _service.CheckCapacity(yogaClass))
            {
                reservationModel.Customer = user;
                _service.CreateReservation(reservationModel);
                if (await _service.Complete())
                {
                    return Ok();
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Reservation creation failed! Please check the details and try again." });
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteReservation(string yogaClassId)
        {
            var user = await _customerService.GetUserIdFromUserName(User.Identity.Name);
            var reservation = await _service.GetReservationByClassAndCustomer(yogaClassId, user.Id);
            if (reservation == null) return NotFound();

            YogaClass yogaClass = await _yogaClassService.GetYogaClass(reservation.YogaClassId);

            if(DateTime.Now.AddHours(2) < yogaClass.Date)
            {
                reservation.Customer = user;
                _service.DeleteReservation(reservation);
                await _service.Complete();
                return Ok();
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Reservation cancelation failed! Please check the details and try again." });

        }

        [HttpPost("admin")]
        public async Task<ActionResult<ReservationReadDTO>> AdminCreateReservation(ReservationAdminCreateDTO reservationAdminCreateDto)
        {
            var reservationModel = _mapper.Map<Reservation>(reservationAdminCreateDto);
            reservationModel.CreatedAt = DateTime.Now;

            var user = await _customerService.GetCustomer(reservationModel.CustomerId);
            if (await _service.CheckReservationExists(reservationModel, user))
            {
                reservationModel.Customer = user;
                _service.CreateReservation(reservationModel);
                if (await _service.Complete())
                {
                    return Ok();
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Reservation creation failed! Please check the details and try again." });
        }
        [HttpDelete("admin")]
        public async Task<ActionResult> AdminDeleteReservation(string yogaClassId, string customerId)
        {
            var reservation = await _service.GetReservationByClassAndCustomer(yogaClassId, customerId);
            if (reservation == null) return NotFound();

            reservation.Customer = await _customerService.GetCustomer(reservation.CustomerId);
            _service.DeleteReservation(reservation);
            await _service.Complete();
            return Ok();
        }
    }
}
