using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IBranchRepository _branchRepository;

    private readonly IUnitOfWork _unitOfWork;

    private IMapper _mapper;
    
   public CreateProductHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    } 
    
    public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var branch = await _branchRepository.GetByIdAsync(command.BranchId, cancellationToken);
        if(branch is null)
            throw new DomainNotFoundException("The branch could not be found.");
        
        if (!branch.HasPermission(command.RequestedBy))
            throw new DomainForbiddenAccessException("You don't have permission to perform this operation.");
        
        var exists = await _branchRepository.GetProductBranchByNameAsync(command.BranchId, command.Title,cancellationToken);
        if (exists != null)
            throw new InvalidOperationException($"Product with name {command.Title} already exists in this Branch"); 
        
        var product = _mapper.Map<Product>(command);
        
       var createdProduct  = await _branchRepository.AddProductAsync(branch.Id,product, cancellationToken);
       
       await _unitOfWork.CommitAsync(cancellationToken);
       return _mapper.Map<CreateProductResponse>(createdProduct);
    }
}