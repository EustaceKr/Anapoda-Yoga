using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ReservationImplementation
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetReservationsByUser(string userId);
        Task<Reservation> GetReservationByClassAndCustomer(string yogaClassId, string customerId);
        void CreateReservation(Reservation reservation);
        Task<bool> CheckReservation(Reservation reservation, Customer user);
        void DeleteReservation(Reservation reservation);
        Task<bool> Complete();
    }
}
