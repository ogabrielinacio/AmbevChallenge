using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateBranch;

public record CreateBranchResponse
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }

    public Address Address { get; init; }
}
