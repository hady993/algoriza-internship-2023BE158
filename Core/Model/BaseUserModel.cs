using Core.Domain.DomainUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class BaseUserModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        //[JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }
    }
}
