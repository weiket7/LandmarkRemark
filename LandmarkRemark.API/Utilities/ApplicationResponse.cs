namespace LandmarkRemark.API.Utilities
{
    public class ApplicationResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public object Data { get; set; }

        public static ApplicationResponse Error(string message, object data = null)
        {
            return new ApplicationResponse
            {
                Status = "Error",
                Message = message,
                Data = data
            };
        }
    }
}
