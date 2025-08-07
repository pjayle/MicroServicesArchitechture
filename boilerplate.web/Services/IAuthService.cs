using boilerplate.web.Models;
using boilerplate.web.Models.Dto;

namespace boilerplate.web.Services
{
    public interface IAuthService
    {
        Task<APIResponseDto?> Login(LoginRequestDto mUser);
        Task<APIResponseDto> GetPermissonByRoleID(int RoleID);
        //Task<APIResponseDto?> GetAllAsync();
        //Task<APIResponseDto?> UpdateAsync(MUser mUser);
        //Task<APIResponseDto?> DeleteAsync(int id);
    }
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        private readonly string api_sub_url = "/api/auth/";

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<APIResponseDto?> Login(LoginRequestDto mUser)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.POST,
                Data = mUser,
                Url = CONST.MICRO_SERVICE_API_BASE_URL_AUTH + api_sub_url + "Login/",
                ContentType = CONST.ContentType.Json
            });
        }

        public async Task<APIResponseDto?> GetPermissonByRoleID(int id)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.GET,
                Url = CONST.MICRO_SERVICE_API_BASE_URL_AUTH + api_sub_url + "getpermissionbyroleid?roleid=" + id
            });

        }

        //public async Task<APIResponseDto?> GetAllAsync()
        //{
        //    return await _baseService.SendAsync(new APIRequestDto()
        //    {
        //        ApiType = CONST.ApiType.GET,
        //        Url = CONST.MICRO_SERVICE_API_BASE_URL + api_sub_url
        //    });
        //}

        //public async Task<APIResponseDto?> CreateAsync(MUser mUser)
        //{
        //    return await _baseService.SendAsync(new APIRequestDto()
        //    {
        //        ApiType = CONST.ApiType.POST,
        //        Data = mUser,
        //        Url = CONST.MICRO_SERVICE_API_BASE_URL + api_sub_url
        //    });
        //}

        //public async Task<APIResponseDto?> DeleteAsync(int id)
        //{
        //    return await _baseService.SendAsync(new APIRequestDto()
        //    {
        //        ApiType = CONST.ApiType.DELETE,
        //        Url = CONST.MICRO_SERVICE_API_BASE_URL + api_sub_url + id
        //    });
        //}

        //public async Task<APIResponseDto?> GetByIdAsync(int id)
        //{
        //    return await _baseService.SendAsync(new APIRequestDto()
        //    {
        //        ApiType = CONST.ApiType.GET,
        //        Url = CONST.MICRO_SERVICE_API_BASE_URL + api_sub_url + id
        //    });
        //}

        //public async Task<APIResponseDto?> UpdateAsync(MUser mUser)
        //{
        //    return await _baseService.SendAsync(new APIRequestDto()
        //    {
        //        ApiType = CONST.ApiType.PUT,
        //        Data = mUser,
        //        Url = CONST.MICRO_SERVICE_API_BASE_URL + api_sub_url,
        //        ContentType = CONST.ContentType.Json
        //    });
        //}


    }
}
