
using FluentAssertions;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Genres.GenreManagers.Exceptions;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;

namespace MovieClub.Services.UnitTests.Films
{
    public class AddFilmTests : BusinessUnitTest
    {
        private readonly FilmService _sut;

        public AddFilmTests()
        {
            _sut = FilmServiceFactory.Create(SetupContext);
        }

        [Fact]
        public async Task Add_adds_a_new_film_properly()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var dto = AddFilmDtoFactory.Create(genre.Id);

            await _sut.Add(dto);

            var actual = ReadContext.Films.Single();
            actual.Name.Should().Be(dto.Name);
            actual.Description.Should().Be(dto.Description);
            actual.PublishYear.Should().Be(dto.PublishYear);
            actual.DailyPriceRent.Should().Be(dto.DailyPriceRent);
            actual.MinAgeLimit.Should().Be(dto.MinAgeLimit);
            actual.PenaltyPriceRent.Should().Be(dto.PenaltyPriceRent);
            actual.Duration.Should().Be(dto.Duration);
            actual.Director.Should().Be(dto.Director);
            actual.GenreId.Should().Be(dto.GenreId);
        }

        [Fact]
        public async Task Add_throw_GenreIsNotExistException()
        {
            var dummyGenreId = 4;
            var dto = AddFilmDtoFactory.Create(dummyGenreId);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<GenreIsNotExistException>();
        }
    }
}