﻿using Core.Domain.DomainUtil;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class DoctorUpdateModel : SuperBaseUserModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public IFormFile ProfileImage { get; set; }

        [Required]
        public SpecializationType SpecializationType { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
