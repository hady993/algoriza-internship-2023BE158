using Core.Domain;
using Core.Model.AppointmentModels;
using Core.Repository;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAppointment(AddDoctorSettingsModel model)
        {
            // Update doctor's price!
            var doctor = await _unitOfWork.DoctorRepository.GetEntityByIdAsync(model.DoctorId);

            if (doctor == null)
            {
                return false;
            }

            doctor.Price = model.Price;
            var priceResult = await _unitOfWork.DoctorRepository.EditEntityAsync(doctor, model.DoctorId);

            if (!priceResult)
            {
                return false;
            }

            // Add doctor's days and times!
            List<Time> times = new List<Time>();

            foreach(var dayModel in model.Days)
            {
                // To add the day!
                var appointment = new Appointment
                {
                    DoctorId = model.DoctorId,
                    Day = dayModel.Day
                };

                await _unitOfWork.AppointmentRepository.AddEntityAsync(appointment);
                _unitOfWork.Complete();

                // To add the time!
                var tempAppointment = (await _unitOfWork.AppointmentRepository.GetAllAsync()).LastOrDefault();

                if (tempAppointment == null)
                {
                    return false;
                }

                var appointmentId = tempAppointment.Id;

                foreach(var timeModel in dayModel.Times)
                {
                    var time = new Time
                    {
                        AppointmentId = appointmentId,
                        DocTime = TimeOnly.ParseExact(timeModel, "hh:mm tt", CultureInfo.InvariantCulture)
                    };

                    times.Add(time);
                }
            }

            // To add all times in the database!
            if (times.Count == 0)
            {
                return false;
            }

            await _unitOfWork.TimeRepository.AddEntitiesAsync(times);
            _unitOfWork.Complete();

            return true;
        }


    }
}
