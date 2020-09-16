using System.Web.Mvc;

namespace MvcMovieStore.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewOrder(int id)
        {

            return View();
        }
    }
}