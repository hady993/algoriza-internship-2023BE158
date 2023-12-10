using Core.Domain;
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
        public IIdentityRepository IdentityRepository { get; }
        public IGeneralRepository<Doctor> DoctorRepository { get; }
        public IGeneralRepository<Booking> BookingRepository { get; }
        public IGeneralRepository<DiscountCode> DiscountCodeRepository { get; }
        public IGeneralRepository<Appointment> AppointmentRepository { get; }
        public IGeneralRepository<Time> TimeRepository { get; }

        public UnitOfWork(
            ApplicationDbContext applicationDbContext,
            IIdentityRepository identityRepository,
            IGeneralRepository<Doctor> doctorRepository,
            IGeneralRepository<Booking> bookingRepository,
            IGeneralRepository<DiscountCode> discountCodeRepository,
            IGeneralRepository<Appointment> appointmentRepository,
            IGeneralRepository<Time> timeRepository)
        {
            _context = applicationDbContext;
            IdentityRepository = identityRepository;
            DoctorRepository = doctorRepository;
            BookingRepository = bookingRepository;
            DiscountCodeRepository = discountCodeRepository;
            AppointmentRepository = appointmentRepository;
            TimeRepository = timeRepository;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
