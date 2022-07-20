using Data.Entities;
using Data.Repositories.GenericDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ReservetionDAO
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetAllReservationsByUserAsync(string userId);
        Task<Reservation> GetReservationByIdAsync(string customerId);
        Task<IEnumerable<Reservation>> GetReservationsFromYogaClass(string yogaClassId);
    }
}
