using MediatR;
using service.roleapi.Models;

namespace service.roleapi.Data.Mediator
{
    //CQRS - COMMAND QUERY RESOURCE SEGRIGATION
    public class CreateRoleCommand : IRequest<Roles>
    {
        Roles _role;
        public CreateRoleCommand(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class UpdateRoleCommand : IRequest<Roles>
    {
        public UpdateRoleCommand(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class DeleteRoleCommand : IRequest<Roles>
    {
        public int Id { get; set; }
    }
}
