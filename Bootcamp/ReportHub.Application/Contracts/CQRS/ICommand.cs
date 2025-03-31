using MediatR;

namespace ReportHub.Application.Contracts.CQRS
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResposne> : IRequest<TResposne>
    {
    }
}
