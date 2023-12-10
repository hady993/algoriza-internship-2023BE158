﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.AppointmentModels
{
    public class DeleteDoctorTimeModel
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int TimeId { get; set; }
    }
}
