using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.UpdateBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.UpdateBranch;

public class UpdateBranchProfile : Profile
{
    public UpdateBranchProfile()
    {
       CreateMap<UpdateBranchRequest, UpdateBranchCommand>()
           .ConstructUsing((src, ctx) =>
           {
               var requestedBy = (Guid)ctx.Items["RequestedBy"];
               return new UpdateBranchCommand{
                   Name = src.Name, 
                   Address = src.Address ,
                   RequestedBy = requestedBy
               };
           });
    }
}