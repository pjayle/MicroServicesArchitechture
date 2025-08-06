using MediatR;
using Microsoft.AspNetCore.Identity;
using service.userapi.Models;

namespace service.userapi.Data.Mediator
{
    //CQRS - COMMAND QUERY RESOURCE SEGRIGATION
    public class CreateUserCommand : IRequest<MUser>
    {
        MUser _user;
        public CreateUserCommand(string fullname, string email, string password, int roleid)
        {
            FullName = fullname;
            Email = email;
            Password = password;
            Roleid = roleid;
        }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public int? RolesId { get; set; }
        public int Roleid { get; }
    }
    public class UpdateUserCommand : IRequest<MUser>
    {
        public UpdateUserCommand(int id, string fullname, string email, string password, int roleid)
        {
            Id = id;
            FullName = fullname;
            Email = email;
            Password = password;
            Roleid = roleid;
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RolesId { get; set; }
        public int Roleid { get; }
    }
    public class DeleteUserCommand : IRequest<MUser>
    {
        public int Id { get; set; }
    }
}
