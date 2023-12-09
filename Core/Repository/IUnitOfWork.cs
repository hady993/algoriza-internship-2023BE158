using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IUnitOfWork
    {
        IGeneralRepository<Doctor> DoctorRepository { get; }
        IGeneralRepository<Booking> BookingRepository { get; }
        int Complete();
    }
}
