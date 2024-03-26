using Microsoft.AspNetCore.Identity;

namespace InstitueProject.Authentication_Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Adress { get; set; }
    }
}
