using AutoMapper;
using E_commerceApp.Core.Entities;
using E_commerceApp.Data.DAL;
using E_commerceApp.Service.Dtos.Features;
using E_commerceApp.Service.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly WatchesDbContext _context;
        private readonly IMapper _mapper;

        public FeaturesController(WatchesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("all")]

        public IActionResult GetAll()
        {
            var features = _context.Features.ToList();

            var dto = _mapper.Map<List<FeatureGetAllDto>>(features);

            return Ok(dto);
        }

        [HttpPost("")]
        public IActionResult Create([FromForm] FeatureCreateDto createDto)
        {
            var rootPath = Directory.GetCurrentDirectory() + "/wwwroot";

            Feature feature = new Feature()
            {
                FTitle = createDto.FTitle,
                STitle = createDto.STitle,
                TTitle = createDto.TTitle,
                Desc = createDto.Desc,
                ImageUri = FileManager.Save(createDto.ImageUri, rootPath, "uploads/features")
            };

            _context.Features.Add(feature);
            _context.SaveChanges();

            return StatusCode(201, new { Id = feature.Id });

        }
    }
}
