using Core.Domain.DomainUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class DiscountCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int RequestsCount { get; set; }
        public DiscountType DiscountType { get; set; }
        public double Value { get; set; }
        public bool IsActive { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
