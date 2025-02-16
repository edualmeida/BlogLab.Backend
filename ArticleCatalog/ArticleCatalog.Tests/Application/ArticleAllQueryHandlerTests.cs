using AutoFixture;
using Moq;
using static ArticleGetAllQuery;

namespace ArticleCatalog.Tests.Application
{
    public class ArticleAllQueryHandlerTests
    {
        private readonly Fixture fixture = new();
        private readonly Mock<IArticleQueryRepository> repository = new();
        private readonly Mock<IAuthorsHttpService> httpervice = new();
        private readonly ArticleAllQueryHandler handler;
        public ArticleAllQueryHandlerTests()
        {
            handler = new ArticleAllQueryHandler(repository.Object, httpervice.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsAListOfBikeResponses()
        {
            var expected = fixture.CreateMany<ArticleResponse>().ToList();
            var input = fixture.Create<ArticleGetAllQuery>();

            repository.Setup(repo => repo.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await handler.Handle(input, new CancellationToken());

            var assignResult = Assert.IsAssignableFrom<List<ArticleResponse>>(result);
            Assert.NotNull(assignResult);
            Assert.Equal(expected.Count, assignResult.Count());
        }

        [Fact]
        public async Task GetAll_ThrowsAnException_WhenExceptionHappens()
        {
            var expected = fixture.CreateMany<ArticleResponse>().ToList();
            var input = fixture.Create<ArticleGetAllQuery>();

            repository.Setup(repo => repo.GetAll(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Any exception"));

            await Assert.ThrowsAnyAsync<Exception>(() => handler.Handle(input, new CancellationToken()));
        }
    }
}
