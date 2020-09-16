using Microsoft.AspNet.Identity;
using MvcMovieStore.DataAccessLayer;
using MvcMovieStore.Extensions;
using MvcMovieStore.Interfaces;
using MvcMovieStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<OrderDetail> GetOrderDetails(int orderId)
        {
            return unitOfWork.OrderDetailRepository.Get(o => o.OrderId == orderId).ToList();
        }

        public void UpdateOrder(int orderId, ShippingDetails model)
        {
            var order = unitOfWork.OrderRepository.GetByID(orderId);

            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.Address = model.Address;
            order.City = model.City;
            order.Country = model.Country;
            order.Province = model.Province;
            order.Email = model.Email;
            order.Phone = model.Phone;
            order.PostalCode = model.PostalCode;

            unitOfWork.OrderRepository.Update(order);

            unitOfWork.Save();
        }
    }
}