using MvcMovieStore.Interfaces;
using MvcMovieStore.Models.ViewModels;
using MvcMovieStore.Repositories;
using System.Configuration;
using System.Web.Mvc;

namespace MvcMovieStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        private IPayment _payment = new Payment();
        private IOrderRepository order = new OrderRepository();

        // Get Paygate keys from webconfig file 
        readonly string PayGateID = ConfigurationManager.AppSettings["PAYGATEID"];
        readonly string PayGateKey = ConfigurationManager.AppSettings["PAYGATEKEY"];

        // GET: Checkout
        public ActionResult Index()
        {
            var cart = ShoppingCartRepository.GetCart(this.HttpContext);

            order.InitializeOrder(cart, User.Identity, this);

            var model = new CheckoutModel
            {
                OrderId = order.GetOrderId(this.HttpContext),
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(model);
        }
    }
}