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
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await FindAll().OrderBy(c => c.CreatedAt).ToListAsync();
        }
        public async Task<Reservation> GetReservationByIdAsync(string reservationId)
        {
            return await FindByCondition(reservation => reservation.ReservationId.Equals(reservationId)).FirstOrDefaultAsync();
        }
    }
}
