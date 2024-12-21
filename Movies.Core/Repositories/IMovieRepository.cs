using Movies.Core.Entites;
using Movies.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        //here we can have all custom operation
        Task<IEnumerable<Movie>> GetMovieByDirectorName(string directorName);
    }
}
