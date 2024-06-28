﻿using E.Application.Models;
using E.Domain.Entities;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Products.Commands;

public class RemoveProductCommand: BaseEntity, IRequest<OperationResult<Product>>
{
}