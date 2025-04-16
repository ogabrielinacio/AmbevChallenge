using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetBranch;

public record GetBranchResponse
{
   public string Name {get; init; }
   public Address Address {get; init; }
}