using Core.Domain.DomainUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.DTOs
{
    public class BookingRequestDto
    {
        public string DoctorImage { get; set; }
        public string DoctorName { get; set; }
        public string SpetializationAr { get; set; }
        public string SpetializationEn { get; set; }
        public Day Day { get; set; }
        public TimeOnly Time { get; set; }
        public double Price { get; set; }
        public string? DiscountCode { get; set; }
        public double FinalPrice { get; set; }
        public BookingStatus Status { get; set; }
    }
}
