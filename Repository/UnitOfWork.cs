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
        public IGeneralRepository<Doctor> DoctorRepository { get; }

        public UnitOfWork(ApplicationDbContext applicationDbContext, IGeneralRepository<Doctor> doctorRepository)
        {
            _context = applicationDbContext;
            DoctorRepository = doctorRepository;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
