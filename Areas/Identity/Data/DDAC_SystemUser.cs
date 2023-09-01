using System;
using Microsoft.AspNetCore.Identity;

namespace DDAC_System.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the DDAC_SystemUser class
    public class DDAC_SystemUser : IdentityUser
    {
        [PersonalData]
        public string UserFullname { get; set; }
        [PersonalData]
        public DateTime UserDOB { get; set; }
        [PersonalData]
        public string UserRole { get; set; }
    }
}
