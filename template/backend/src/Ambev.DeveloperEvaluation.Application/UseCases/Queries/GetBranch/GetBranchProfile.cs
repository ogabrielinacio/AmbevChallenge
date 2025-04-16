using Ambev.DeveloperEvaluation.Domain.Aggregates.BranchAggragate;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetBranch;

public class GetBranchProfile : Profile
{
   public GetBranchProfile()
   {
       CreateMap<GetBranchQuery, Branch>();

       CreateMap<Branch, GetBranchResponse>(); 
   }
}