using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Customer : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public string? MobileNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Sex { get; set; }
        public string? Adress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<Reservation> Reservations { get; set; }

        public Customer(string firstName, string lastName)
        {
            Id = new Guid().ToString();
            FirstName = firstName;
            LastName = lastName;
            CreatedDate = DateTime.Now;
            SecurityStamp = Guid.NewGuid().ToString();
        }

        public Customer(string id, string email, string userName, string normalizedUserName, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            UserName = userName;
            NormalizedUserName = normalizedUserName;
            FirstName = firstName;
            LastName = lastName;
            CreatedDate = DateTime.Now;
        }
    }
}
