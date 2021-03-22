using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Author
    {
        [Key]
        [DisplayName("ID")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(100, ErrorMessage = "Imie zawiera zbyt wiele znaków")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Imie musi się składać tylko z liter")]
        [DisplayName("Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(100, ErrorMessage = "Nazwisko zawiera zbyt wiele znaków")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Nazwisko musi się składać tylko z liter")]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        public virtual IEnumerable<Book> Book { get; set; }

        public string FirstAndLastName { get { return FirstName + " " + LastName; } }
    }
}
