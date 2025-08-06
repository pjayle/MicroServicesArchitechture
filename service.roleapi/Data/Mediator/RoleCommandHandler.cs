using MediatR;
using service.roleapi.Models;
using service.roleapi.Service;

namespace service.roleapi.Data.Mediator
{
    //CQRS - COMMAND QUERY RESOURCE SEGRIGATION
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Roles>
    {
        private readonly IRoleService _roleService;
        public CreateRoleHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<Roles> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            Roles mRoles = new Roles
            {
                Title = request.Title,
                Description = request.Description,
            };
            return await _roleService.add(mRoles);
        }
    }
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Roles>
    {
        private readonly IRoleService _roleService;
        public UpdateRoleHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Roles> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            Roles mRole = await _roleService.getbyid(request.Id);
            mRole.Title = request.Title;
            mRole.Description = request.Description;

            return await _roleService.update(mRole);
        }
    }
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, Roles>
    {
        private readonly IRoleService _roleService;
        public DeleteRoleHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<Roles> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleService.delete(request.Id);
        }
    }
}
