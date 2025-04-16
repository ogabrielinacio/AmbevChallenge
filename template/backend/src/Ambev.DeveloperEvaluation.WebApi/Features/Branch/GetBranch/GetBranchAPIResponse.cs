using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.GetBranch;

public record GetBranchAPIResponse
{
    public string Name {get; init; }
    public Address Address {get; init; } 
}