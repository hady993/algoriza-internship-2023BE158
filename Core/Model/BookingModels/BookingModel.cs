using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.BookingModels
{
    public class BookingModel
    {
        [Required]
        public string PatientId { get; set; }

        [Required]
        public int TimeId { get; set; }

        public string? DiscountCodeCoupon { get; set; }
    }
}
