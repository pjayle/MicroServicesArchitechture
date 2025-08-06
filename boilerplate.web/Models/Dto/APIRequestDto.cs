using static boilerplate.web.CONST;

namespace boilerplate.web.Models.Dto
{
    public class APIRequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public object AccessToken { get; set; }

        public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
