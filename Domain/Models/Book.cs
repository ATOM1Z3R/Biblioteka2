using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(100, ErrorMessage = "Nazwa jest za długa")]
        [DisplayName("Nazwa książki")]
        public string Name { get; set; }

        [DisplayName("Autor")]
        public Author Author { get; set; }

        [Required]
        [DisplayName("Autor")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DisplayName("Data wydania")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(1024, ErrorMessage = "Opis jest za długi")]
        [DisplayName("Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [DisplayName("Sztuk")]
        public int Quantity { get; set; }

        public virtual IEnumerable<Reservation> Reservation { get; set; }

        public string ReturnReleaseDate { get { return ReleaseDate.ToShortDateString(); } }
    }
}
