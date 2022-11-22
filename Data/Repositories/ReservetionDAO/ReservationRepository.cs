using Data.Context;
using Data.Entities;
using Data.Repositories.GenericDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ReservetionDAO
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Reservation>> GetAllReservationsByUserAsync(string userId)
        {
            return await FindByCondition(reservation => reservation.CustomerId == userId).ToListAsync();
        }
        public async Task<Reservation> GetReservationByIdAsync(string reservationId)
        {
            return await FindByCondition(reservation => reservation.ReservationId.Equals(reservationId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsFromYogaClass(string yogaClassId)
        {
            return await FindByCondition(reservation => reservation.YogaClassId == yogaClassId).ToListAsync();
        }

        public async Task<Reservation> GetReservationByClassAndCustomer(string yogaClassId, string customerId)
        {
            return await FindByCondition(reservation => reservation.YogaClassId == yogaClassId).Where(reservation => reservation.CustomerId == customerId).FirstOrDefaultAsync();
        }
    }
}
