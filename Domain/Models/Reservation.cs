using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Reservation
    {
        [Key]
        [DisplayName("ID")]
        public int ReservationId { get; set; }
        [DisplayName("Data rezerwacji")]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; } = DateTime.Now;

        [DisplayName("Data zwrotu")]
        [DataType(DataType.Date)]
        public DateTime RetriveDate { get; set; }

        public virtual Book Book { get; set; }
        [Required]
        public int BookId { get; set; }

        public virtual User User { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
