using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.DeleteBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.DeleteBranch;

public class DeleteBranchProfile : Profile
{
   public DeleteBranchProfile()
   {
       CreateMap<Guid, DeleteBranchCommand>()
           .ConstructUsing((id, ctx) =>
           {
               var requestedBy = (Guid)ctx.Items["RequestedBy"];
               return new DeleteBranchCommand{
                   Id = id,  
                   RequestedBy = requestedBy
                    
               };
           });
   } 
}