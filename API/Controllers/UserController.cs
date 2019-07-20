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
    [RoutePrefix("api/user")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly UnitOfWork _work;

        public UserController()
        {
            _work = new UnitOfWork();
        }
        
        // GET api/user/id
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/user
        [HttpPost]
        public void Post([FromBody] UserRating value)
        {
            throw new NotImplementedException();
        }

        // PUT api/user/5
        [Route("{fullname}/movie/{movieid}/rating/{rating}")]
        [HttpPut]
        public HttpResponseMessage Put(string fullName, int movieid, int rating, [FromBody] UserRating inUserRating)
        {
            HttpResponseMessage response;

            var user = _work.UserRepository.Find(x => x.Name.ToLower() == fullName.ToLower()).FirstOrDefault();
            if (user == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "User Not Found");
                return response;
            }

            if (movieid <= 0)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, movieid);
                return response;
            }

            if (rating <= 0)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, rating);
                return response;
            }

            var userRating = _work.UserRatingRepository
                .Find(x => x.User_Id == user.Id && x.Movie_Id == movieid)
                .FirstOrDefault();

            if (userRating == null)
                userRating = new UserRating(){
                    Movie_Id = movieid,
                    User_Id = user.Id
                };

            userRating.Rating = rating;

            if (inUserRating != null)
                userRating.Comment = inUserRating.Comment;


            if (userRating.Id > 0)
            {
                _work.UserRatingRepository.Update(userRating);
            }
            else
            {
                _work.UserRatingRepository.Add(userRating);
            }
            
            _work.Complete();
            
            //update average total user rating for movie
            var allUserRatingsForMovie = _work.UserRatingRepository.Find(x => x.Movie_Id == movieid);

            var movie = _work.MovieRepository.Get(movieid);
            movie.AverageRating = Math.Round(allUserRatingsForMovie.Average(x => x.Rating) * 2, MidpointRounding.AwayFromZero) / 2;
            _work.MovieRepository.Update(movie);

            _work.Complete();

            response = Request.CreateResponse(HttpStatusCode.OK, movie);

            return response;
        }

        // DELETE api/user/5
        [HttpDelete]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
