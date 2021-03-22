using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Biblioteka2.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "To pole nie może być puste")]
            [EmailAddress(ErrorMessage = "Adres email ma niewłaściwy format")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "To pole nie może być puste")]
            [Display(Name = "Imię")]
            [StringLength(100, ErrorMessage = "{0} nie może zawierać więce niż {1} znaków")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "To pole nie może być puste")]
            [Display(Name = "Nazwisko")]
            [StringLength(100, ErrorMessage = "{0} nie może zawierać więce niż {1} znaków")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "To pole nie może być puste")]
            [StringLength(100, ErrorMessage = "The {0} powinno co najmniej {2} i nie więcej niż {1} znaków.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Hasło")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potwierdzenie hasła")]
            [Compare("Password", ErrorMessage = "Hasła nie są identyczne.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Identity/Account/Login");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Email,
                    FristName = Input.FirstName,
                    LastName = Input.LastName,
                    Email = Input.Email,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
