﻿using Microsoft.EntityFrameworkCore;
using Movies.Core.Entites;
using Movies.Core.Repositories;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories
{
    public class MovieRepository: Repository<Movie>,IMovieRepository
    {
        public MovieRepository(MovieContext movieContext):base(movieContext)
        {
            
        }

        public async Task<IEnumerable<Movie>> GetMovieByDirectorName(string directorName)
        {
            return await _movieContext.Movies
                .Where(m => m.DirectorName == directorName).ToListAsync();
        }
    }
}
