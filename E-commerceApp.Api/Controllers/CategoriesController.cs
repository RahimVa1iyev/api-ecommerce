using E_commerceApp.Service.Dtos.CategoryDtos;
using E_commerceApp.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceApp.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryGetDto> Get(int id)
        {
           return Ok(_categoryService.Get(id));

        }

        [HttpGet("all")]
        public ActionResult<List<CategoryGetAllDto>> GetAll()
        {
            return Ok(_categoryService.GetAll());
        }

        [HttpPost("")]
        public IActionResult Create(CategoryPostDto postDto)
        {
            var id = _categoryService.Create(postDto);

            return StatusCode(201, id);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id , CategoryPutDto putDto)
        {
            _categoryService.Edit(id, putDto);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);

            return NoContent();
        }
    }
}
