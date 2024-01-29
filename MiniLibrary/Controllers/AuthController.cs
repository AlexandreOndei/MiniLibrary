using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MiniLibrary.Entity;
using MiniLibrary.Models;
using MiniLibrary.Service.Interfaces;
using System.Security.Claims;

namespace MiniLibrary.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        private async Task<ResponseViewModel> ValidateLoginAsync(User user)
        {
            ResponseViewModel response = new();

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                response.Success = false;
                response.Message = "Email not informed or invalid.";
                response.Element = "#inputEmail";
            }
            else if (string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                response.Success = false;
                response.Message = "Password not informed or invalid.";
                response.Element = "#inputPassword";
            }

            User _user = await _authService.GetUserByEmailAsync(user.Email);

            if (_user == null)
            {
                response.Success = false;
                response.Message = "Email not informed or invalid.";
                response.Element = "#inputEmail";
            }
            else if (!_user.PasswordHash.Equals(user.PasswordHash))
            {
                response.Success = false;
                response.Message = "Password not informed or invalid.";
                response.Element = "#inputPassword";
            }

            return response;
        }

        private async Task<ResponseViewModel> ValidateRegistrationAsync(User user)
        {
            ResponseViewModel response = new();

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                response.Success = false;
                response.Message = "Email not informed.";
                response.Element = "#inputEmail";
            }
            else if (string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                response.Success = false;
                response.Message = "Password not informed.";
                response.Element = "#inputPassword";
            }
            else if (string.IsNullOrWhiteSpace(user.PasswordHashConfirmation))
            {
                response.Success = false;
                response.Message = "Password Confirmation not informed.";
                response.Element = "#inputPasswordConfirmation";
            }
            else if (!user.PasswordHash.Equals(user.PasswordHashConfirmation))
            {
                response.Success = false;
                response.Message = "Password and Password Confirmation didn't match.";
                response.Element = "#inputPasswordConfirmation";
            }

            return response;
        }

        private async Task SignInUserAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                  new ClaimsPrincipal(claimsIdentity),
                  authProperties
            );
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user, string returnUrl = null)
        {
            ResponseViewModel response = await ValidateLoginAsync(user);

            if (response.Success)
                await SignInUserAsync(user);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Json(new ResponseViewModel());
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            ResponseViewModel response = await ValidateRegistrationAsync(user);

            if (response.Success)
            {
                user.Id = Guid.NewGuid().ToString().Replace("-", "");
                User addedUser = await _authService.RegisterUserAsync(user);
                if (addedUser == null)
                {
                    response = new ResponseViewModel
                    {
                        Success = false,
                        Message = "Error adding user."
                    };
                }
                else
                    await SignInUserAsync(addedUser);
            }

            return Json(response);
        }
    }
}
