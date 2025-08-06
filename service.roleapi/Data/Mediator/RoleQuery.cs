using MediatR;
using service.roleapi.Models;

namespace service.roleapi.Data.Mediator
{
    //CQRS - COMMAND QUERY RESOURCE SEGRIGATION
    public class GetRoleListQuery : IRequest<List<Roles>>
    {
    }
    public class GetRoleByIdQuery : IRequest<Roles>
    {
        public int Id { get; set; }
    }
}
