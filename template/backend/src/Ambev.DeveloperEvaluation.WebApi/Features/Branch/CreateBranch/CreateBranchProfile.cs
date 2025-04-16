using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.CreateBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.CreateBranch;

public class CreateBranchProfile : Profile
{
   public CreateBranchProfile()
   {
      CreateMap<CreateBranchRequest, CreateBranchCommand>()
         .ConstructUsing((src, ctx) =>
         {
            var ownerId = (Guid)ctx.Items["OwnerId"];
            return new CreateBranchCommand{
              Name = src.Name, 
               Address = src.Address ,
               OwnerId = ownerId
               
            };
         });
      
      CreateMap<CreateBranchResponse, CreateBranchAPIResponse>();
 
   } 
}