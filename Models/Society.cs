using System.ComponentModel.DataAnnotations;

namespace Society_Management_System.Models
{
    public class Society
    {
        [Key]
        public int SocietyId { get; set; }

        [Required]
        public string SocietyName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PinCode { get; set; }

        [Required]
        public int TotalHouses { get; set; }

        [Required]
        public string Address { get; set; }

        public string Image { get; set; }
    }
}