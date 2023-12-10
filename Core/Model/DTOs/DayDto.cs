using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.DTOs
{
    public class DayDto
    {
        public string Day { get; set; }
        public List<TimeDto> Times { get; set; }
    }
}
