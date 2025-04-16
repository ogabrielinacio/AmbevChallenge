using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateBranch;

public record CreateBranchCommand : IRequest<CreateBranchResponse>
{
   public string Name { get; init; } 
   public Address Address { get; init; }
   public Guid OwnerId { get; init; }

   public ValidationResultDetail Validate()
   {
      var validator = new CreateBranchCommandValidator();
      var result = validator.Validate(this);
      return new ValidationResultDetail
      {
         IsValid = result.IsValid,
         Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
      };
   }
}