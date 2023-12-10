using Core.Model.AppointmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IDoctorService
    {
        Task<bool> AddAppointmentAsync(AddDoctorSettingsModel model);
        Task<bool> UpdateTimeAsync(UpdateDoctorSettingModel model);
        Task<bool> DeleteTimeAsync(DeleteDoctorTimeModel model);
    }
}
