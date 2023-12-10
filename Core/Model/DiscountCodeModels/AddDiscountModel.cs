using Core.Domain.DomainUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.DiscountCodeModels
{
    public class AddDiscountModel
    {
        [Required]
        public string DiscountCode { get; set; }

        [Required]
        public int RequestsCount { get; set; }

        [Required]
        public DiscountType DiscountType { get; set; }

        [Required]
        public double Value { get; set; }
    }
}
