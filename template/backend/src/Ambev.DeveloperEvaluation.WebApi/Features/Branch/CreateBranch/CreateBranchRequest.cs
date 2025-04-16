using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CreateBranch;

public record CreateBranchRequest
{
    public string Name { get; init; }

    public Address Address { get; init; } 
}