using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using service.roleapi.Data.Mediator;
using service.roleapi.Models;
using service.roleapi.Models.DTO;
using System.Data;

namespace service.roleapi.Controllers
{
    [Route("api/rolems")]
    [ApiController]
    public class RolemsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMediator _roleMediator;
        private APIResponseDto _response;
        private readonly string ControllerName = "rolems";
        public RolemsController(IConfiguration configuration, IMediator mediator, IMapper mapper)
        {
            this._configuration = configuration;
            this._roleMediator = mediator;
            this._mapper = mapper;
            _response = new APIResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _response.Result = await _roleMediator.Send(new GetRoleListQuery());
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
                _response.Result = await _roleMediator.Send(new GetRoleByIdQuery() { Id = id });
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
        public async Task<IActionResult> Post([FromBody] Roles RoleaddDto)
        {
            try
            {
                Roles mRoles = _mapper.Map<Roles>(RoleaddDto);
                _response.Result = await _roleMediator.Send(new CreateRoleCommand(mRoles.Title, mRoles.Description));
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
        public async Task<IActionResult> Put([FromBody] Roles RoleUpdateDto)
        {
            try
            {
                Roles mRoles = _mapper.Map<Roles>(RoleUpdateDto);
                _response.Result = await _roleMediator.Send(new UpdateRoleCommand(mRoles.Id, mRoles.Title, mRoles.Description));
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
                _response.Result = await _roleMediator.Send(new DeleteRoleCommand() { Id = id });
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
