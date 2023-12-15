using AutoMapper;
using E_commerceApp.Data.DAL;
using E_commerceApp.Service.Dtos.Infos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfosController : ControllerBase
    {
        private readonly WatchesDbContext _context;
        private readonly IMapper _mapper;

        public InfosController(WatchesDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var info = _context.Infos.ToList();
            return Ok(_mapper.Map<List<InfoGetDto>>(info));
        }
    }
}
