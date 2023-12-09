﻿using Core.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IAdminService
    {
        Task<IdentityResult> AddDoctorAsync(DoctorModel model);
        Task<IdentityResult> EditDoctorAsync(DoctorUpdateModel model);
    }
}