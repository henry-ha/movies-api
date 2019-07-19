using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Http;
using MoviesWeb.Repository;
using MoviesWeb.Models;
using System.Web.Http.Cors;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MoviesController : ControllerBase
    {
        public UnitOfWork work;

        public MoviesController()
        {
            work = new UnitOfWork();
        }
        
        // GET api/movies/{filter}/{searchtext}
        [HttpGet("{filter}/{searchtext}")]
        public ActionResult<string> Get(string filter, string searchtext)
        {
            //search movies by title, year of release, genre(s)
            IEnumerable<Movie> result = null;

            // 400 - invalid / no criteria given
            if (string.IsNullOrWhiteSpace(searchtext))
                return new NotFoundObjectResult(
                    new ErrorJsonResponse(
                        400,
                        ErrorJsonResponse.ErrorMessages.NotFound,
                        new
                        {
                            searchtext
                        }
                    )
                );

            if (string.IsNullOrWhiteSpace(filter))
            {
                var errorMsg = "Please select a filter";
                return new NotFoundObjectResult(
                    new ErrorJsonResponse(
                        400,
                        ErrorJsonResponse.ErrorMessages.NotFound,
                        new
                        {
                            errorMsg
                        }
                    )
                );
            }


            //get by searchtext
            switch (filter.ToLower())
            {
                case "title":
                    result = work.MovieRepository.Find(x => x.Title.ToLower() == searchtext.ToLower());
                    break;

                case "year":
                    result = work.MovieRepository.Find(x => x.YearOfRelease.ToString() == searchtext.ToLower());
                    break;

                case "genre":
                    result = work.MovieRepository.Find(x => x.Genre.ToLower() == searchtext.ToLower());
                    break;
            }
           

            if (result == null)
                return new NotFoundObjectResult(
                    new ErrorJsonResponse(
                        404,
                        ErrorJsonResponse.ErrorMessages.NotFound,
                        new
                        {
                            searchtext
                        }
                    )
                );

            return new OkObjectResult(result);
        }

        // POST api/movies
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
