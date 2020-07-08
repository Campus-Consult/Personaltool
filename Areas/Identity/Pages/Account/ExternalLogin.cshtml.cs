using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Personaltool.Data;
using Personaltool.Helpers;
using Personaltool.Models;

namespace Personaltool.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;

        public static readonly String SURNAME_CLAIM = "family_name";
        public static readonly String GIVEN_NAME_CLAIM = "given_name";

        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            public Person Person { get; set; }
        }

        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor : true);
            if (result.Succeeded)
            {
                // Store the access token and resign in so the token is included in
                // in the cookie
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, 
                    info.ProviderKey);

                var props = new AuthenticationProperties();
                props.StoreTokens(info.AuthenticationTokens);

                await _signInManager.SignInAsync(user, props, info.LoginProvider);

                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", 
                    info.Principal.Identity.Name, info.LoginProvider);

                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then check if user infos are claimed.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email) &&
                    info.Principal.HasClaim(c => c.Type == SURNAME_CLAIM) &&
                    info.Principal.HasClaim(c => c.Type == GIVEN_NAME_CLAIM))
                {
                    Input = new InputModel
                    {
                        FirstName = info.Principal.FindFirstValue(GIVEN_NAME_CLAIM),
                        LastName = info.Principal.FindFirstValue(SURNAME_CLAIM),
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    };


                    // Connect or create person with this user
                    LinkPerson(info);

                    // If user infos are claimed, create the user directly 
                    await CreateUserAsync(info);

                    return LocalRedirect(returnUrl);
                } 
                else
                {
                    // this should be unreachable, claims should always be there
                    // and if they're not it's an error
                    return Page();
                }
            }
        }

        private void LinkPerson(ExternalLoginInfo info)
        {
            var person = _context.Persons.FirstOrDefault(x => x.EmailAssociaton == Input.Email);
            if(person is null)
            {
                var graphClient = GraphSdkHelper.GetAuthenticatedClient(info.AuthenticationProperties);
                var graphUser = graphClient.Me.Request().GetAsync().Result;

                person = new Person()
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    EmailAssociaton = Input.Email,
                    Gender = Gender.DIVERSE, // doesn't seem to be in the graph API
                    Birthdate = graphUser.Birthday?.DateTime ?? DateTime.MinValue,
                    AdressCity = graphUser.City,
                };
                _context.Persons.Add(person);
            }

            Input.Person = person;

        }


        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                await CreateUserAsync(info);
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task CreateUserAsync(ExternalLoginInfo info)
        {
            var user = new ApplicationUser { UserName = Input.FirstName.Trim().Replace(" ", "_", true, null), Email = Input.Email, Person = Input.Person };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    // Include the access token in the properties
                    var props = new AuthenticationProperties();
                    props.StoreTokens(info.AuthenticationTokens);
                    props.IsPersistent = true;
                    
                    _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                                        
                    await _signInManager.SignInAsync(user, props, info.LoginProvider);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
