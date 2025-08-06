using MediatR;
using service.userapi.Models;
using service.userapi.Service;

namespace service.userapi.Data.Mediator
{
    //CQRS - COMMAND QUERY RESOURCE SEGRIGATION
    public class GetUserListHandler : IRequestHandler<GetUserListQuery, List<MUser>>
    {
        private readonly IUserService _userService;
        public GetUserListHandler(IUserService UserService)
        {
            _userService = UserService;
        }
        public async Task<List<MUser>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return await _userService.getall();
        }
    }

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, MUser>
    {
        private readonly IUserService _userService;
        public GetUserByIdHandler(IUserService UserService)
        {
            _userService = UserService;
        }

        public async Task<MUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.getbyid(request.Id);
        }
    }
}
