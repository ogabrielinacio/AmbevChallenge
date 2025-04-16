using Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.GetBranch;

public class GetBranchProfile : Profile
{
   public GetBranchProfile()
   {
      CreateMap<Guid, GetBranchQuery>()
         .ConstructUsing((id, ctx) =>
         {
            var requestedBy = (Guid)ctx.Items["RequestedBy"];
            return new GetBranchQuery(){
               Id = id,  
               RequestedBy = requestedBy
                    
            };
         });
      CreateMap<GetBranchResponse, GetBranchAPIResponse>();
   } 
}