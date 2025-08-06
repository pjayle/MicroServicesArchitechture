using boilerplate.web.Models;
using boilerplate.web.Models.Dto;

namespace boilerplate.web.Services
{
    public interface IRoleService
    {
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> CreateAsync(MRoles mRoles);
        Task<APIResponseDto?> GetByIdAsync(int id);
        Task<APIResponseDto?> UpdateAsync(MRoles mRoles);
        Task<APIResponseDto?> DeleteAsync(int id);
    }
    public class RoleService : IRoleService
    {
        private readonly IBaseService _baseService;

        private readonly string this_service_url = CONST.MICRO_SERVICE_API_BASE_URL + "/api/rolems/";

        public RoleService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> GetAllAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.GET,
                Url = this_service_url
            });
        }

        public async Task<APIResponseDto?> CreateAsync(MRoles mRole)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.POST,
                Data = mRole,
                Url = this_service_url
            });
        }

        public async Task<APIResponseDto?> DeleteAsync(int id)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.DELETE,
                Url = this_service_url + id
            });
        }

        public async Task<APIResponseDto?> GetByIdAsync(int id)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.GET,
                Url = this_service_url + id
            });
        }

        public async Task<APIResponseDto?> UpdateAsync(MRoles mRole)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.PUT,
                Data = mRole,
                Url = this_service_url,
                ContentType = CONST.ContentType.Json
            });
        }
    }
}
