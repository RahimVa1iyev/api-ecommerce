using E_commerceApp.Service.Dtos.SliderDtos;
using E_commerceApp.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly ISliderService _sliderService;

        public SlidersController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        [HttpPost("")]
        public IActionResult Create([FromForm] SliderPostDto postDto)
        {
            var id = _sliderService.Create(postDto);

            return StatusCode(201, id);
        }
        [HttpGet("all")]
        public ActionResult<List<SliderGetAllDto>> GetAll()
        {
            return Ok(_sliderService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<SliderGetDto> Get(int id)
        {
            return Ok(_sliderService.Get(id));
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromForm] SliderPutDto putDto)
        {
            _sliderService.Edit(id, putDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _sliderService.Delete(id);

            return NoContent();
        }
    }
}
