using AutoMapper;
using E_commerceApp.Core.Enums;
using E_commerceApp.Service.Dtos.Gender;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly IMapper _mapper;

        public GendersController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet("all")]
        public ActionResult<List<GenderGetAllDto>> GetAll()
        {
            var genderStatusValues = Enum.GetValues(typeof(GenderStatus)).Cast<GenderStatus>();
            var getAlldto = _mapper.Map<List<GenderGetAllDto>>(genderStatusValues);

            return getAlldto;
        }
    }
}
