using Ambev.DeveloperEvaluation.Domain.Aggregates.BranchAggragate;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateBranch;

public sealed class CreateBranchProfile : Profile
{
    public CreateBranchProfile()
    {
        CreateMap<CreateBranchCommand, Branch>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                new Address(src.Address.Street, src.Address.City, src.Address.State, src.Address.Country, src.Address.ZipCode)
            ));

        CreateMap<Branch, CreateBranchResponse>();

    }
}