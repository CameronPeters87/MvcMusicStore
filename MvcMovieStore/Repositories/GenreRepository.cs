using MvcMovieStore.DataAccessLayer;
using MvcMovieStore.Interfaces;
using MvcMovieStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MvcMovieStore.Repositories
{
    public class GenreRepository : IGenreRepository, IDisposable
    {
        private readonly ApplicationDbContext db;
        public GenreRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void DeleteGenre(int genreId) => db.Genres.Remove(db.Genres.Find(genreId));

        public Genre GetGenreByID(int genreId) => db.Genres.Find(genreId);

        public IEnumerable<Genre> GetGenres() => db.Genres.ToList();

        public void InsertGenre(Genre genre) => db.Genres.Add(genre);

        public void Save() => db.SaveChanges();

        public void UpdateGenre(Genre genre) => db.Entry(genre).State = EntityState.Modified;

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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