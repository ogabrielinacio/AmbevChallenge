using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.UpdateProduct;

public class UpdateProductProfile : Profile
{
   public UpdateProductProfile()
   {
      CreateMap<UpdateProductCommand, Product>();
      CreateMap<bool, UpdateProductResponse>()
         .ConstructUsing(success => new UpdateProductResponse(success));

   } 
}