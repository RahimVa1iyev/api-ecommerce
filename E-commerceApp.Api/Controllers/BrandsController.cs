using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.BrandDtos;
using E_commerceApp.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceApp.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("{id}")]


        public ActionResult<BrandGetDto> Get(int id)
        {
            return  Ok(_brandService.Get(id));
       
        }


        [HttpGet("all")]
        public ActionResult<BrandGetAllDto> GetAll()
       {
            return Ok(_brandService.GetAll());
        }

        [HttpPost("")]
        public IActionResult Create(BrandPostDto postDto)
        {
            var brandId = _brandService.Create(postDto);
            return StatusCode(201,brandId);

        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id,BrandPutDto putDto)
        {
            _brandService.Edit(id,putDto);

            return NoContent();
            
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _brandService.Delete(id);

            return NoContent();
        }

        [HttpGet("")]
        public ActionResult<PaginatedListDto<BrandGetPaginatedDto>> GetAll(int page = 1)
        {
            return Ok(_brandService.GetPaginated(page));
        }

    }
}
