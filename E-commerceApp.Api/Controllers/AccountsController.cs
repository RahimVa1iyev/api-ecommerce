using E_commerceApp.Core.Entities;
using E_commerceApp.Service.Dtos.AccountDtos;
using E_commerceApp.Service.Exceptions;
using E_commerceApp.Service.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtService _jwtService;
        private readonly IConfiguration _configuration;

        public AccountsController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, JwtService jwtService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpGet("createrole")]
        public async Task<IActionResult> CreteRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            //await _roleManager.CreateAsync(new IdentityRole("Member"));

            return Ok();

        }

        //[HttpGet("createadmin")]
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser user = new AppUser()
        //    {
        //        UserName = "SuperAdmin",
        //        IsAdmin = true,
        //    };

        //    await _userManager.CreateAsync(user, "Admin123");
        //    await _userManager.AddToRoleAsync(user, "SuperAdmin");

        //    return Ok();
        //}


        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountLoginDto loginDto)
        {
            AppUser admin = await _userManager.FindByNameAsync(loginDto.UserName);

            if (admin == null)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "", "Username or Password is not correct");
            }

            var result = await _userManager.CheckPasswordAsync(admin, loginDto.Password);

            if (!result)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "", "Username or Password is not correct");

            }

            return Ok(new { token = await _jwtService.GenerateToken(admin) });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("")]
        public async Task<IActionResult> Create(AccountCreateAdminDto createAdminDto)
        {
            AppUser admin = new AppUser
            {
                FirstName = createAdminDto.FirstName,
                LastName = createAdminDto.LastName,
                UserName = createAdminDto.UserName,
                IsAdmin =true
            };

            var resultPassword = await _userManager.CreateAsync(admin, createAdminDto.Password);

            if (!resultPassword.Succeeded)
            {
                foreach (var err in resultPassword.Errors)
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, "", $"{err.Description}");

               
                }
            }

            var resultRole = await _userManager.AddToRoleAsync(admin, createAdminDto.Role);

            if (!resultRole.Succeeded)
            {
                foreach (var err in resultRole.Errors)
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, "", $"{err.Description}");

                }
            }

            return StatusCode(201, admin.Id);


        }

        [HttpGet("all")]
        public async Task<ActionResult<List<AccountAdminGetDto>>> GetAll()
        {
            var admins = _userManager.Users.Where(x => x.IsAdmin == true && x.UserName != "SuperAdmin").ToList();

            List<AccountAdminGetDto> getDto = new List<AccountAdminGetDto>();

            foreach (var admin in admins)
            {
                AccountAdminGetDto adminDto = new AccountAdminGetDto()
                {
                    Id = admin.Id,
                    UserName = admin.UserName,
                    FullName = admin.FirstName + " " + admin.LastName,

                };

                adminDto.Role = await _userManager.GetRolesAsync(admin);

                getDto.Add(adminDto);

            }

            return Ok(getDto);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);

            return NoContent();
        }


        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var users =  _userManager.Users.Where(x => x.IsAdmin == false).ToList();

            return Ok(new {count = users.Count});
        }


    }
}
