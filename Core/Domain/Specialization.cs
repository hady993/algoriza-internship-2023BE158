﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Specialization : BaseEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
