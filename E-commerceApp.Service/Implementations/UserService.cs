using E_commerceApp.Core.Entities;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.Email;
using E_commerceApp.Service.Dtos.UserDtos;
using E_commerceApp.Service.Exceptions;
using E_commerceApp.Service.Hubs;
using E_commerceApp.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<UserHub> _hubContext;

        public UserService(UserManager<AppUser> userManager, JwtService jwtService,IEmailService emailService,IHttpContextAccessor httpContextAccessor,IHubContext<UserHub> hubContext)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _hubContext = hubContext;
        }



        public async Task<GenerateCreateToken> Login(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);


            if (user == null || user.IsAdmin)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Username or Password", "Username or Password is not correct");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Username or Password", "Username or Password is not correct");

           


            return new GenerateCreateToken { Token = await _jwtService.GenerateToken(user) };
        }

        public  async Task<GenerateUserId> Register(UserRegisterDto registerDto)
        {
            var memberEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (memberEmail != null)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Email", "Email is already exist");

            var existingUser = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existingUser != null)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Username", "Username is already exists");

            int code = new Random().Next(1, 9999);

            AppUser user = new AppUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                TwoFactorEnabled = true,
                IsAdmin = false,
                MailConfirmCode = code,
                
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, "", $"{err.Description}");
                }
            }

            await _userManager.AddToRoleAsync(user, "Member");

            var message = new Message(new string[] { user.Email! }, "OTP Confrimation", user.MailConfirmCode);
            _emailService.SendEmail(message);

            return new GenerateUserId { Id = user.Id };
        }

    

    }
}
