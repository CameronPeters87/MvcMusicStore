using MvcMovieStore.DataAccessLayer;
using System;
using System.Collections.Generic;

namespace MvcMovieStore.Interfaces
{
    public interface IGenreRepository : IDisposable
    {
        IEnumerable<Genre> GetGenres(); // List
        Genre GetGenreByID(int genreId); // Get one
        void InsertGenre(Genre genre); // Create
        void DeleteGenre(int genreId); // Delete
        void UpdateGenre(Genre genre); // Update
        void Save();
    }
}
