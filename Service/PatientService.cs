using Core.Domain;
using Core.Domain.DomainUtil;
using Core.Model.BookingModels;
using Core.Model.DTOs;
using Core.Model.SearchModels;
using Core.Model.UserModels;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PatientService : IPatientService
    {
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IIdentityService identityService, IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> RegisterPatientAsync(UserRegisterModel model, string? imagePath)
        {
            return await _identityService.RegisterUserAsync(model, AccountType.Patient, imagePath);
        }

        public async Task<IEnumerable<DoctorPatientDto>> GetAllDoctorsAsync(StringSearchModel searchModel)
        {
            // Retrieving all doctors from the database!
            var doctors = await _unitOfWork.DoctorRepository.GetAllAsync(includeProperties: "User,Specialization,Appointments");

            // Create new list of doctorDto to retrieve the data!
            List<DoctorPatientDto> result = new List<DoctorPatientDto>();

            foreach (var doctor in doctors)
            {
                // To get appointments and times!
                List<DayDto> dayDtos = new List<DayDto>();

                foreach (var appointment in doctor.Appointments)
                {
                    // To get list of times!
                    List<TimeDto> timeDtos = new List<TimeDto>();
                    var times = (await _unitOfWork.TimeRepository.GetAllAsync())
                        .Where(t => t.AppointmentId == appointment.Id);

                    foreach (var time in times)
                    {
                        var timeDto = new TimeDto
                        {
                            Id = time.Id,
                            Time = time.DocTime
                        };

                        timeDtos.Add(timeDto);
                    }

                    // Add the list of times to their days!
                    var dayDto = new DayDto
                    {
                        Day = appointment.Day.ToString(),
                        Times = timeDtos
                    };

                    dayDtos.Add(dayDto);
                }

                // Insert doctor's fundamental data and his/her appointments list!
                var doctorPatientDto = new DoctorPatientDto
                {
                    ProfileImage = doctor.User.ProfileImage,
                    FullName = doctor.User.FullName,
                    Email = doctor.User.Email,
                    Phone = doctor.User.PhoneNumber,
                    Gender = doctor.User.Gender.ToString(),
                    SpetializationAr = doctor.Specialization.NameAr,
                    SpetializationEn = doctor.Specialization.NameEn,
                    Price = doctor.Price,
                    Appointments = dayDtos
                };

                result.Add(doctorPatientDto);
            }

            // To filter doctors and support pagination!
            var itemsToSkip = (searchModel.PageNumber - 1) * searchModel.PageSize;

            var filteredResult = result
                .Where(d => d.FullName.Contains(searchModel.Search) || d.Email.Contains(searchModel.Search))
                .Skip(itemsToSkip)
                .Take(searchModel.PageSize);

            return filteredResult;
        }

        public async Task<bool> AddBookingAsync(BookingModel bookingModel)
        {
            // Check if the patient exists as a user!
            var patient = await _unitOfWork.IdentityRepository.FindUserByIdAsync(bookingModel.PatientId);
            
            if (patient == null)
            {
                return false;
            }

            if (patient.AccountType != AccountType.Patient)
            {
                return false;
            }

            // Check if you have the provided time and not booked!
            var time = await _unitOfWork.TimeRepository.GetEntityByIdAsync(bookingModel.TimeId, includeProperties: "Booking,Appointment");
            
            if (time == null)
            {
                return false;
            }

            if (time.Booking != null)
            {
                if (time.Booking.Status != BookingStatus.Cancelled)
                {
                    return false;
                }
            }

            // Check if you booked this doctor before!
            var oldBookings = (await _unitOfWork.BookingRepository.GetAllAsync(includeProperties: "Time"))
                .Where(b => b.UserId == bookingModel.PatientId);

            foreach (var oldBooking in  oldBookings)
            {
                var oldAppointment = await _unitOfWork.AppointmentRepository.GetEntityByIdAsync(oldBooking.Time.AppointmentId);

                if (oldAppointment != null && 
                    oldAppointment.DoctorId == time.Appointment.DoctorId && 
                    oldBooking.Status == BookingStatus.Pending)
                {
                    return false;
                }
            }

            // Check if you have an active discount code coupon!
            var code = bookingModel.DiscountCodeCoupon;
            var coupons = await _unitOfWork.DiscountCodeRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(code))
            {
                if (coupons.All(c => c.Code != code))
                {
                    return false;
                }

                if (coupons.Where(c => c.Code == code).All(c => !c.IsActive))
                {
                    return false;
                }

                // To check if the coupon is open for the patient of not!
                var numOfCompletedBookings = oldBookings.Where(b => b.Status == BookingStatus.Completed).Count();
                var numOfAvailability = coupons.FirstOrDefault(c =>  c.Code == code)?.RequestsCount;

                if (numOfAvailability > numOfCompletedBookings)
                {
                    return false;
                }
            }

            // Get doctor's price!
            var price = (await _unitOfWork.DoctorRepository.GetEntityByIdAsync(time.Appointment.DoctorId)).Price;

            // Get discount coupon!
            var coupon = coupons.FirstOrDefault(c => c.Code == code);

            // Calculate the final price!
            var finalPrice = CalculateFinalPrice(coupon, price);

            // Adding the booking!
            var booking = new Booking
            {
                UserId = bookingModel.PatientId,
                TimeId = bookingModel.TimeId,
                Price = price,
                FinalPrice = finalPrice,
                Status = BookingStatus.Pending
            };

            if (coupon != null)
            {
                booking.DiscountCodeId = coupon.Id;
            }

            await _unitOfWork.BookingRepository.AddEntityAsync(booking);
            _unitOfWork.Complete();

            return true;
        }

        public async Task<bool> CancelBookingAsync(BookingCancelModel model)
        {
            // Check if the patient have this booking!
            var booking = await _unitOfWork.BookingRepository.GetEntityByIdAsync(model.BookingId);

            if (booking == null)
            {
                return false;
            }

            if (booking.UserId != model.PatientId)
            {
                return false;
            }

            // Cancel the booking!
            booking.Status = BookingStatus.Cancelled;

            var result = await _unitOfWork.BookingRepository.EditEntityAsync(booking, model.BookingId);
            _unitOfWork.Complete();

            return result;
        }

        // Helper method to calculate the final price after discount!
        private double CalculateFinalPrice(DiscountCode? coupon, double price)
        {
            if (coupon == null)
            {
                return price;
            }

            if (coupon.DiscountType == DiscountType.Percentage)
            {
                return price - ((price * coupon.Value) / 100.0);
            }

            return price - coupon.Value;
        }
    }
}
