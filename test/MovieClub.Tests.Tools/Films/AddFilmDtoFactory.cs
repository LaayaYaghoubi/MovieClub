
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;

namespace MovieClub.Tests.Tools.Films
{
    public static class AddFilmDtoFactory
    {
        public static AddFilmDto Create(int genreId)
        {
            return new AddFilmDto
            {
                Name = "dummy-film-name",
                Description = "dummy-film-Description",
                PublishYear = new DateTime(2000, 12, 1),
                DailyPriceRent = 100.12M,
                MinAgeLimit = 14,
                PenaltyPriceRent = 0.10M,
                Duration = 2,
                Director = "jonney_depp",
                GenreId = genreId
            };
        }
    }
}
