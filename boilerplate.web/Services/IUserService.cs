using boilerplate.web.Models;
using boilerplate.web.Models.Dto;

namespace boilerplate.web.Services
{
    public interface IUserService
    {
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> CreateAsync(MUser mUser);
        Task<APIResponseDto?> GetByIdAsync(int id);
        Task<APIResponseDto?> UpdateAsync(MUser mUser);
        Task<APIResponseDto?> DeleteAsync(int id);
    }
    public class UserService : IUserService
    {
        private readonly IBaseService _baseService;

        private readonly string api_sub_url = "/api/userms/";

        public UserService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> GetAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.GET,
                Url = CONST.MICRO_SERVICE_API_BASE_URL + api_sub_url
            });
        }

        public async Task<APIResponseDto?> CreateAsync(MUser mUser)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.POST,
                Data = mUser,
                Url = CONST.MICRO_SERVICE_API_BASE_URL + api_sub_url
            });
        }

        public async Task<APIResponseDto?> DeleteAsync(int id)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.DELETE,
                Url = CONST.MICRO_SERVICE_API_BASE_URL + api_sub_url + id
            });
        }

        public async Task<APIResponseDto?> GetByIdAsync(int id)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.GET,
                Url = CONST.MICRO_SERVICE_API_BASE_URL + api_sub_url + id
            });
        }

        public async Task<APIResponseDto?> UpdateAsync(MUser mUser)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.PUT,
                Data = mUser,
                Url = CONST.MICRO_SERVICE_API_BASE_URL + api_sub_url,
                ContentType = CONST.ContentType.Json
            });
        }
    }
}
