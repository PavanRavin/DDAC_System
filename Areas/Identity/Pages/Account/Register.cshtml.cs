using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using DDAC_System.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DDAC_System.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<DDAC_SystemUser> _signInManager;
        private readonly UserManager<DDAC_SystemUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SelectList RoleSelectList = new SelectList(new List<SelectListItem>
        { 
            //new SelectListItem { Selected = true, Text = "Select Role", Value = "" }, 
            new SelectListItem { Selected = true, Text = "Teacher", Value = "Teacher" },
            new SelectListItem { Selected = true, Text = "Student", Value = "Student" },
        }, "Value", "Text", 1);

        public RegisterModel(
            UserManager<DDAC_SystemUser> userManager,
            SignInManager<DDAC_SystemUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> rolemanager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = rolemanager;
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

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "You must enter the Full Name!")]
            [StringLength(256, ErrorMessage = "You must enter the value between (6) - (256) characters", MinimumLength = 6)]
            [Display(Name = "Full Name(Username)")] //label
            public string userFullname { get; set; }

            [Required(ErrorMessage = "You must enter the Date of Birth")]
            [Display(Name = "Date Of Birth")]
            [DataType(DataType.Date)]
            public DateTime userDoB { get; set; }

            [Required(ErrorMessage = "Select a Role")]
            [Display(Name = "Role")]
            public string userRole { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

                bool roleExist = await _roleManager.RoleExistsAsync("Student");
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Student"));
                }

                roleExist = await _roleManager.RoleExistsAsync("Teacher");
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Teacher"));
                }

                var user = new DDAC_SystemUser{
                    Email = Input.Email,
                    UserFullname = Input.userFullname,
                    UserName = Input.userFullname,
                    UserDOB = Input.userDoB,
                    UserRole = Input.userRole,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, Input.userRole);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount){
                            
                        return RedirectToPage("Index");
                    }
                    else{
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors){
                    
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
