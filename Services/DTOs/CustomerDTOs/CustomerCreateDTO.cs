using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CustomerDTOs
{
    public class CustomerCreateDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? MobileNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Sex { get; set; }
        public string? Adress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public DateTime? PayDate { get; set; }
        public int? TimesPerMonth { get; set; }
        public string Username { get; set; }
        public string Password { get;set; }
    }
}
