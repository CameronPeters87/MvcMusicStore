using System.ComponentModel.DataAnnotations;

namespace MvcMovieStore.Models.ViewModels
{
    public class ShippingDetails
    {
        public int OrderId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string Province { get; set; }
        [Required]

        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }
    }
}