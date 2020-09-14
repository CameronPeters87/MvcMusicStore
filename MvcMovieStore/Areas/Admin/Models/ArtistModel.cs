using MvcMovieStore.DataAccessLayer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovieStore.Areas.Admin.Models
{
    public class ArtistModel
    {
        [Required]
        public string Name { get; set; }

        public IEnumerable<Artist> Artists { get; set; }
    }
}