using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
{
    private readonly IBranchRepository _branchRepository;

    private readonly IUnitOfWork _unitOfWork;

    private IMapper _mapper; 
    
    public DeleteProductHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<DeleteProductResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(command.BranchId);
        if(branch is null)
            throw new DomainNotFoundException("The branch could not be found.");
        
        if (!branch.HasPermission(command.RequestedBy))
            throw new DomainForbiddenAccessException("You don't have permission to perform this operation."); 
        
        var result = await _branchRepository.RemoveProductAsync(command.BranchId, command.ProductId);
        await _unitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<DeleteProductResponse>(result);
        
    }
}