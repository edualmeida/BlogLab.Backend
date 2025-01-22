﻿using MediatR;

public class CategoryGetAllQuery : IRequest<List<CategoryResponse>>
{
    public class CategoryGetAllQueryHandler(
        ICategoriesQueryRepository repository
        ) : IRequestHandler<CategoryGetAllQuery, List<CategoryResponse>>
    {
        public async Task<List<CategoryResponse>> Handle(
            CategoryGetAllQuery request,
            CancellationToken cancellationToken)
            => await repository.GetAll(cancellationToken);
    }
}