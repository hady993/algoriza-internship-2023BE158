using Core.Domain;
using Core.Domain.DomainUtil;
using Core.Model.AppointmentModels;
using Core.Model.BookingModels;
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

        public async Task<bool> ConfirmCheckUpAsync(ConfirmCheckupModel model)
        {
            // Check if doctor's time have this booking!
            var booking = await _unitOfWork.BookingRepository.GetEntityByIdAsync(model.BookingId);

            if (booking == null)
            {
                return false;
            }

            if (booking.TimeId != model.TimeId || booking.Status == BookingStatus.Cancelled)
            {
                return false;
            }

            // Confirm checkup!
            booking.Status = BookingStatus.Completed;

            var result = await _unitOfWork.BookingRepository.EditEntityAsync(booking, model.BookingId);
            _unitOfWork.Complete();

            return result;
        }

        public async Task<bool> AddAppointmentAsync(AddDoctorSettingsModel model)
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

        public async Task<bool> UpdateTimeAsync(UpdateDoctorSettingModel model)
        {
            // Ensure that doctor exists!
            var doctor = await _unitOfWork.DoctorRepository.GetEntityByIdAsync(model.DoctorId, includeProperties: "Appointments");

            if (doctor == null)
            {
                return false;
            }

            // Ensure that doctor has the time with a certain id!
            var time = await _unitOfWork.TimeRepository.GetEntityByIdAsync(model.TimeId, includeProperties: "Appointment,Booking");

            if (time == null)
            {
                return false;
            }

            if (time.Appointment.DoctorId != model.DoctorId)
            {
                return false;
            }

            // Ensure that the time is not booked with Completed or Pending booking!
            var bookingStatus = time.Booking?.Status;

            if (bookingStatus == BookingStatus.Pending || bookingStatus == BookingStatus.Completed)
            {
                return false;
            }

            // Update doctor's time!
            time.DocTime = TimeOnly.ParseExact(model.NewTime, "hh:mm tt", CultureInfo.InvariantCulture);

            var result = await _unitOfWork.TimeRepository.EditEntityAsync(time, model.TimeId);
            _unitOfWork.Complete();

            return result;
        }

        public async Task<bool> DeleteTimeAsync(DeleteDoctorTimeModel model)
        {
            // Ensure that doctor exists!
            var doctor = await _unitOfWork.DoctorRepository.GetEntityByIdAsync(model.DoctorId, includeProperties: "Appointments");

            if (doctor == null)
            {
                return false;
            }

            // Ensure that doctor has the time with a certain id!
            var time = await _unitOfWork.TimeRepository.GetEntityByIdAsync(model.TimeId, includeProperties: "Appointment,Booking");

            if (time == null)
            {
                return false;
            }

            if (time.Appointment.DoctorId != model.DoctorId)
            {
                return false;
            }

            // Ensure that the time is not booked with Completed or Pending booking!
            var bookingStatus = time.Booking?.Status;

            if (bookingStatus == BookingStatus.Pending || bookingStatus == BookingStatus.Completed)
            {
                return false;
            }

            // Delete doctor's time!
            _unitOfWork.TimeRepository.DeleteEntity(time);
            _unitOfWork.Complete();

            return true;
        }
    }
}
