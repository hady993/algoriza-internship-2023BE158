using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.SearchModels
{
    public class BasePaginationSearchModel
    {
        [Required]
        public int PageNumber { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}
