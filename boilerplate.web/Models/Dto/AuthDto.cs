namespace boilerplate.web.Models.Dto
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public UserProfile User { get; set; }
        public string Message { get; set; } = "";
        public string Token { get; set; }
    }

    public class UserProfile
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int RoleID { get; set; }
    }

}
