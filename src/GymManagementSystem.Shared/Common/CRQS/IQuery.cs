using MediatR;

namespace GymManagementSystem.Shared.Common.CRQS;

public interface IQuery<out TResponse> : IRequest<TResponse>
where TResponse : notnull
{
}
