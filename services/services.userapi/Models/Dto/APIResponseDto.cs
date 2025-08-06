namespace service.userapi.Models.DTO
{
    public class APIResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = "";
    }
}
