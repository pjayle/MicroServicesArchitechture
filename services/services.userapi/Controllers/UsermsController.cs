using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using service.userapi.Data.Mediator;
using service.userapi.Models;
using service.userapi.Models.DTO;

namespace service.userapi.Controllers
{
    [Route("api/userms")]
    [ApiController]
    public class UsermsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMediator _userMediator;
        private APIResponseDto _response;
        private readonly string ControllerName = "userms";
        public UsermsController(IConfiguration configuration, IMediator mediator, IMapper mapper)
        {
            this._configuration = configuration;
            this._userMediator = mediator;
            this._mapper = mapper;
            _response = new APIResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _response.Result = await _userMediator.Send(new GetUserListQuery());
                _response.Message = "SUCCESS";
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ControllerName);
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpGet("{id:int}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _response.Result = await _userMediator.Send(new GetUserByIdQuery() { Id = id });
                _response.Message = "SUCCESS";
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ControllerName);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Post([FromBody] MUser UseraddDto)
        {
            try
            {
                MUser User = _mapper.Map<MUser>(UseraddDto);
                _response.Result = await _userMediator.Send(new CreateUserCommand(User.FullName, User.Email, User.Password, User.RolesId));
                _response.Message = "ADD SUCCESS";
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ControllerName);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPut()]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Put([FromBody] MUser UserUpdateDto)
        {
            try
            {
                MUser User = _mapper.Map<MUser>(UserUpdateDto);
                _response.Result = await _userMediator.Send(new UpdateUserCommand(User.Id, User.FullName, User.Email, User.Password, User.RolesId));
                _response.Message = "UPDATE SUCCESS";
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ControllerName);

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _response.Result = await _userMediator.Send(new DeleteUserCommand() { Id = id });
                _response.Message = "ADD SUCCESS";
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ControllerName);

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }


    }
}
