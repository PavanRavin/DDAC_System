using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DDAC_System.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DDAC_System.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<DDAC_SystemUser> _userManager;
        private readonly SignInManager<DDAC_SystemUser> _signInManager;

        public IndexModel(
            UserManager<DDAC_SystemUser> userManager,
            SignInManager<DDAC_SystemUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "You must enter the name first before submitting your form!")]
            [StringLength(256, ErrorMessage = "You must enter the value between 6 - 256 chars", MinimumLength = 6)]
            [Display(Name = "Your Full Name")] //label
            public string userFullname { get; set; }

            [Required]
            [Display(Name = "Your DOB")]
            [DataType(DataType.Date)]
            public DateTime userDoB { get; set; }
        }

        private async Task LoadAsync(DDAC_SystemUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                userFullname = user.UserFullname,
                userDoB = user.UserDOB
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if (Input.userFullname != user.UserFullname)
            {
                user.UserFullname = Input.userFullname;
            }
            if (Input.userDoB != user.UserDOB)
            {
                user.UserDOB = Input.userDoB;
            }
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
