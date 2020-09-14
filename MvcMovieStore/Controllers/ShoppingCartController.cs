using MvcMovieStore.Repositories;
using System.Web.Mvc;

namespace MvcMovieStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public ShoppingCartController()
        {

        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void AddToCart(int id)
        {
            var cart = ShoppingCartRepository.GetCart(this.HttpContext);
            cart.AddToCart(unitOfWork.AlbumRepository.GetByID(id));
        }
    }
}