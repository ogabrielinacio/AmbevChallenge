using Ambev.DeveloperEvaluation.Domain.Aggregates.BranchAggragate;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.DeleteBranch;

public class DeleteBranchProfile: Profile
{
    public DeleteBranchProfile()
    {
        CreateMap<bool, DeleteBranchResponse>()
            .ConstructUsing(success => new DeleteBranchResponse(success));
    }
}