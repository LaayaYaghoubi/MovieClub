using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Genres;
using MovieClub.Services.Genres.GenreManagers;
using MovieClub.Services.Genres.GenreManagers.Contracts;

namespace MovieClub.Tests.Tools.Genres
{
    public static class GenreServiceFactory
    {
        public static GenreService Create(EFDataContext context)
        {
            return new GenreAppService(new EFGenreRepository(context), new EFUnitOfWork(context));
        }
    }
}
