using MvcMovieStore.Areas.Admin.Models;
using MvcMovieStore.DataAccessLayer;
using MvcMovieStore.Extensions;
using MvcMovieStore.Interfaces;
using MvcMovieStore.Models;
using MvcMovieStore.Repositories;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcMovieStore.Areas.Admin.Controllers
{
    public class GenresController : Controller
    {

        private readonly IGenreRepository genreRepository;
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public GenresController()
        {
            this.genreRepository = new GenreRepository(new ApplicationDbContext());
        }

        // GET: Admin/Genre
        public ActionResult Index()
        {
            var model = new GenreModel
            {
                Genres = genreRepository.GetGenres()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(GenreModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = genreRepository.GetGenres();
                return View("Index", model);
            }

            genreRepository.InsertGenre(new Genre
            {
                Name = model.Name,
                Description = model.Description,
                Albums = new List<Album>()
            });

            genreRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public string Remove(int id)
        {
            genreRepository.DeleteGenre(id);
            genreRepository.Save();

            var albums = unitOfWork.AlbumRepository.GetAlbumsByGenreId(id);
            unitOfWork.AlbumRepository.DeleteRange(albums);
            unitOfWork.Save();

            return string.Empty;
        }

        protected override void Dispose(bool disposing)
        {
            genreRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}