using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        [DisplayName("Imię")]
        public string FristName { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
