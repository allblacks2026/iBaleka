using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace iBalekaService.Domain.Models
{
    public class User
    {
        public User(string name,string surname,string country,DateTime dob)
        {
            Name = name;
            Surname = surname;
            Country = country;
            DateOfBirth = dob;
            Deleted = false;
            DateJoined = DateTime.Now.Date;
        }
        [Key]
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
        public bool Deleted { get; set; }
        //navigational properties
        public virtual ICollection<Club> Clubs { get; set; }

    }
}
