using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.DiscountCodeModels
{
    public class UpdateDiscountModel : AddDiscountModel
    {
        [Required]
        public int Id { get; set; }
    }
}
