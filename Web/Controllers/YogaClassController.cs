using Application.DTOs.YogaClassDTOs;
using Application.DTOs.YogaClassTypeDTOs;
using Application.Services.YogaClassImplementation;
using Application.UserAuthentication;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/yogaclasses")]
    [ApiController]
    public class YogaClassController : Controller
    {
        private readonly IYogaClassService _service;
        private readonly IMapper _mapper;
        public YogaClassController(IYogaClassService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        //GET api/yogaclasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YogaClassReadDTO>>> GetAllYogaClasses()
        {
            var yogaClasses = await _service.GetAllYogaClasses();
            return Ok(_mapper.Map<IEnumerable<YogaClassReadDTO>>(yogaClasses));
        }

        //GET api/yogaclasses/{time}
        [HttpGet("{time}")]
        public async Task<ActionResult<IEnumerable<YogaClassReadDTO>>> GetYogaClassesByTime(DateTime time)
        {
            var yogaClasses = await _service.GetYogaClassesByDate(time);
            return Ok(_mapper.Map<IEnumerable<YogaClassReadDTO>>(yogaClasses));
        }

        //GET api/yogaclass/{id}
        [HttpGet("{id:Guid}", Name = "GetYogaClassById")]
        public async Task<ActionResult<YogaClassReadDTO>> GetYogaClassById(string id)
        {
            var yogaClass = await _service.GetYogaClass(id);
            if (yogaClass != null) return Ok(_mapper.Map<YogaClassReadDTO>(yogaClass));
            return NotFound();
        }

        //POST api/yogaclasses
        [HttpPost]
        public async Task<ActionResult<YogaClassReadDTO>> CreateYogaClass(YogaClassCreateDTO yogaClassCreateDto)
        {
            var yogaClassModel = _mapper.Map<YogaClass>(yogaClassCreateDto);
            _service.CreateYogaClass(yogaClassModel);
            if (await _service.Complete())
            {
                var yogaClassReadDto = _mapper.Map<YogaClassReadDTO>(yogaClassModel);
                return CreatedAtRoute(nameof(GetYogaClassById), new { Id = yogaClassReadDto.YogaClassId }, yogaClassReadDto);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Yoga Class creation failed! Please check the details and try again." });
        }

        //PUT api/yogaclasses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateYogaClass(string id, YogaClassUpdateDTO yogaClassUpdateDto)
        {
            var yogaClass = await _service.GetYogaClass(id);
            if (yogaClass == null) return NotFound();

            _mapper.Map(yogaClassUpdateDto, yogaClass);
            _service.UpdateYogaClass(yogaClass);
            await _service.Complete();
            return Ok();
        }

        //PATCH api/yogaclasses/{id}
        //"op":"replace",
        //"path":"/Name",
        //"value":"Lalala"
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialΥogaClassUpdate(string id, JsonPatchDocument<YogaClassUpdateDTO> patchDoc)
        {
            var yogaClass = await _service.GetYogaClass(id);
            if (yogaClass == null) return NotFound();

            var yogaClassToPatch = _mapper.Map<YogaClassUpdateDTO>(yogaClass);
            patchDoc.ApplyTo(yogaClassToPatch, ModelState);
            if (!TryValidateModel(yogaClassToPatch)) return ValidationProblem(ModelState);

            _mapper.Map(yogaClassToPatch, yogaClass);
            _service.UpdateYogaClass(yogaClass);
            await _service.Complete();
            return Ok();
        }

        //DELETE api/yogaclasses/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteYogaClass(string id)
        {
            var yogaClass = await _service.GetYogaClass(id);
            if (yogaClass == null) return NotFound();

            _service.DeleteYogaClass(yogaClass);
            await _service.Complete();
            return Ok();
        }
    }
}
