using Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetProductBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.GetProduct;

public class GetProductProfile : Profile
{
   public GetProductProfile()
   {
      CreateMap<GetProductRequest, GetProductBranchQuery>()
         .ConstructUsing((src, ctx) =>
         {
            var requestedBy = (Guid)ctx.Items["RequestedBy"];
            return new GetProductBranchQuery{
               BranchId = src.BranchId,
               ProductId = src.ProductId,
               RequestedBy = requestedBy
            };
         });

      CreateMap<GetProductBranchResponse, GetProductBranchAPIResponse>();
   } 
}