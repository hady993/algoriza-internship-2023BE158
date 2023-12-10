using Core.Domain.DomainUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.AppointmentModels
{
    public class DayModel
    {
        [Required]
        public Day Day { get; set; }

        [Required]
        public List<string> Times { get; set; }
    }
}
