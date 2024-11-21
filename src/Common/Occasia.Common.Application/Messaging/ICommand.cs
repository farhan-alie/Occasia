using ErrorOr;
using MediatR;

namespace Occasia.Common.Application.Messaging;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : IErrorOr;
