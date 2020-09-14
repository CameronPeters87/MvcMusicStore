using MvcMovieStore.Areas.Admin.Models;
using MvcMovieStore.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace MvcMovieStore.Areas.Admin.Controllers
{
    public class ArtistsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Admin/Artists
        public ActionResult Index()
        {
            var model = new ArtistModel
            {
                Artists = unitOfWork.ArtistRepository.Get().ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ArtistModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Artists = unitOfWork.ArtistRepository.Get().ToList();

                return View("Index", model);
            }

            unitOfWork.ArtistRepository.Insert(new DataAccessLayer.Artist
            {
                Name = model.Name
            });

            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}