using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.UpdateBranch;

public record UpdateBranchRequest()
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Address Address { get; init; }
}