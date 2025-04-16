using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.DeleteProduct;

public record DeleteProductCommand : IRequest<DeleteProductResponse>
{
    public Guid BranchId { get; init; }
    public Guid ProductId { get; init; }
    public Guid RequestedBy { get; init; }
};