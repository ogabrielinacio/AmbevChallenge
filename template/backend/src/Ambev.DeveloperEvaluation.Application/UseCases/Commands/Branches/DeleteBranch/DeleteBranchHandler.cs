using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.DeleteBranch;

public class DeleteBranchHandler: IRequestHandler<DeleteBranchCommand, DeleteBranchResponse> 
{
    private readonly IBranchRepository _branchRepository;

    private readonly IUnitOfWork _unitOfWork;

    private IMapper _mapper; 
    
    public DeleteBranchHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<DeleteBranchResponse> Handle(DeleteBranchCommand command, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(command.Id);
        if(branch is null)
            throw new DomainNotFoundException("The branch could not be found.");
        
        if (!branch.IsOwner(command.RequestedBy))
            throw new DomainForbiddenAccessException("You don't have permission to perform this operation.");
        
        var result = await _branchRepository.Remove(command.Id, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<DeleteBranchResponse>(result);
    }
}