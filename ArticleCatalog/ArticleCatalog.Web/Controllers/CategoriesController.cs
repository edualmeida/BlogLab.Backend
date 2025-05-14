using ArticleCatalog.Application.Categories.Queries.Common;
using ArticleCatalog.Application.Categories.Queries.GetAll;
using Common.Web;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleCatalog.Web.Controllers;
[Authorize(AuthenticationSchemes = ApiKeyConstants.SchemeName)]
public class CategoriesController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<List<CategoryResponse>>> GetAll()
        => await Send(new CategoryGetAllQuery());
}