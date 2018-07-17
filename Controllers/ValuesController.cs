using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Repositories;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        public  IFakeMovieRepository repository;
        public MovieController(IFakeMovieRepository fakeMovieRepository)
        {
            repository = fakeMovieRepository;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<Movie> Get([FromQuery] string name, [FromQuery] string director)
        {
            return repository.GetAll(name, director);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Movie Get(Guid id)
        {
            return repository.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public Movie Post([FromBody]Movie value)
        {
            return repository.Add(value);
        }

        // PUT api/values/5
        [HttpPut]
        public Movie Put([FromBody]Movie value)
        {
            return repository.Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            return repository.Delete(id);
        }
    }
}
