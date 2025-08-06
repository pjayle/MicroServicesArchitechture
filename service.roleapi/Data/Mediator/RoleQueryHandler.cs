using MediatR;
using service.roleapi.Models;
using service.roleapi.Service;

namespace service.roleapi.Data.Mediator
{
    //CQRS - COMMAND QUERY RESOURCE SEGRIGATION
    public class GetUserListHandler : IRequestHandler<GetRoleListQuery, List<Roles>>
    {
        private readonly IRoleService _userService;
        public GetUserListHandler(IRoleService UserService)
        {
            _userService = UserService;
        }
        public async Task<List<Roles>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            return await _userService.getall();
        }
    }

    public class GetUserByIdHandler : IRequestHandler<GetRoleByIdQuery, Roles>
    {
        private readonly IRoleService _userService;
        public GetUserByIdHandler(IRoleService UserService)
        {
            _userService = UserService;
        }

        public async Task<Roles> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.getbyid(request.Id);
        }
    }
}
