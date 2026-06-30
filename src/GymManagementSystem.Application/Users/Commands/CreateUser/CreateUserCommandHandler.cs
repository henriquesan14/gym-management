using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Users;
using GymManagementSystem.Domain.Users.Contracts;
using GymManagementSystem.Domain.ValueObjects;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHash passwordHash) : ICommandHandler<CreateUserCommand, ResultT<UserResponse>>
{
    public async Task<ResultT<UserResponse>> Handle(CreateUserCommand request, CancellationToken ct)
    {
        var emailExist = await unitOfWork.Users.EmailExistsAsync(request.Email, ct);
        if (emailExist) return UserErrors.Conflict(request.Email);

        var user = new User(UserId.New(), request.Name, Email.Of(request.Email), request.Role, request.Password, passwordHash);
        await unitOfWork.Users.AddAsync(user, ct);
        await unitOfWork.CompleteAsync(ct);

        return user.ToDto(); 
    }
}
