using E.Application.Brands.Commands;
using E.Application.Models;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Brand;
using E.Domain.Entities.Brands.Events;
using MediatR;

namespace E.Application.Brands.CommandHandlers;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, OperationResult<Brand>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }

    public async Task<OperationResult<Brand>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Brand>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var brand = Brand.CreateBrand(request.BrandName);

            await _unitOfWork.Brands.AddAsync(brand);
            await _unitOfWork.CompleteAsync();

            var brandCreatedEvent = new BrandCreatedEvent(brand.BrandId, brand.BrandName);
            await _eventPublisher.PublishAsync(brandCreatedEvent);

            await _unitOfWork.CommitAsync();

            result.Payload = brand;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}