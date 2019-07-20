namespace MoviesWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MoviesWeb.Models;
    using MoviesWeb.Repository;
    using Bogus;

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
                for (var i = 0; i < 100; i++)
                {
                    //var fake = new Faker<Movie>()
                    //    .RuleFor(a => a.Title, f => f.Movies().MovieTitle())
                    //    .RuleFor(a => a.YearOfRelease, f => f.Movies().MovieReleaseDate().Year)
                    //    .RuleFor(a => a.Genre, f => f.Movies().Movie().Genres.ToString());

                    var fake = new Faker<Movie>()
                        .RuleFor(a => a.Title, f => f.Company.Random.Words())
                        .RuleFor(a => a.YearOfRelease, f => f.Date.Past(50).Year)
                        .RuleFor(a => a.Genres, f => f.Random.Words().Replace(" ", ","))
                        .RuleFor(a => a.Duration, f => f.Date.Timespan(TimeSpan.FromHours(6)));

                    _unit.MovieRepository.Add(fake.Generate());
                }
            }
            
            var users = _unit.UserRepository.GetAll();
            if (!users.Any())
            {
                for (var i = 0; i < 100; i++)
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
                for (var i = 0; i < 100; i++)
                {
                    var fake = new Faker<UserRating>()
                        .RuleFor(a => a.Movie_Id, f => f.Random.Int(movies.Min(x => x.Id), movies.Max(x => x.Id)))
                        .RuleFor(a => a.User_Id, f => f.Random.Int(users.Min(x => x.Id), users.Max(x => x.Id)))
                        .RuleFor(a => a.Rating, f => f.Random.Int(1, 5))
                        .RuleFor(a => a.Comment, f => f.Lorem.Sentences());

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
