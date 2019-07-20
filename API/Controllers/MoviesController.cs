using MoviesWeb.Models;
using MoviesWeb.Repository;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http;
using System.Linq;
using System;

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
                    var yearOutput = 0;
                    var isInt = int.TryParse(searchtext, out yearOutput);
                    if (!isInt)
                    {
                        response = Request.CreateResponse(HttpStatusCode.BadRequest, $"{searchtext} is not a year!");
                        return response;
                    }

                    result = _work.MovieRepository.Find(x => x.YearOfRelease == yearOutput);
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

        [Route("top/{filter}")]
        [HttpGet]
        public HttpResponseMessage GetTopMovies(int filter)
        {
            //search movies by title, year of release, genre(s)
            IEnumerable<Movie> result = null;
            HttpResponseMessage response;

            if (filter <= 0)
                response = Request.CreateResponse(HttpStatusCode.BadRequest, filter);

            result = _work.MovieRepository.GetAll()
                .OrderByDescending(x => x.AverageRating)
                .ThenBy(x => x.Title)
                .Take(filter);

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

        [Route("top/{filter}/user/{fullname}")]
        [HttpGet]
        public HttpResponseMessage GetTopMoviesByUser(int filter, string fullName)
        {
            //search movies by title, year of release, genre(s)
            HttpResponseMessage response;

            if (filter <= 0)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, filter);
                return response;
            }


            var user = _work.UserRepository.Find(x => x.Name.ToLower() == fullName.ToLower()).FirstOrDefault();
            if(user == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "User Not Found");
                return response;
            }

            var movies = _work.MovieRepository.GetAll();

            var userRatings = _work.UserRatingRepository
                .Find(x => x.User_Id == user.Id)
                .OrderByDescending(x => x.Rating)
                .AsEnumerable();

            var result = (from movie in movies
                        join userRating in userRatings on movie.Id equals userRating.Movie_Id
                        select userRating)
                        .OrderByDescending(x => x.Rating)
                        .ThenBy(x => x.Movie.Title)
                        .Take(filter);

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
        public void Post([FromBody] UserRating value)
        {
        }

        // PUT api/movies/5
        [HttpPut]
        public void Put(int id, [FromBody] UserRating value)
        {
        }

        // DELETE api/movies/5
        [HttpDelete]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
