using ErrorOr;
using MediatR;

namespace Occasia.Common.Application.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : IErrorOr;
