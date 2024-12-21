using Microsoft.Extensions.Logging;
using Movies.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Data
{
    public class MovieContextSeed
    {
        public static async Task SeedAsync(MovieContext movieContext,ILoggerFactory loggerFactory,int? retry = 0)
        {
            int retryForAvailabilty = retry.Value;
            try
            {
                await movieContext.Database.EnsureCreatedAsync();
                if(!movieContext.Movies.Any() )
                {
                    movieContext.Movies.AddRange(GetMovies());
                    await movieContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                if(retryForAvailabilty < 3)
                {
                    retryForAvailabilty++;
                    var log = loggerFactory.CreateLogger<MovieContextSeed>();
                    log.LogError($"Exception occure while connecting:{ex.Message}");
                    await SeedAsync(movieContext, loggerFactory, retryForAvailabilty);
                }
            }
        }

        private static IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>()
            {
                new Movie{MovieName="Avtar",DirectorName="James Cameron",ReleaseYear="2009"},
                new Movie{MovieName="Titanic",DirectorName="James Cameron",ReleaseYear="1997"},
                new Movie{MovieName="Die Another Day",DirectorName="Lee Thamorie",ReleaseYear="2002"},
                new Movie{MovieName="Godzilla",DirectorName="Gareth Edwards",ReleaseYear="2014"}
            };
        }
    }
}
