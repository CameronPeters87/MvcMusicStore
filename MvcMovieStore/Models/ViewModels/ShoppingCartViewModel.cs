using MvcMovieStore.DataAccessLayer;
using System.Collections.Generic;

namespace MvcMovieStore.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}