using MvcMovieStore.Interfaces;
using MvcMovieStore.Models.ViewModels;
using MvcMovieStore.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace MvcMovieStore.Controllers
{
    public class CheckoutController : Controller
    {
        private IOrderRepository order = new OrderRepository();

        // GET: Checkout
        public ActionResult Index()
        {
            var cart = ShoppingCartRepository.GetCart(this.HttpContext);

            order.InitializeOrder(cart, User.Identity, this);

            var model = new CheckoutModel
            {
                OrderId = order.GetOrderId(this.HttpContext),
                OrderDetails = order.GetOrderDetails(order.GetOrderId(this.HttpContext))
            };

            model.CartTotal = model.OrderDetails.Sum(o => o.UnitPrice * o.Quantity);

            return View(model);
        }

        [HttpPost]
        public string UpdateOrder(ShippingDetails model)
        {
            if (!ModelState.IsValid)
            {
                return "Failed";
            }
            order.UpdateOrder(order.GetOrderId(this.HttpContext), model);

            return "Success";
        }
    }
}