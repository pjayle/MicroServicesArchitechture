using AutoMapper;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        private readonly IMediator _userMediator;
        private APIResponseDto _response;
        private readonly string ControllerName = "userms";

        public UsermsController(IConfiguration configuration, IMediator mediator, IMapper mapper, IMemoryCache memoryCache)
        {
            this._configuration = configuration;
            this._userMediator = mediator;
            this._mapper = mapper;
            this._memoryCache = memoryCache;
            _response = new APIResponseDto();
        }

        [HttpGet]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)] //THIS IS EXEMPLE OF ResponseCache
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
                if (_memoryCache.TryGetValue($"user_{id}", out MUser mUser))  //THIS IS EXEMPLE OF InMemory Cache
                {
                    _response.Message = "SUCCESS";
                    _response.IsSuccess = true;
                    _response.Result = mUser;
                    return Ok(_response);
                }

                _response.Result = await _userMediator.Send(new GetUserByIdQuery() { Id = id });
                _response.Message = "SUCCESS";
                _response.IsSuccess = true;

                var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1)).SetPriority(CacheItemPriority.High);

                _memoryCache.Set($"user_{id}", _response.Result, cacheOptions);
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
