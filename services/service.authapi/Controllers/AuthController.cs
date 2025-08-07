
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using service.authapi.Models.DTO;
using service.authapi.Service;

namespace service.authapi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        protected APIResponseDto _response;
        public AuthController(IConfiguration configuration, IAuthService authService, IMapper mapper)
        {
            _configuration = configuration;
            _authService = authService;
            _response = new APIResponseDto();
            _mapper = mapper;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            _response.Result = await _authService.Login(model);
            _response.Message = "SUCCESS";
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpGet("getpermissionbyroleid")]
        public async Task<IActionResult> GetPermissonByRoleID(int roleid)
        {
            _response.Result = await _authService.GetPermissonByRoleID(roleid);
            _response.Message = "SUCCESS";
            _response.IsSuccess = true;
            return Ok(_response);
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        //{
        //    var errorMessage = await _authService.Register(model);
        //    if (!string.IsNullOrEmpty(errorMessage))
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = errorMessage;
        //        return BadRequest(_response);
        //    }
        //    return Ok(_response);
        //}

        //[HttpPost("AssignRole")]
        //public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        //{
        //    var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
        //    if (!assignRoleSuccessful)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = "Error encountered";
        //        return BadRequest(_response);
        //    }
        //    return Ok(_response);
        //}
    }
}
