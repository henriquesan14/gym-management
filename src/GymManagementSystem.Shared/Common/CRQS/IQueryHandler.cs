using MediatR;

namespace GymManagementSystem.Shared.Common.CRQS;

public interface IQueryHandler<in TQuery, TResponse>
: IRequestHandler<TQuery, TResponse>
where TQuery : IQuery<TResponse>
where TResponse : notnull
{
}
