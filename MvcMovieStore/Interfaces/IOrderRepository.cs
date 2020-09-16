using MvcMovieStore.DataAccessLayer;
using MvcMovieStore.Repositories;
using System.Collections.Generic;
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

        List<OrderDetail> GetOrderDetails(int orderId);
    }
}