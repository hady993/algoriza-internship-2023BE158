﻿using Core.Domain;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGeneralRepository<Doctor> DoctorRepository { get; }
        public IGeneralRepository<Booking> BookingRepository { get; }

        public UnitOfWork(
            ApplicationDbContext applicationDbContext,
            IGeneralRepository<Doctor> doctorRepository,
            IGeneralRepository<Booking> bookingRepository)
        {
            _context = applicationDbContext;
            DoctorRepository = doctorRepository;
            BookingRepository = bookingRepository;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
