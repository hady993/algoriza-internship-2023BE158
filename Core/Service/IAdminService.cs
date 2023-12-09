using Core.Model.DTOs;
using Core.Model.SearchModels;
using Core.Model.UserModels;
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
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync(StringSearchModel searchModel);
        Task<DoctorDto> GetDoctorByIdAsync(int id);
        Task<IdentityResult> AddDoctorAsync(DoctorModel model, string? imagePath);
        Task<IdentityResult> EditDoctorAsync(DoctorUpdateModel model, string? imagePath);
        Task<IdentityResult> DeleteDoctorAsync(int id);
        Task<IEnumerable<UserDto>> GetAllPatientsAsync(StringSearchModel searchModel);
    }
}
