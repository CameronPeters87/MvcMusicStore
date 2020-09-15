using MvcMovieStore.Models.ViewModels;
using MvcMovieStore.Repositories;
using System.Web.Mvc;

namespace MvcMovieStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCartRepository.GetCart(this.HttpContext);
            var model = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(model);
        }

        [HttpPost]
        public void AddToCart(int id, int qty)
        {
            var cart = ShoppingCartRepository.GetCart(this.HttpContext);
            cart.AddToCart(unitOfWork.AlbumRepository.GetByID(id), qty);

            TempData["AddToCart"] = string.Format("You added {0} of {1} to your cart", qty, unitOfWork.AlbumRepository.GetByID(id).Title);
        }

        public ActionResult CartSummary()
        {
            var cart = ShoppingCartRepository.GetCart(this.HttpContext);

            ViewBag.CartItems = cart.GetNumberOfCartItems();

            return PartialView("_CartSummary");
        }
    }
}