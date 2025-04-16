using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetProductBranch;

public class GetProductBranchProfile : Profile
{
   public GetProductBranchProfile()
   {
      CreateMap<Product,GetProductBranchResponse>();
   } 
}