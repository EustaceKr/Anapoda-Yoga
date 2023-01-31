using Data.Entities;
using Data.Repositories.CustomerDAO;
using Data.Repositories.ReservetionDAO;
using Data.Repositories.YogaClassDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ReservationImplementation
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        private readonly IYogaClassRepository _yogaClassRepository;
        private readonly ICustomerRepository _customerRepository;
        public ReservationService(IReservationRepository repository, IYogaClassRepository yogaClassRepository, ICustomerRepository customerRepository)
        {
            _repository = repository;
            _yogaClassRepository = yogaClassRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUser(string userId)
        {
            return await _repository.GetAllReservationsByUserAsync(userId);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByClassAsync(string yogaClassId)
        {
            return await _repository.GetReservationsFromYogaClass(yogaClassId);
        }

        public async Task<Reservation> GetReservationByClassAndCustomer(string customerId, string yogaClassId)
        {
            return await _repository.GetReservationByClassAndCustomer(customerId, yogaClassId);
        }

        public void CreateReservation(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));
            
            if(reservation.Customer.TimesPerMonth > 0)
            {
                reservation.Customer.TimesPerMonth -= 1;
                _customerRepository.Update(reservation.Customer);
                reservation.ReservationId = Guid.NewGuid().ToString();
                _repository.Create(reservation);
            }
            else
            {
                throw new InvalidOperationException("The customer has no more reservations left for this month.");
            }
            
        }

        public async Task<bool> CheckReservationExists(Reservation reservation, Customer user)
        {
            var userReservations = await _repository.GetAllReservationsByUserAsync(user.Id);
            List<string> userIds = new List<string>();
            foreach(Reservation res in userReservations)
            {
                userIds.Add(res.CustomerId);
            }
            var yogaClass = await _yogaClassRepository.GetYogaClassByIdAsync(reservation.YogaClassId);
            var reservationOfYogaClass = await _repository.GetReservationsFromYogaClass(yogaClass.YogaClassId);
            foreach (Reservation res in reservationOfYogaClass)
            {
                if (userIds.Contains(res.CustomerId)) return false;
            }
            return true;
        }
        
        public async Task<bool> CheckCapacity(YogaClass yogaClass)
        {
            if(yogaClass.Reservations.Count >= yogaClass.YogaClassType.Capacity ) return false;
            return true;
        }

        public void DeleteReservation(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));
            reservation.Customer.TimesPerMonth += 1;
            _customerRepository.Update(reservation.Customer);
            _repository.Delete(reservation);
        }

        public async Task<bool> Complete()
        {
            try
            {
                return await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
