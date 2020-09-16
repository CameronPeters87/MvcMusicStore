using MvcMovieStore.Repositories;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace MvcMovieStore.Interfaces
{
    public interface IOrderRepository
    {
        void InitializeOrder(ShoppingCartRepository cart,
            IIdentity identity, Controller controller);
        int GetOrderId(HttpContextBase controller);
    }
}