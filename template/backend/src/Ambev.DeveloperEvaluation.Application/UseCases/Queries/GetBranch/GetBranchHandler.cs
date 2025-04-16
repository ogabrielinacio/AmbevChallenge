using Ambev.DeveloperEvaluation.Domain.Aggregates.BranchAggragate;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetBranch;

public class GetBranchHandler : IRequestHandler<GetBranchQuery, GetBranchResponse>
{
   private readonly IBranchRepository _branchRepository;

   private IMapper _mapper;

   public GetBranchHandler(IBranchRepository branchRepository,IMapper mapper)
   {
      _branchRepository = branchRepository;
      _mapper = mapper;
   }

   public async Task<GetBranchResponse> Handle(GetBranchQuery request, CancellationToken cancellationToken)
   {
      var branch = await _branchRepository.GetByIdAsync(request.Id, cancellationToken);
      if (branch is null)
         throw new DomainNotFoundException("Branch not found");
      return _mapper.Map<GetBranchResponse>(branch);
   }
}