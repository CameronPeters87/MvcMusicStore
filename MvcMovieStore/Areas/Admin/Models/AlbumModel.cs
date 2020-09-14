using MvcMovieStore.DataAccessLayer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace MvcMovieStore.Areas.Admin.Models
{
    public class AlbumModel
    {
        public int GenreId { get; set; }
        public IEnumerable<SelectListItem> GenreDropdown { get; set; }

        public int ArtistId { get; set; }
        public IEnumerable<SelectListItem> ArtistDropdown { get; set; }
        [Required]
        public string Title { get; set; }

        public decimal Price { get; set; }

        public HttpPostedFileBase AlbumArtFile { get; set; }

        public IEnumerable<Album> Albums { get; set; }
    }
}