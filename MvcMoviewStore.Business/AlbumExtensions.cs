using MvcMovieStore.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMoviewStore.Business
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
    }
}
