using AutoMapper;
using E_commerceApp.Core.Entities;
using E_commerceApp.Data.DAL;
using E_commerceApp.Service.Dtos.Offers;
using E_commerceApp.Service.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly WatchesDbContext _context;
        private readonly IMapper _mapper;

        public OffersController(WatchesDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("all")]

        public IActionResult GetAll()
        {
            var offers = _context.Offers.ToList();

            var dto = _mapper.Map<List<OfferGetAllDto>>(offers);

            return Ok(dto);
        }

        [HttpPost("")]
        public IActionResult Create([FromForm] OfferCreateDto createDto)
        {
            var rootPath = Directory.GetCurrentDirectory() + "/wwwroot";

            Offer offer = new Offer()
            {
                Desc = createDto.Desc,
                Title = createDto.Title,
                Icon = FileManager.Save(createDto.Icon, rootPath, "uploads/offers")
            };

            _context.Offers.Add(offer);
            _context.SaveChanges();

            return StatusCode(201, new { Id = offer.Id });
            
        }
    }
}
