using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.DTOs
{
    public class DoctorPatientDto : BaseUserDto
    {
        public string SpetializationAr { get; set; }
        public string SpetializationEn { get; set; }
        public double Price { get; set; }
        public List<DayDto> Appointments { get; set; }
    }
}
