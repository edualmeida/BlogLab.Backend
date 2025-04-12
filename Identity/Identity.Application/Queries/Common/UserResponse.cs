﻿using AutoMapper;
using Identity.Domain;

namespace Identity.Application.Queries.Common;
public class UserResponse : IMapFrom<User>
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<User, UserResponse>();
}
