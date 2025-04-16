using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetProductBranch;

public record GetProductBranchQuery : IRequest<GetProductBranchResponse>
{
   public Guid BranchId { get; init; } 
   public Guid ProductId { get; init; } 
   public Guid RequestedBy { get; init; } 
}