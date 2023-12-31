﻿using Core.Domain.DomainUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Doctor : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public double Price { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
