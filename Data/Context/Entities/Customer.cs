using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Customer : IdentityUser
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
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<Reservation> Reservations { get; set; }
        //[Timestamp]
        //public byte[] TimeStamp { get; set; }

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
        public Customer(string? firstName,string? lastName,string? phone,string? mobileNumber, DateTime? dateOfBirth,string? sex, string? adress, string? city, string? state
                       ,string? postalCode, DateTime? payDate, int? timesPerMonth)   
        {
            Id = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            MobileNumber = mobileNumber;
            DateOfBirth = dateOfBirth;
            Sex = sex;
            Adress = adress;
            City = city;
            State = state;
            PostalCode = postalCode;
            PayDate = payDate;
            TimesPerMonth = timesPerMonth;
            CreatedDate = DateTime.Now;
        }
    }
}
