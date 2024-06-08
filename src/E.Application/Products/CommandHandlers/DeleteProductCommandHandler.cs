using AutoMapper;
using E.Application.Enums;
using E.Application.Models;
using E.Application.Products.Commands;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Products.CommandHandlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, OperationResult<Product>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<OperationResult<Product>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Product>();
        try
        {
            var product = await _unitOfWork.Products.FirstOrDefaultAsync(p => p.ProductId == request.ProductId);
            if (product is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ProductErrorMessage.ProductNotFound, request.ProductId));
                return result;
            }
            if(product.ProductId != request.ProductId)
            {
                result.AddError(ErrorCode.PostDeleteNotPossible, ProductErrorMessage.ProductDeleteNotPossible);
                return result;
            }
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.CompleteAsync();

            await _eventPublisher.PublishAsync(product);
            result.Payload = product;
        }catch (Exception ex)
        {
            result.AddUnknownError(ex.Message);
        }
        return result;
    }
}