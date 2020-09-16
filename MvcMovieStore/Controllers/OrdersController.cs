using MvcMovieStore.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace MvcMovieStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("view-order")]
        public ActionResult ViewOrder(int id)
        {
            var order = unitOfWork.OrderRepository.GetByID(id);

            ViewBag.OrderDetails = unitOfWork.OrderDetailRepository.Get(details => details.OrderId == id).ToList();

            if (order == null)
            {
                order = new DataAccessLayer.Order();
            }

            return View("ViewOrder", order);
        }
    }
}