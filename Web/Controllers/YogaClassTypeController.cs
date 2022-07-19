using Application.Services.YogaClassTypesImplementation;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.YogaClassTypeDTOs;
using Data.Entities;
using Application.UserAuthentication;
using Microsoft.AspNetCore.JsonPatch;

namespace Web.Controllers
{
    [Route("api/yogaclasstypes")]
    [ApiController]
    public class YogaClassTypeController : Controller
    {
        private readonly IYogaClassTypeService _service;
        private readonly IMapper _mapper;
        public YogaClassTypeController(IYogaClassTypeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //GET api/yogaclasstypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YogaClassTypeReadDTO>>> GetAllYogaClassTypes()
        {
            var yogaClassTypes = await _service.GetYogaClassTypes();
            return Ok(_mapper.Map<IEnumerable<YogaClassTypeReadDTO>>(yogaClassTypes));
        }

        //GET api/yogaclasstype/{id}
        [HttpGet("{id:Guid}", Name = "GetYogaClassTypeById")]
        public async Task<ActionResult<YogaClassTypeReadDTO>> GetYogaClassTypeById(string id)
        {
            var yogaClassType = await _service.GetYogaClassType(id);
            if (yogaClassType != null) return Ok(_mapper.Map<YogaClassTypeReadDTO>(yogaClassType));
            return NotFound();
        }

        //POST api/yogaclasstypes
        [HttpPost]
        public async Task<ActionResult<YogaClassTypeReadDTO>> CreateYogaClassType(YogaClassTypeCreateDTO yogaClassTypeCreateDto)
        {
            var yogaClassTypeModel = _mapper.Map<YogaClassType>(yogaClassTypeCreateDto);
            _service.CreateYogaClassType(yogaClassTypeModel);
            if(await _service.Complete())
            {
                var yogaClassTypeReadDto = _mapper.Map<YogaClassTypeReadDTO>(yogaClassTypeModel);
                return CreatedAtRoute(nameof(GetYogaClassTypeById), new { Id = yogaClassTypeReadDto.YogaClassTypeId }, yogaClassTypeReadDto);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Yoga Class Type creation failed! Please check the details and try again." });
        }

        //PUT api/yogaclasstypes/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateYogaClassType(string id, YogaClassTypeUpdateDTO yogaClassTypeUpdateDto)
        {
            var yogaClassType = await _service.GetYogaClassType(id);
            if (yogaClassType == null) return NotFound();

            _mapper.Map(yogaClassTypeUpdateDto, yogaClassType);
            _service.UpdateYogaClassType(yogaClassType);
            await _service.Complete();
            return NoContent();
        }

        //PATCH api/yogaclasstypes/{id}
        //"op":"replace",
        //"path":"/Name",
        //"value":"Lalala"
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialΥogaClassTypeUpdate(string id, JsonPatchDocument<YogaClassTypeUpdateDTO> patchDoc)
        {
            var yogaClassType = await _service.GetYogaClassType(id);
            if (yogaClassType == null) return NotFound();

            var yogaClassTypeToPatch = _mapper.Map<YogaClassTypeUpdateDTO>(yogaClassType);
            patchDoc.ApplyTo(yogaClassTypeToPatch, ModelState);
            if (!TryValidateModel(yogaClassTypeToPatch)) return ValidationProblem(ModelState);

            _mapper.Map(yogaClassTypeToPatch, yogaClassType);
            _service.UpdateYogaClassType(yogaClassType);
            await _service.Complete();
            return NoContent();
        }

        //DELETE api/yogaclasstypes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteYogaClassType(string id)
        {
            var yogaClassType = await _service.GetYogaClassType(id);
            if (yogaClassType == null) return NotFound();

            _service.DeleteYogaClassType(yogaClassType);
            await _service.Complete();
            return NoContent();
        }
    }
}
