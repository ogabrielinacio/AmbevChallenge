using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>()
            .ConstructUsing((src, ctx) =>
            {
                var requestedBy = (Guid)ctx.Items["RequestedBy"];
                return new CreateProductCommand{
                    BranchId = src.BranchId,
                    Title = src.Title,
                    Description = src.Description,
                    Price = src.Price,
                    Category = src.Category,
                    Image = src.Image,
                    RequestedBy = requestedBy
                    
                };
            });
        CreateMap<CreateProductResponse, CreateProductAPIResponse>();
    }
}
