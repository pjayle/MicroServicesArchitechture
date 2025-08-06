using MediatR;
using service.userapi.Models;
using service.userapi.Service;

namespace service.userapi.Data.Mediator
{
    //CQRS - COMMAND QUERY RESOURCE SEGRIGATION
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, MUser>
    {
        private readonly IUserService _userService;
        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<MUser> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            MUser user = new MUser
            {
                Email = request.Email,
                FullName = request.FullName,
                RolesId = request.Roleid,
                Password = request.Password
            };
            return await _userService.add(user);
        }
    }
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, MUser>
    {
        private readonly IUserService _userService;
        public UpdateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<MUser> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            MUser mUser = await _userService.getbyid(request.Id);
            mUser.Email = request.Email;
            mUser.FullName = request.FullName;
            mUser.RolesId = request.Roleid;
            mUser.Password = request.Password;

            return await _userService.update(mUser);
        }
    }
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, MUser>
    {
        private readonly IUserService _userService;
        public DeleteUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<MUser> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.delete(request.Id);
        }
    }
}
