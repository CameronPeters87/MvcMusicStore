using MvcMovieStore.Extensions;
using MvcMovieStore.Models.ViewModels;
using MvcMovieStore.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace MvcMovieStore.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            var model = unitOfWork.AlbumRepository.GetTopSellingAlbums(4);
            return View(model);
        }

        public ActionResult GenrePartial()
        {
            var model = (from g in unitOfWork.GenreRepository.Get()
                         select new GenrePartial
                         {
                             Name = g.Name,
                             Link = "/store/browse?genre=" + g.Name
                         }).ToList();

            return PartialView("_GenresDropdownPartial", model);
        }
    }
}