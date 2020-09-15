using MvcMovieStore.Models.ViewModels;
using MvcMovieStore.Repositories;
using System.Web.Mvc;

namespace MvcMovieStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Checkout
        public ActionResult Index()
        {
            var cart = ShoppingCartRepository.GetCart(this.HttpContext);
            var model = new CheckoutModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(model);
        }
    }
}