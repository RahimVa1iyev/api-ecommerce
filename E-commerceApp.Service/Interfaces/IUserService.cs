using E_commerceApp.Core.Entities;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Interfaces
{
    public interface IUserService
    {
        Task<GenerateUserId> Register(UserRegisterDto registerDto);

         Task<GenerateCreateToken> Login(UserLoginDto loginDto);





    }
}
