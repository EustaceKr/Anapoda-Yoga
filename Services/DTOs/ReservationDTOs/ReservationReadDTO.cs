using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ReservationDTOs
{
    public class ReservationReadDTO
    {
        public string ReservationId { get; set; }
        public string YogaClassId { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
