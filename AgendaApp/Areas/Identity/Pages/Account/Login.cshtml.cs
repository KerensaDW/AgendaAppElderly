using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AgendaApp.Models;
using System.Security.Claims;

namespace AgendaApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel: PageModel
    {
        private readonly UserManager<Gebruiker> _userManager;
        private readonly SignInManager<Gebruiker> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IGebruikerRepository _gebruikerRepository;

        public LoginModel(SignInManager<Gebruiker> signInManager, UserManager<Gebruiker> userManager, ILogger<LoginModel> logger
            , IGebruikerRepository gebruikerRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
           
            _gebruikerRepository = gebruikerRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }


        public class InputModel
        {
            [Required(ErrorMessage = "Gelieve een gebruikersnaam in te vullen")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Gelieve een wachtwoord in te vullen")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            returnUrl = returnUrl ?? Url.Content("~/");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password,false,true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(Input.UserName);
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Gebruiker"));
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    await _signInManager.RefreshSignInAsync(user);

                    _logger.LogInformation("U bent ingelogd.");
                    return LocalRedirect(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    Gebruiker gebruiker = await _userManager.FindByNameAsync(Input.UserName);
                    gebruiker.Status = "GEBLOKKEERD";
                    _gebruikerRepository.SaveChanges();
                    _logger.LogWarning("Uw account is geblokkeerd.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    var res = await _userManager.FindByNameAsync(Input.UserName);
                    if (res != null)
                    {
                       
                        ModelState.AddModelError(string.Empty, "Foutief wachtwoord.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Foutieve gebruikersnaam.");
                    }
                    return Page();
                }
            }
            return Page();
        }

    }
}
