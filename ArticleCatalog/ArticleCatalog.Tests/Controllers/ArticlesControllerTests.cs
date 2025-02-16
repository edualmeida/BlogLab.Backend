using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ArticleCatalog.Tests.Controllers
{
    public class ArticlesControllerTests
    {
        private readonly Fixture fixture = new();
        private readonly Mock<IMediator> mediator = new();
        private readonly ArticlesController controller;

        public ArticlesControllerTests()
        {
            controller = new ArticlesController(mediator.Object);
        }

        // [Fact]
        // public async Task GetAll_ReturnsAnActionResult_WithAListOfArticleResponses()
        // {
        //     var expected = fixture.CreateMany<ArticleResponse>().ToList();
        //
        //     mediator.Setup(repo => repo.Send(It.IsAny<IRequest<List<ArticleResponse>>>(), It.IsAny<CancellationToken>()))
        //         .ReturnsAsync(expected);
        //
        //     var result = await controller.GetAll();
        //
        //     var actionResult = Assert.IsAssignableFrom<ActionResult<List<ArticleResponse>>>(result);
        //     Assert.NotNull(actionResult.Value);
        //
        //     var response = Assert.IsAssignableFrom<List<ArticleResponse>>(actionResult.Value);
        //     Assert.Equal(expected.Count, response.Count());
        // }
        //
        // [Fact]
        // public async Task GetAll_ReturnsAnActionResult_WithEqualsListOfArticles()
        // {
        //     var expected = fixture.CreateMany<ArticleResponse>().ToList();
        //
        //     mediator.Setup(repo => repo.Send(It.IsAny<IRequest<List<ArticleResponse>>>(), It.IsAny<CancellationToken>()))
        //         .ReturnsAsync(expected);
        //
        //     var result = await controller.GetAll();
        //
        //     var actionResult = Assert.IsAssignableFrom<ActionResult<List<ArticleResponse>>>(result);
        //     Assert.NotNull(actionResult.Value);
        //
        //     var response = Assert.IsAssignableFrom<List<ArticleResponse>>(actionResult.Value);
        //     response.Should().BeEquivalentTo(expected);
        // }
        //
        // [Fact]
        // public async Task GetAll_ThrowsAnException_WhenExceptionHappens()
        // {
        //     var expected = fixture.CreateMany<ArticleResponse>().ToList();
        //
        //     mediator.Setup(repo => repo.Send(It.IsAny<IRequest<List<ArticleResponse>>>(), It.IsAny<CancellationToken>()))
        //         .ThrowsAsync(new Exception("Any exception"));
        //
        //     await Assert.ThrowsAnyAsync<Exception>(() => controller.GetAll());
        // }

        //[Fact]
        //public async Task MiddlewareTest_ReturnsNotFoundForRequest()
        //{
        //    using var host = await new HostBuilder()
        //        .ConfigureWebHost(webBuilder =>
        //        {
        //            webBuilder
        //                .UseTestServer()
        //                .ConfigureServices(services =>
        //                {
        //                    services.AddArticleCatalogWebComponents();
        //                })
        //                .Configure(app =>
        //                {
        //                    app.UseWebService(app.Environment);
        //                });
        //        })
        //        .StartAsync();

        //    var response = await host.GetTestClient().GetAsync("api/articles");

        //    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        //}
    }
}
