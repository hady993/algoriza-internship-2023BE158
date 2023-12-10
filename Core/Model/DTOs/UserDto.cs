﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.DTOs
{
    public class UserDto : BaseUserDto
    {
        public DateOnly DateOfBirth { get; set; }
    }
}
