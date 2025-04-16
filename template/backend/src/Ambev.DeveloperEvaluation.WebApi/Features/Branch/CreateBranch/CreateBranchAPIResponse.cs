using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CreateBranch;

public record CreateBranchAPIResponse
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }

    public Address Address { get; init; }
    
}