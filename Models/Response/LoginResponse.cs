namespace OnlineCasino.Models.Response
{
    public class LoginResponse
    {
        public string? Token { get; set; }

        public string? Message { get; set; }

        public string? Status { get; set; }

        public DateTime Expiration { get; set; }

    }

    public enum Status
    {
        Success,
        Failure
    }
}
