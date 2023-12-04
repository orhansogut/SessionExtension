using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SessionExtension.Models
{
    public class User
    { 
        public required string Id { get; set; }
      
        public required string Username { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

    }

}
