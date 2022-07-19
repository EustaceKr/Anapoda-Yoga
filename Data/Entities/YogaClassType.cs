using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class YogaClassType
    {
        public string YogaClassTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public TimeSpan Duration { get; set; }
        public List<YogaClass> YogaClasses { get; set;}
    }
}
