using MediatR;

namespace GymManagementSystem.Shared.Common.CRQS;

public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
