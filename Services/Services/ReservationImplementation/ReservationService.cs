using Data.Entities;
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
        public ReservationService(IReservationRepository repository, IYogaClassRepository yogaClassRepository)
        {
            _repository = repository;
            _yogaClassRepository = yogaClassRepository;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUser(string userId)
        {
            return await _repository.GetAllReservationsByUserAsync(userId);
        }

        public async Task<Reservation> GetReservationByClassAndCustomer(string customerId, string yogaClassId)
        {
            return await _repository.GetReservationByClassAndCustomer(customerId, yogaClassId);
        }

        public void CreateReservation(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));
            reservation.ReservationId = Guid.NewGuid().ToString();
            _repository.Create(reservation);
        }

        public async Task<bool> CheckReservation(Reservation reservation, Customer user)
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
        public void DeleteReservation(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));
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
