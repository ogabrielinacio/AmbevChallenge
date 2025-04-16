using Ambev.DeveloperEvaluation.Domain.Aggregates.BranchAggragate;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateBranch;

public class CreateBranchHandler : IRequestHandler<CreateBranchCommand, CreateBranchResponse>
{
    private readonly IBranchRepository _branchRepository;

    private readonly IUnitOfWork _unitOfWork;

    private IMapper _mapper;

    public CreateBranchHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<CreateBranchResponse> Handle(CreateBranchCommand command, CancellationToken cancellationToken)
    {
        
        var validator = new CreateBranchCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var branch = _mapper.Map<Branch>(command);
 
        var exists = await _branchRepository.GetByNameAsync(branch.Name,cancellationToken);
        if (exists != null)
            throw new InvalidOperationException($"Branch with name {branch.Name} already exists"); 
        var result = await _branchRepository.AddAsync(branch, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<CreateBranchResponse>(result);
    }
}