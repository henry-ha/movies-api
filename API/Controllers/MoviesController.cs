using MoviesWeb.Models;
using MoviesWeb.Repository;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http;
using System.Linq;

namespace API.Controllers
{
    [RoutePrefix("api/movies")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MoviesController : ApiController
    {
        private readonly UnitOfWork _work;

        public MoviesController()
        {
            _work = new UnitOfWork();
        }


        // GET api/movies/{filter}/{searchtext}
        [Route("{filter}/{searchtext}")]
        [HttpGet]
        public HttpResponseMessage Get(string filter, string searchtext)
        {
            //search movies by title, year of release, genre(s)
            IEnumerable<Movie> result = null;
            HttpResponseMessage response;

            // 400 - invalid / no criteria given
            if (string.IsNullOrWhiteSpace(searchtext))
                response = Request.CreateResponse(HttpStatusCode.BadRequest, searchtext);

            if (string.IsNullOrWhiteSpace(filter))
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Please select a filter");


            //get by searchtext
            switch (filter.ToLower())
            {
                case "title":
                    result = _work.MovieRepository.Find(x => x.Title.ToLower().Contains(searchtext.ToLower()));
                    break;

                case "year":
                    result = _work.MovieRepository.Find(x => x.YearOfRelease.ToString() == searchtext.ToLower());
                    break;

                case "genre":
                    result = _work.MovieRepository.Find(x => x.Genre.ToLower().Contains(searchtext.ToLower()));
                    break;
            }


            if (!result.Any())
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }

            return response;
        }

        // POST api/movies
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/movies/5
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/movies/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
