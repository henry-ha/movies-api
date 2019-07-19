using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Http;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // GET api/movies
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/movies/{filter}/{searchtext}
        [HttpGet("{filter}/{searchtext}")]
        public ActionResult<string> Get(string filter, string searchtext)
        {
            //search movies by title, year of release, genre(s)
            string result = null;

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

            //get by searchtext

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
