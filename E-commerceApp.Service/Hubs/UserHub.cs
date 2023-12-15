using E_commerceApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Hubs
{
    public class UserHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public UserHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
      

        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(Context.User.Identity.Name);
                user.IsLogin = true;
                await _userManager.UpdateAsync(user);
            }
        }

    }
}
