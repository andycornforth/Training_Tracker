using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainingTrackerAPI.Models
{
    public class ApiPerson
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        public ApiGender Gender { get; set; }
    }
}