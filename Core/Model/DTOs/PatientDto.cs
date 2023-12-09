using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.DTOs
{
    public class PatientDto
    {
        public UserDto Details { get; set; }
        public IEnumerable<BookingRequestDto>? Requests { get; set; }
    }
}
