using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto.AccountsDtos;
using Filminurk.Core.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace Filminurk.ApplicationServices.Services
{
    public class AccountsServices : IAccountsServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailsServices _emailsService;

        public AccountsServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailsServices emailsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailsService = emailsService;
        }
        public async Task<ApplicationUser> Register(ApplicationUserDto userDto)
        {
            var user = new ApplicationUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                ProfileType = userDto.ProfileType,
                DisplayName = userDto.DisplayName,
            };
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // HOMEWORK LOCATION
            }
            return user;
        }

        // HOMEWORK LOCATION
        public async Task<ApplicationUser> Login(LoginDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            return user;
        }
    }
}
