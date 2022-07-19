using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Reservation
    {
        public string ReservationId { get; set; }
        [ForeignKey("YogaClass")]
        public string YogaClassId { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public YogaClass YogaClass { get; set; }
        public Customer Customer { get; set; }
    }
}
