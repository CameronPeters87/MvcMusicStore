using MvcMovieStore.Areas.Admin.Models;
using MvcMovieStore.DataAccessLayer;
using MvcMovieStore.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MvcMovieStore.Areas.Admin.Controllers
{
    public class AlbumController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Admin/Album
        public ActionResult Index()
        {
            var model = new AlbumModel
            {
                ArtistDropdown = new SelectList(unitOfWork.ArtistRepository.Get(),
                    "Id", "Name"),
                GenreDropdown = new SelectList(unitOfWork.GenreRepository.Get().ToList(),
                    "Id", "Name"),
                Albums = unitOfWork.AlbumRepository.Get().ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AlbumModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ArtistDropdown = new SelectList(unitOfWork.ArtistRepository.Get().ToList(),
                    "Id", "Name");
                model.GenreDropdown = new SelectList(unitOfWork.GenreRepository.Get().ToList(),
                    "Id", "Name");
                model.Albums = unitOfWork.AlbumRepository.Get().ToList();
                return View("Index", model);
            }

            #region Upload Album Art

            string albumArtUrl;

            if (model.AlbumArtFile.ContentLength > 0)
            {
                string filename = model.AlbumArtFile.FileName;
                string path = Server.MapPath("~/AlbumArt");
                string combined = Path.Combine(path, filename);
                model.AlbumArtFile.SaveAs(combined);

                albumArtUrl = "/AlbumArt/" + filename;
            }
            else
            {
                albumArtUrl = "/Content/Theme/Images/placeholder.gif";
            }
            #endregion


            unitOfWork.AlbumRepository.Insert(new DataAccessLayer.Album
            {
                AlbumArtUrl = albumArtUrl,
                Artist = unitOfWork.ArtistRepository.GetByID(model.ArtistId),
                ArtistId = model.ArtistId,
                Genre = unitOfWork.GenreRepository.GetByID(model.GenreId),
                GenreId = model.GenreId,
                OrderDetails = new List<OrderDetail>(),
                Price = model.Price,
                Title = model.Title
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