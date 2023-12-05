using Core.Model.ModelUtil;
using Microsoft.AspNetCore.Identity;

namespace Core.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? ProfileImage { get; set; }
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public AccountType AccountType { get; set; }
    }
}
