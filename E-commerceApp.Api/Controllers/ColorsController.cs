using E_commerceApp.Service.Dtos.ColorDtos;
using E_commerceApp.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("{id}")]
        public ActionResult<ColorGetDto> Get(int id)
        {
            return Ok(_colorService.Get(id));
        }

        [HttpGet("all")]
        public ActionResult<ColorGetAllDto> GetAll()
        {
            return Ok(_colorService.GetAll());
        }
        [HttpPost("")]

        public IActionResult Create(ColorPostDto postDto)
        {
            var id = _colorService.Create(postDto);

            return StatusCode(201, id);

        }

        [HttpPut("{id}")]

        public IActionResult Edit(int id, ColorPutDto putDto)
        {
            _colorService.Edit(id, putDto);

            return NoContent();
        }
        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            _colorService.Delete(id);

            return NoContent();
        }
    }
}
