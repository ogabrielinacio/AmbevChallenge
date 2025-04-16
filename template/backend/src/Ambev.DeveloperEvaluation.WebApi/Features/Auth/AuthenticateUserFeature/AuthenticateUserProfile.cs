using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Auth.AuthenticateUser;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;

/// <summary>
/// AutoMapper profile for authentication-related mappings
/// </summary>
public sealed class AuthenticateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserProfile"/> class
    /// </summary>
    public AuthenticateUserProfile()
    {
        // Request → Command (controller → application)
        CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();

        // Result (handler) → Response (controller)
        CreateMap<AuthenticateUserResult, AuthenticateUserResponse>();
        // CreateMap<User, AuthenticateUserResponse>()
        //     .ForMember(dest => dest.Token, opt => opt.Ignore())
        //     .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
