using E_commerceApp.Core.Entities;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.Email;
using E_commerceApp.Service.Dtos.UserDtos;
using E_commerceApp.Service.Exceptions;
using E_commerceApp.Service.Implementations;
using E_commerceApp.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IMailService _mailService;
        private readonly JwtService _jwtService;


        public UsersController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager ,IUserService userService, SignInManager<AppUser> signInManager,IEmailService emailService, IMailService mailService, JwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
            _signInManager = signInManager;
            _emailService = emailService;
            _mailService = mailService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto registerDto)
        {

            var id = await _userService.Register(registerDto);


            return StatusCode(201, new { Id = id });

        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDto loginDto)
        {
            return Ok(await _userService.Login(loginDto));

             
        }

        [HttpPost("EmailConfirm")]
        public async Task<IActionResult> EmailConfirm(UserConfirmDto confirmDto)
        {
            AppUser user = await _userManager.FindByNameAsync(confirmDto.UserName);
            if (user == null)
                return NotFound();
            if (user.MailConfirmCode != confirmDto.Code)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "ConfirmCode", "Code is not true!");
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _userManager.ConfirmEmailAsync(user, token);
            return NoContent();
        }

        [HttpPost("AgainEmailConfirm")]
        public async Task<IActionResult> AgainEmailConfirm(UserSendAgainDto sendAgainDto)
        {
            AppUser user = await _userManager.FindByIdAsync(sendAgainDto.UserId);
            if (user == null)
                return NotFound();
            int code = new Random().Next(1000, 9999);
            user.MailConfirmCode = code;
            await _userManager.UpdateAsync(user);

            var message = new Message(new string[] { user.Email! }, "OTP Confrimation", user.MailConfirmCode);
            _emailService.SendEmail(message);
            return NoContent();
        }

        [Authorize(Roles = "Member")]
        [HttpGet("")]
        public async Task<ActionResult<UserGetDto>> Get()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var userGetDto = new UserGetDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

            };

            return Ok(userGetDto);

        }

        [Authorize(Roles = "Member")]
        [HttpPut("")]
        public async Task<IActionResult> Update(UserProfileDto profileDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return NotFound();


            if (profileDto.CurrentPassword != null)
            {
                var resultCurrentP = await _signInManager.CheckPasswordSignInAsync(user, profileDto.CurrentPassword, false);
                if (!resultCurrentP.Succeeded)
                {
                    //ModelState.AddModelError("Current Password", "Current Password is not correct");
                    //return BadRequest(ModelState);
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, "CurrentPassword", "Current Password is not correct");
                }
            }
            if (profileDto.NewPassword != null && profileDto.NewPassword == profileDto.ConfirmPassword)
              await _userManager.ChangePasswordAsync(user, profileDto.CurrentPassword, profileDto.NewPassword);
            else
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "", "New Password and Confirm Password is not same");


            user.FirstName = profileDto.FirstName;
            user.LastName = profileDto.LastName;
            user.Email = profileDto.Email;
            user.UserName = profileDto.UserName;
            user.PhoneNumber = profileDto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, "", $"{err.Description}");

                 
                }
            }


            await _signInManager.SignInAsync(user, false);

            return NoContent();


        }



        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(UserForgotPasswordDto forgotPasswordDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                return BadRequest();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            token = WebEncoders.Base64UrlEncode(tokenBytes);

            var url = "http://localhost:3000/reset-password/" + user.Id + "/" + token;


            await _mailService.SendEmailAsync(new MailRequest
            {

                ToEmail = user.Email,
                Subject = "Change Password",
                Body = $"<h3>Hello {user.LastName} {user.FirstName},</h3><br/><h4></h4>Follow this link to change your password <a href={url}>click</h1>."

        });
            return NoContent();

        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] UserResetPasswordDto dto)
        {
            AppUser user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
            {
                return BadRequest();
            }
            byte[] byteToken = WebEncoders.Base64UrlDecode(dto.Token);
            var tokenmn = Encoding.UTF8.GetString(byteToken);

            if (dto.Password != dto.ConfirmPassword)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Wrong Password");
            }

            var result = await _userManager.ResetPasswordAsync(user, tokenmn, dto.Password);

            if (!result.Succeeded)
            {
               
                foreach (var error in result.Errors)
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, "", $"{error.Description}");

                }
                
            }

            return NoContent();

        }



        


    }
}
