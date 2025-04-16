using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.UpdateBranch;

public class UpdateBranchCommand : IRequest<UpdateBranchResponse>
{
   public Guid Id { get; init; }
   public string Name { get; init; } 
   public Address Address { get; init; }
   
   public Guid RequestedBy { get; init; }

   public ValidationResultDetail Validate()
   {
      var validator = new UpdateBranchCommandValidator();
      var result = validator.Validate(this);
      return new ValidationResultDetail
      {
         IsValid = result.IsValid,
         Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
      };
   }
}