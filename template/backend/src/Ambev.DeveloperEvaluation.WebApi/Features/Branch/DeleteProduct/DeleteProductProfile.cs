using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.DeleteProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.DeleteProduct;

public class DeleteProductProfile : Profile
{
   public DeleteProductProfile()
   {
     CreateMap<DeleteProductRequest, DeleteProductCommand>()
           .ConstructUsing((src, ctx) =>
           {
               var requestedBy = (Guid)ctx.Items["RequestedBy"];
               return new DeleteProductCommand{
                   BranchId = src.BranchId,
                   ProductId = src.ProductId,
                   RequestedBy = requestedBy
               };
           }); 
   }

}