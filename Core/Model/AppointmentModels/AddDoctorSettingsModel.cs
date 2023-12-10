using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.AppointmentModels
{
    public class AddDoctorSettingsModel
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        [Range(25.0, 500.0)]
        public double Price { get; set; }

        [Required]
        public List<DayModel> Days { get; set; }
    }
}
