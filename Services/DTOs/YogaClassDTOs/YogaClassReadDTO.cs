using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.YogaClassDTOs
{
    public class YogaClassReadDTO
    {
        public string YogaClassId { get; set; }
        public string YogaClassTypeId { get; set; }
        public DateTime Date { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
