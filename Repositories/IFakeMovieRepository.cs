using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Repositories
{
    public interface IFakeMovieRepository
    {
        Movie Add(Movie movie);
        Movie Update(Movie movie);
        IList<Movie> GetAll(string name, string director);
        Movie GetById(Guid id);
        bool Delete(Guid id);
        
    }
}
