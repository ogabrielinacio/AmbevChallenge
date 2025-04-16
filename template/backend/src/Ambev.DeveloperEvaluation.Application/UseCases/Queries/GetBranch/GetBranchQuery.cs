using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetBranch;

public record GetBranchQuery : IRequest<GetBranchResponse>
{
    public Guid Id { get; init; }
    public Guid RequestedBy { get; init; }
}