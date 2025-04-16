using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetProductBranch;

public class GetProductProfile : Profile
{
   public GetProductProfile()
   {
      CreateMap<Product, GetProductBranchResponse>(); 
   } 
}