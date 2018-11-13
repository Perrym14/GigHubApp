using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGenreRepository
    {
        List<Genre> GetListOfGenres();
    }
}