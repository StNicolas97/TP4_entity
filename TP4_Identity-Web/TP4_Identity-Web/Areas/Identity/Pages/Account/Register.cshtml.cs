// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using TP4_Identity_Web.Data;
using TP4_Identity_Web.Models;

namespace TP4_Identity_Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Peuple")]
            public string Peuple { get; set; }


            [Required]
            [Display(Name = "Nom")]
            public string Nom { get; set; }

            [Required]
            [Display(Name = "Surnom")]
            public string Surnom { get; set; }

            [Display(Name = "Adresse")]
            public string? Adresse { get; set; }

            //Champs Gondor
            [Display(Name = "Rang")]
            public RangGondor? Rang { get; set; }

            [Display(Name = "Division")]
            public DivisionGondor? Division { get; set; }

            // Champs Elfe
            [Display(Name = "Royaume d'origine")]
            public RoyaumeElfe? RoyaumeDorigine { get; set; }

            [Display(Name = "Âge elfique")]
            public int? AgeElfique { get; set; }

            // Champs Hobbit
            [Display(Name = "Village d'origine")]
            public VillageHobbit? VillageOrigine { get; set; }

            [Display(Name = "Métier")]
            public MetierHobbit? MetierHobbit { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                ValidateTypeFields();



                if (!ModelState.IsValid)
                    return Page();

                var user = CreateUser();
                user.Email = Input.Email;
                user.UserName = Input.Email;
                user.Nom = Input.Nom;
                user.Surnom = Input.Surnom;
                user.Adresse = Input.Adresse;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Création de l'utilisateur spécialisé
                    var userId = await _userManager.GetUserIdAsync(user);

                    if (Input.Peuple == "Gondor")
                    {
                        _context.LesGuerriers.Add(new GuerrierDuGondor
                        {
                            Rang     = Input.Rang,
                            Division = Input.Division,
                            UserId   = userId
                        });
                        
                    }
                    else if (Input.Peuple == "Elfe")
                    {
                        _context.LesElfes.Add(new Elfe
                        {
                            RoyaumeDorigine = Input.RoyaumeDorigine,
                            AgeElfique      = Input.AgeElfique.Value,
                            UserId          = userId
                        });
                    }
                    else if (Input.Peuple == "Hobbit")
                    {
                        _context.LesHobbits.Add(new HobbitComte
                        {
                            VillageOrigine = Input.VillageOrigine,
                            MetierHobbit   = Input.MetierHobbit,
                            UserId         = userId
                        });
                    }

                    await _context.SaveChangesAsync();

                    
                    await _userManager.AddClaimAsync(user, new Claim("Peuple", Input.Peuple));

                    string role;
                    if (Input.Peuple == "Gondor" && Input.Rang == RangGondor.Intendant)
                        role = Roles.Roi;
                    else if (Input.Peuple == "Gondor" && Input.Rang == RangGondor.Capitaine)
                        role = Roles.Capitaine;
                    else
                        role = Roles.Habitant;

                    await _userManager.AddToRoleAsync(user, role);

                    _logger.LogInformation("User created a new account with password.");

                    //var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private void ValidateTypeFields()
        {
            if (Input.Peuple == "Gondor")
            {
                if (Input.Rang == null)
                    ModelState.AddModelError("Input.Rang", "Le rang est obligatoire.");
                if (Input.Division == null)
                    ModelState.AddModelError("Input.Division", "La division est obligatoire.");
            }
            else if (Input.Peuple == "Elfe")
            {
                if (Input.RoyaumeDorigine == null)
                    ModelState.AddModelError("Input.RoyaumeDorigine", "Le royaume est obligatoire.");
                if (Input.AgeElfique == null || Input.AgeElfique <= 0)
                    ModelState.AddModelError("Input.AgeElfique", "L'âge elfique est obligatoire.");
            }
            else if (Input.Peuple == "Hobbit")
            {
                if (Input.VillageOrigine == null)
                    ModelState.AddModelError("Input.VillageOrigine", "Le village est obligatoire.");
                if (Input.MetierHobbit == null)
                    ModelState.AddModelError("Input.MetierHobbit", "Le métier est obligatoire.");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
