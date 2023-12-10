using Core.Domain.DomainUtil;
using Core.Model.BookingModels;
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
    public interface IPatientService
    {
        Task<IdentityResult> RegisterPatientAsync(UserRegisterModel model, string? imagePath);
        Task<IEnumerable<DoctorPatientDto>> GetAllDoctorsAsync(StringSearchModel searchModel);
        Task<bool> AddBookingAsync(BookingModel bookingModel);
        Task<bool> CancelBookingAsync(BookingCancelModel model);
    }
}
