using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Repositories
{
    public class FakeMovieRepository : IFakeMovieRepository
    {

        //private static FakeMovieRepository _instance = new FakeMovieRepository();
        //public static FakeMovieRepository Instance { get { return _instance; } }
        

        private IList<Movie> _movies;

        public FakeMovieRepository()
        {
            _movies = new List<Movie>
            {
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Name = "Inglorious bastards",
                    Genre = "Adventure",
                    Director = "Tarantino"
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Name = "Mother",
                    Genre = "Drama",
                    Director = "Aronofsky"
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Name = "Stalker",
                    Genre = "Drama",
                    Director = "Tarkovsky"
                }
            };
        }

        public Movie Add(Movie movie)
        {
            movie.Id = Guid.NewGuid();
            _movies.Add(movie);
            return movie;
        }

        public Movie Update(Movie movie)
        {
            var movieToUpdate = _movies.Where<Movie>(m => m.Id == movie.Id).FirstOrDefault();
            if (movieToUpdate != null)
            {
                var objFields = movie.GetType().GetProperties();

                foreach (var field in objFields)
                {
                    field.SetValue(movieToUpdate, field.GetValue(movie));
                }

            }
            return movieToUpdate;

        }

        public IList<Movie> GetAll(string name, string director)
        {
            IList<Movie> filteredMovies = _movies.Where<Movie>(m => m.Name == name || name == null).ToList();
            filteredMovies = filteredMovies.Where<Movie>(m => m.Director == director || director == null).ToList();

            return filteredMovies;
        }

        public Movie GetById(Guid Id)
        {
            var movie = _movies.Where<Movie>(m => m.Id == Id).FirstOrDefault();
            return movie;
        }

        public bool Delete(Guid id)
        {
            var movieToDelete = _movies.Where<Movie>(m => m.Id == id).FirstOrDefault();
            if (movieToDelete != null)
            {
                _movies.Remove(movieToDelete);
                return true;
            }
            else return false;
        }
    }
}
