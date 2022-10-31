using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Mpilo1.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Zip code")]
        public int? ZipCode { get; set; }
        public string City { get; set; }

        
    }

    public enum Field
    {
        Id,
        Name,
        Surname,
        Email,
        PhoneNumber,
        ZipCode,
        City
    }
}
