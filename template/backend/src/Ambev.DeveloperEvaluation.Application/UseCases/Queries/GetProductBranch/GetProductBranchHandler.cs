using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetProductBranch;

public class GetProductBranchHandler : IRequestHandler<GetProductBranchQuery, GetProductBranchResponse>
{
    private readonly IBranchRepository _branchRepository;

    private IMapper _mapper;
    
    public GetProductBranchHandler(IBranchRepository branchRepository,IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
    }
    
    public async Task<GetProductBranchResponse> Handle(GetProductBranchQuery command, CancellationToken cancellationToken)
    {
        
        var branch = await _branchRepository.GetByIdAsync(command.BranchId, cancellationToken);
        if (branch is null)
            throw new DomainNotFoundException("Branch not found");
        
        if (!branch.HasPermission(command.RequestedBy))
            throw new DomainForbiddenAccessException("You don't have permission to perform this operation."); 
        
        var result = await  _branchRepository.GetProductByIdAsync(command.BranchId, command.ProductId, cancellationToken);
        return _mapper.Map<GetProductBranchResponse>(result);
    }
}