namespace MoviesWeb.Migrations
{
    using System;
    using Bogus;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MoviesWeb.Models;
    using MoviesWeb.Repository;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesContext>
    {
        private UnitOfWork _unit;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoviesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            _unit = new UnitOfWork();
            _unit.BeginTransaction();

            var movies = _unit.MovieRepository.GetAll();
            if (!movies.Any())
            {
                for (var i = 0; i < 20; i++)
                {
                    var fake = new Faker<Movie>()
                        .RuleFor(a => a.Title, f => f.Company.Random.Words())
                        .RuleFor(a => a.YearOfRelease, f => f.Date.Past().Year)
                        .RuleFor(a => a.Genre, f => f.Random.Word());

                    _unit.MovieRepository.Add(fake.Generate());
                }
            }
            
            var users = _unit.UserRepository.GetAll();
            if (!users.Any())
            {
                for (var i = 0; i < 20; i++)
                {
                    var fake = new Faker<User>()
                        .RuleFor(a => a.Name, f => f.Name.FullName());

                    _unit.UserRepository.Add(fake.Generate());
                }
            }

            var userRatings = _unit.UserRatingRepository.GetAll();
            if (!userRatings.Any() &&
                movies.Any() &&
                users.Any()
                )
            {
                for (var i = 0; i < 20; i++)
                {
                    var fake = new Faker<UserRating>()
                        .RuleFor(a => a.Movie_Id, f => f.Random.Int(movies.Min(x => x.Id), movies.Max(x => x.Id)))
                        .RuleFor(a => a.User_Id, f => f.Random.Int(users.Min(x => x.Id), users.Max(x => x.Id)))
                        .RuleFor(a => a.Rating, f => f.Random.Decimal((decimal)0.0, (decimal)5.0))
                        .RuleFor(a => a.Comment, f => f.Random.Words());

                    _unit.UserRatingRepository.Add(fake.Generate());
                }
            }

            if (userRatings.Any())
            {
                //rounded to closest 0.5
                var averageRatings = userRatings
                .GroupBy(x => x.Movie_Id)
                .Select(movie => new
                {
                    Id = movie.Key,
                    Rating = Math.Round(movie.Average(s => s.Rating) * 2, MidpointRounding.AwayFromZero) / 2
                });

                foreach (var item in averageRatings)
                {
                    var movie = movies.FirstOrDefault(x => x.Id == item.Id);
                    movie.AverageRating = item.Rating;
                    _unit.MovieRepository.Update(movie);
                }
            };            

            _unit.EndTransaction();
        }

        private void SeedMovies()
        {

        }
    }
}
