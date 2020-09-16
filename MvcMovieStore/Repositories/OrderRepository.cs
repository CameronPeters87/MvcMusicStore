using Microsoft.AspNet.Identity;
using MvcMovieStore.Extensions;
using MvcMovieStore.Interfaces;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace MvcMovieStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public int GetOrderId(HttpContextBase context)
        {
            return Convert.ToInt32(context.Session["OrderId"]);
        }

        public void InitializeOrder(ShoppingCartRepository cart, IIdentity identity,
            Controller controller)
        {
            unitOfWork.OrderRepository.Insert(new DataAccessLayer.Order
            {
                Username = identity.IsAuthenticated ? identity.GetUserName() : cart.GetCartId(controller.HttpContext),
                Total = cart.GetTotal(),
                OrderDate = DateTime.Now,
                Email = identity.IsAuthenticated ? identity.GetUserName() : string.Empty
            });

            unitOfWork.Save();

            var lastOrder = unitOfWork.OrderRepository.GetLastOrder();

            cart.CreateOrderDetails(lastOrder);

            controller.Session["OrderId"] = lastOrder.Id;

        }
    }
}