using MvcMovieStore.DataAccessLayer;
using MvcMovieStore.Repositories;
using System.Linq;

namespace MvcMovieStore.Extensions
{
    public static class OrderExtensions
    {
        public static Order GetLastOrder(this GenericRepository<Order> order)
        {
            return order.Get().OrderByDescending(o => o.Id).FirstOrDefault();
        }
    }
}