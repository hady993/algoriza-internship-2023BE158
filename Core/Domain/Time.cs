using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Time : BaseEntity
    {
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public TimeOnly DocTime { get; set; }
        public Booking Booking { get; set; }
    }
}
