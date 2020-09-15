using MvcMovieStore.DataAccessLayer;
using System.Collections.Generic;

namespace MvcMovieStore.Models.ViewModels
{
    public class CheckoutModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
        public string Province { get; set; }

        public string PostalCode { get; set; }
        public string Country { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }


    }
}