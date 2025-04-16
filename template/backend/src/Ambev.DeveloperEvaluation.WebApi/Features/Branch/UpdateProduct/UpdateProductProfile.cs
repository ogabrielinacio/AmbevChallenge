using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateProduct;
using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.UpdateProduct;

public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductRequest, UpdateProductCommand>()
            .ConstructUsing((src, ctx) =>
            {
                var requestedBy = (Guid)ctx.Items["RequestedBy"];
                return new UpdateProductCommand{
                    BranchId = src.BranchId,
                    Title = src.Title,
                    Description = src.Description,
                    Price = src.Price,
                    Category = src.Category,
                    Image = src.Image,
                    RequestedBy = requestedBy
                };
            });
        CreateMap<UpdateProductResponse, UpdateProductAPIResponse>(); 
    }
}