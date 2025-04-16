using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly IBranchRepository _branchRepository;

    private readonly IUnitOfWork _unitOfWork;

    private IMapper _mapper;
    
    public UpdateProductHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    } 
    
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var branch = await _branchRepository.GetByIdAsync(command.BranchId, cancellationToken);
        if(branch is null)
            throw new DomainNotFoundException("The branch could not be found.");
        
        if (!branch.HasPermission(command.RequestedBy))
            throw new DomainForbiddenAccessException("You don't have permission to perform this operation.");

        var newProduct = _mapper.Map<Product>(command);
        
        var result = await _branchRepository.UpdateProductAsync(branch.Id,newProduct, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<UpdateProductResponse>(result);
    }
}
