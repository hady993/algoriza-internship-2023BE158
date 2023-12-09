using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.SearchModels
{
    public class StringSearchModel : BasePaginationSearchModel
    {
        public string? Search { get; set; }
    }
}
