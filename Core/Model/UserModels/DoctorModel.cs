using Core.Domain.DomainUtil;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.UserModels
{
    public class DoctorModel : BaseUserModel
    {
        [Required]
        public IFormFile ProfileImage { get; set; }

        [Required]
        public SpecializationType SpecializationType { get; set; }
    }
}
