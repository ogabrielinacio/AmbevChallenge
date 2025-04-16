using System.Security.Claims;
using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateProduct;
using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.DeleteBranch;
using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.UpdateBranch;
using Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetBranch;
using Ambev.DeveloperEvaluation.Application.UseCases.Queries.GetProductBranch;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branch.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Branch.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Branch.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branch.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Branch.UpdateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branch.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.CreateBranch;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch;

[ApiController]
[Route("api/[controller]")]
public class BranchesController : BaseController
{
   private readonly IMediator _mediator;
   private readonly IMapper _mapper;

   public BranchesController(IMediator mediator, IMapper mapper)
   {
      _mediator = mediator;
      _mapper = mapper;
   }

   [HttpPost]
   public async Task<IActionResult> CreateBranch([FromBody] CreateBranchRequest request, CancellationToken cancellationToken)
   {
       var validator = new CreateBranchRequestValidator();
       var validationResult = await validator.ValidateAsync(request, cancellationToken);
       
       if (!validationResult.IsValid)
           return BadRequest(validationResult.Errors);
       
       var requestedUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
       
       var command = _mapper.Map<CreateBranchCommand>(request, 
           opt => opt.Items["OwnerId"] = requestedUserId);
       
       var response = await _mediator.Send(command, cancellationToken);
       return Created(string.Empty, new ApiResponseWithData<CreateBranchAPIResponse>
       {
           Success = true,
           Message = "Branch created",
           Data = _mapper.Map<CreateBranchAPIResponse>(response)
       });
   }
   
   
   [Authorize(Roles = "Admin")]
   [HttpPut]
   public async Task<IActionResult> UpdateBranch([FromBody] UpdateBranchRequest request, CancellationToken cancellationToken)
   {
       var validator = new UpdateBranchRequestValidator();
       var validationResult = await validator.ValidateAsync(request, cancellationToken);
       
       if (!validationResult.IsValid)
           return BadRequest(validationResult.Errors);
       
       var requestedUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
       
       var command = _mapper.Map<UpdateBranchCommand>(request, 
           opt => opt.Items["RequestedBy"] = requestedUserId);
       var response = await _mediator.Send(command, cancellationToken);
       return Ok(new ApiResponse
       {
           Success = true,
           Message = "Branch updated",
       });
   }
   
   
   [Authorize(Roles = "Admin")]
   [HttpDelete("{branchId}")]
   public async Task<IActionResult> DeleteBranch([FromRoute] Guid branchId , CancellationToken cancellationToken)
   {
       var requestedUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

       var command = _mapper.Map<DeleteBranchCommand>(branchId,
           opt => opt.Items["RequestedBy"] = requestedUserId);
       
       var response = await _mediator.Send(command, cancellationToken);
       
       return Ok( new ApiResponse
       {
           Success = true,
           Message = "Branch Deleted",
       });
   }
   
   
   [Authorize(Roles = "Admin, Manager")]
   [HttpGet("{branchId}")]
   public async Task<IActionResult> GetBranch([FromRoute] Guid branchId, CancellationToken cancellationToken)
   {
       
       var requestedUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
       
       var command = _mapper.Map<GetBranchQuery>(branchId, 
           opt => opt.Items["RequestedBy"] = requestedUserId);
       
       var response = await _mediator.Send(command, cancellationToken);
       return Ok(new ApiResponseWithData<GetBranchAPIResponse>
       {
           Success = true,
           Message = "Branch Retrieved",
           Data = _mapper.Map<GetBranchAPIResponse>(response)
       });
   }
   
   [Authorize(Roles = "Admin,Manager")]
   [HttpPost]
   [Route("[action]")]
   public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
   {
       var validator = new CreateProductRequestValidator();
       var validationResult = await validator.ValidateAsync(request, cancellationToken);
       
       if (!validationResult.IsValid)
           return BadRequest(validationResult.Errors);
       
       var requestedUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
       
       var command = _mapper.Map<CreateProductCommand>(request, 
           opt => opt.Items["RequestedBy"] = requestedUserId);
       
       var response = await _mediator.Send(command, cancellationToken);
       return Created(string.Empty, new ApiResponseWithData<CreateProductAPIResponse>
       {
           Success = true,
           Message = "Product created",
           Data = _mapper.Map<CreateProductAPIResponse>(response)
       });
   }
   
   [Authorize(Roles = "Admin,Manager")]
   [HttpPut]
   [Route("[action]")]
   public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
   {
       var validator = new UpdateProductRequestValidator();
       var validationResult = await validator.ValidateAsync(request, cancellationToken);
       
       if (!validationResult.IsValid)
           return BadRequest(validationResult.Errors);
       
       var requestedUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
       
       var command = _mapper.Map<UpdateProductCommand>(request, 
           opt => opt.Items["RequestedBy"] = requestedUserId);
       
       var response = await _mediator.Send(command, cancellationToken);
       return Ok( new ApiResponse
       {
           Success = true,
           Message = "Product Updated",
       });
   }
   
   [Authorize(Roles = "Admin,Manager")]
   [HttpDelete]
   [Route("[action]")]
   public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductRequest request, CancellationToken cancellationToken)
   {
       var requestedUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
       
       var command = _mapper.Map<DeleteProductCommand>(request, 
           opt => opt.Items["RequestedBy"] = requestedUserId);
       
       var response = await _mediator.Send(command, cancellationToken);
       
       return Ok( new ApiResponse
       {
           Success = true,
           Message = "Product Deleted",
       });
   }

   [Authorize(Roles = "Admin,Manager")]
   [HttpGet]
   [Route("[action]")]
   public async Task<IActionResult> GetProduct([FromBody] GetProductRequest request, CancellationToken cancellationToken)
   {
       var requestedUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
       
       var command = _mapper.Map<GetProductBranchQuery>(request, 
           opt => opt.Items["RequestedBy"] = requestedUserId);
       
       var response = await _mediator.Send(command, cancellationToken); 
       
       return Ok(new ApiResponseWithData<GetProductBranchAPIResponse>
       {
           Success = true,
           Message = "Product Retrieved",
           Data = _mapper.Map<GetProductBranchAPIResponse>(response)
       });
   }

}