using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.DeleteBranch;

public class DeleteBranchCommand : IRequest<DeleteBranchResponse>
{
   public Guid Id { get; init; } 
   public Guid RequestedBy { get; init; }
}