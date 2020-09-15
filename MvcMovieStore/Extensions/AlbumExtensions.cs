using MvcMovieStore.DataAccessLayer;
using MvcMovieStore.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MvcMovieStore.Extensions
{
    public static class AlbumExtensions
    {
        public static IEnumerable<Album> GetTopSellingAlbums(this GenericRepository<Album> repository,
            int count)
        {
            return repository.Get()
                .OrderByDescending(r => r.OrderDetails.Count)
                .Take(count)
                .ToList();
        }

        public static IEnumerable<Album> GetAlbumsByGenreId(this GenericRepository<Album> repository,
            int genreId)
        {
            return repository.Get(r => r.GenreId == genreId)
                .ToList();
        }
    }
}