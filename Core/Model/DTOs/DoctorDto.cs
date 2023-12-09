using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.DTOs
{
    public class DoctorDto : UserDto
    {
        public string SpetializationAr { get; set; }
        public string SpetializationEn { get; set; }
    }
}
