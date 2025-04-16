using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Aggregates.BranchAggragate;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.UpdateBranch;


public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, UpdateBranchResponse>
{
    private readonly IBranchRepository _branchRepository;

    private readonly IUnitOfWork _unitOfWork;

    private IMapper _mapper;

    public UpdateBranchHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<UpdateBranchResponse> Handle(UpdateBranchCommand command, CancellationToken cancellationToken)
    {
        
        var validator = new UpdateBranchCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var branch = await _branchRepository.GetByIdAsync(command.Id);
        if(branch is null)
            throw new DomainNotFoundException("The branch could not be found.");
        
        if (!branch.IsOwner(command.RequestedBy))
            throw new DomainForbiddenAccessException("You don't have permission to perform this operation.");
        
        
        branch.UpdateName(command.Name);
        branch.UpdateAddress(command.Address);
        
        var result = await _branchRepository.Update(branch, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<UpdateBranchResponse>(result);
    }
}