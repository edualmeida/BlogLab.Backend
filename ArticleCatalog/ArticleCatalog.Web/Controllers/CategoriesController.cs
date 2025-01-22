using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
public class CategoriesController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<List<CategoryResponse>>> GetAll()
        => await Send(new CategoryGetAllQuery());
}