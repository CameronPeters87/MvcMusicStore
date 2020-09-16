using MvcMovieStore.DataAccessLayer;
using MvcMovieStore.Models;
using System;

namespace MvcMovieStore.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<Artist> artistRepository;
        private GenericRepository<Album> albumRepository;
        private GenericRepository<Genre> genreRepository;
        private GenericRepository<Order> orderRepository;
        private GenericRepository<OrderDetail> orderDetailRepository;

        public GenericRepository<OrderDetail> OrderDetailRepository
        {
            get
            {

                if (this.orderDetailRepository == null)
                {
                    this.orderDetailRepository = new GenericRepository<OrderDetail>(context);
                }
                return orderDetailRepository;
            }
        }
        public GenericRepository<Genre> GenreRepository
        {
            get
            {

                if (this.genreRepository == null)
                {
                    this.genreRepository = new GenericRepository<Genre>(context);
                }
                return genreRepository;
            }
        }
        public GenericRepository<Order> OrderRepository
        {
            get
            {

                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(context);
                }
                return orderRepository;
            }
        }


        public GenericRepository<Artist> ArtistRepository
        {
            get
            {

                if (this.artistRepository == null)
                {
                    this.artistRepository = new GenericRepository<Artist>(context);
                }
                return artistRepository;
            }
        }

        public GenericRepository<Album> AlbumRepository
        {
            get
            {
                if (this.albumRepository == null)
                {
                    this.albumRepository = new GenericRepository<Album>(context);
                }
                return albumRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}