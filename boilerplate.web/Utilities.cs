namespace boilerplate.web
{
    public class CONST
    {
        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
        public enum ContentType
        {
            Json, MultipartFormData
        }
        public static string MICRO_SERVICE_API_BASE_URL_AUTH { get; set; }
        public static string MICRO_SERVICE_API_BASE_URL { get; set; }

        public const string TOKEN_COOKIE_KEY = "JWTToken";


        //public static string ProductAPIBase { get; set; }
        //public static string OrderAPIBase { get; set; }
        //public static string AuthAPIBase { get; set; }

        //public const string RoleSuperAdmin = "SUPERADMIN";
        //public const string RoleAdmin = "ADMIN";
        //public const string RoleSupervisor = "SUPERVISOR";
        //public const string RoleOperator = "OPERATOR";


        //public const string ORDER_Status_Created = "Created";
        //public const string ORDER_Status_Pending = "Pending";
        //public const string ORDER_Status_Approved = "Approved";
        //public const string ORDER_Status_ReadyForPickup = "ReadyForPickup";
        //public const string ORDER_Status_Completed = "Completed";
        //public const string ORDER_Status_Refunded = "Refunded";
        //public const string ORDER_Status_Cancelled = "Cancelled";
    }
}
