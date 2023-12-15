using E_commerceApp.Service.Dtos.SizeDtos;
using E_commerceApp.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizesController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet("{id}")]
        public ActionResult<SizeGetDto> Get(int id)
        {
           return Ok( _sizeService.Get(id));
        }

        [HttpGet("all")]
        public ActionResult<List<SizeGetAllDto>> GetAll()
        {
            return Ok(_sizeService.GetAll());   
        }

        [HttpPost("")]
        public IActionResult Create(SizePostDto postDto)
        {
            var id = _sizeService.Create(postDto);

            return StatusCode(201, id);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id , SizePutDto putDto)
        {
            _sizeService.Edit(id, putDto);
            return NoContent();
        }
        [HttpDelete("{id}")] 

        public IActionResult Delete(int id)
        {
            _sizeService.Delete(id);
            return NoContent();
        }
    }
}
