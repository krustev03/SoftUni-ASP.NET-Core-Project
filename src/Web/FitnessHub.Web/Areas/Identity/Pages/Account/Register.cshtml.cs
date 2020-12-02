namespace FitnessHub.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Web.Enums;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    using static FitnessHub.Common.GlobalConstants;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._emailSender = emailSender;
            this.userRepository = userRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "The username is required.")]
            [Display(Name = "Username")]
            [MinLength(2, ErrorMessage = "The username must be between 2 and 35 characters.")]
            [MaxLength(35, ErrorMessage = "The username must be between 2 and 35 characters.")]
            [RegularExpression("([a-zA-Z][^\\s]*)", ErrorMessage = "The username cannot start with digit.")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Phone number is required.")]
            [Phone(ErrorMessage = "Not a valid phone number.")]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Choose your account purpose")]
            [EnumDataType(typeof(Role))]
            public Role RoleTypes { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = this.Input.UserName, Email = this.Input.Email, PhoneNumber = this.Input.PhoneNumber };

                var userExists = this.userRepository.AllWithDeleted().Any(x => x.UserName == user.UserName);

                if (userExists)
                {
                    this.ModelState.AddModelError(string.Empty, "User already exists");
                    return this.Page();
                }

                var emailExists = this.userRepository.AllWithDeleted().Any(x => x.Email == user.Email);

                if (emailExists)
                {
                    this.ModelState.AddModelError(string.Empty, "Email already taken");
                    return this.Page();
                }

                var result = await this._userManager.CreateAsync(user, this.Input.Password);
                string role = this.Input.RoleTypes.ToString();

                if (role == "Trainer")
                {
                    await _userManager.AddToRoleAsync(user, TrainerRoleName);
                }

                if (result.Succeeded)
                {
                    this._logger.LogInformation("User created a new account with password.");

                    var code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this._emailSender.SendEmailAsync(
                        this.Input.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this._userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this._signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
