using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ArticleCatalog.Web.Tests
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

        [Fact]
        public async Task GetAll_ReturnsAnActionResult_WithAListOfBikeResponses()
        {
            var expected = fixture.CreateMany<ArticleResponse>().ToList();

            mediator.Setup(repo => repo.Send(It.IsAny<IRequest<List<ArticleResponse>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await controller.GetAll();

            var actionResult = Assert.IsAssignableFrom<ActionResult<List<ArticleResponse>>>(result);
            Assert.NotNull(actionResult.Value);
            var model = Assert.IsAssignableFrom<List<ArticleResponse>>(actionResult.Value);
            Assert.Equal(expected.Count, model.Count());
        }
    }
}
