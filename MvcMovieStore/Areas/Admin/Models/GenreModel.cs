using MvcMovieStore.DataAccessLayer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovieStore.Areas.Admin.Models
{
    public class GenreModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Album> Albums { get; set; }
    }
}