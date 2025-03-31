﻿using MediatR;

namespace ReportHub.Application.Contracts.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {
    }
}
