using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.DeleteProduct;

public class DeleteProductProfile: Profile
{
    public DeleteProductProfile()
    {
        CreateMap<bool, DeleteProductResponse>()
            .ConstructUsing(success => new DeleteProductResponse(success));
    }
}