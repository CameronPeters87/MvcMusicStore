using MvcMovieStore.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace MvcMovieStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        // GET: /Store/
        public ActionResult Index()
        {
            var model = unitOfWork.AlbumRepository.Get();
            return View(model);
        }
        //
        // GET: /Store/Browse
        public ActionResult Browse(string genre)
        {
            var model = unitOfWork.AlbumRepository.Get(a => a.Genre.Name == genre).ToList();

            ViewBag.Genre = genre;

            return View(model);
        }

        // GET: /Store/Details
        public ActionResult Details(int id)
        {
            var model = unitOfWork.AlbumRepository.GetByID(id);

            ViewBag.SimilarAlbums = unitOfWork.AlbumRepository.Get(a => a.Genre.Name == model.Genre.Name).ToList();

            return View(model);
        }
    }
}