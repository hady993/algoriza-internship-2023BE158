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
        IIdentityRepository IdentityRepository { get; }
        IGeneralRepository<Doctor> DoctorRepository { get; }
        IGeneralRepository<Booking> BookingRepository { get; }
        IGeneralRepository<DiscountCode> DiscountCodeRepository { get; }
        int Complete();
    }
}
