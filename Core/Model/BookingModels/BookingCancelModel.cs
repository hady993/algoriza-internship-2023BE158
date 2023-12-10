using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.BookingModels
{
    public class BookingCancelModel
    {
        [Required]
        public string PatientId { get; set; }

        [Required]
        public int BookingId { get; set; }
    }
}
