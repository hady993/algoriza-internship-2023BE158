using Core.Domain.DomainUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Booking : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TimeId { get; set; }
        public Time Time { get; set; }
        public int? DiscountCodeId { get; set; }
        public DiscountCode DiscountCode { get; set; }
        public double Price { get; set; }
        public double FinalPrice { get; set; }
        public BookingStatus Status { get; set; }
    }
}
