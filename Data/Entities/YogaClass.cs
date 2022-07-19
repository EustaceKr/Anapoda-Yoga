using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class YogaClass
    {
        public string YogaClassId { get; set; }
        [ForeignKey("YogaClassType")]
        public string YogaClassTypeId { get; set; }
        public DateTime Date { get; set; }
        public YogaClassType YogaClassType { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
