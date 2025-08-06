using MediatR;
using service.userapi.Models;

namespace service.userapi.Data.Mediator
{
    //CQRS - COMMAND QUERY RESOURCE SEGRIGATION
    public class GetUserListQuery : IRequest<List<MUser>>
    {
    }
    public class GetUserByIdQuery : IRequest<MUser>
    {
        public int Id { get; set; }
    }
}
