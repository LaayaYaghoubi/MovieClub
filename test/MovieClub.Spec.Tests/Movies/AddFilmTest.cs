using FluentAssertions;
using MovieClub.Entities;
using MovieClub.Services.Films.FilmMananger.Contracts;
using MovieClub.Services.Films.FilmMananger.Contracts.Dtos;
using MovieClub.Tests.Tools.Films;
using MovieClub.Tests.Tools.Genres;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig;
using MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using Xunit;

namespace MovieClub.Spec.Tests.Movies;

[Scenario("اضافه شدن فیلم")]
[Story("",
    AsA = "مدیر کلاب ",
    IWantTo = "فیلم جدید اضافه کنم ",
    InOrderTo = "فیلم ها را اجاره دهم")]

public class AddFilmTest : BusinessIntegrationTest
{
    private readonly FilmService _sut;
    private Genre _genre;

    public AddFilmTest()
    {
        _sut = FilmServiceFactory.Create(SetupContext);
    }
    [Given("هیچ فیلمی در فهرست فیلم ها وجود ندارد")]
    [And("یک ژانر به اسم کمدی در فهرست ژانرها وجود دارد")]
    private void Given()
    {
        _genre = new GenreBuilder()
            .WithTitle("کمدی")
            .Build();
        DbContext.Save(_genre);
    }

    [When("فیلم با عنوان  کریم و سال تولید 1380 و مدت زمان 120 دقیقه و کاگردان مهدی رحیمی را اضافه می کنم")]
    private async Task When()
    {
        var dto = new AddFilmDto()
        {
            Director = "مهدی رحیمی",
            PublishYear = new DateTime(2004, 1, 1),
            Duration = 120,
            Description = "",
            DailyPriceRent = 5645654,
            PenaltyPriceRent = 18,
            Name = "کریم",
            MinAgeLimit = 16,
            GenreId = _genre.Id
        };


        await _sut.Add(dto);

    }

    [Then("تنها یک فیلم با عنوان  کریم و سال تولید 1380 و مدت زمان 120 دقیقه و کاگردان مهدی رحیمی باید در فهرست فیلم ها وجود داشته باشد ")]
    private void Then()
    {
        var actual = ReadContext.Films.Single();
        actual.Director.Should().Be("مهدی رحیمی");
    }


    [Fact]
    public void Run()
    {
        Runner.RunScenario(
            _ => Given(),
            _ => When().Wait(),
            _ => Then());
    }
}