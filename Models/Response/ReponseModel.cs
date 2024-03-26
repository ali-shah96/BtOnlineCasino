namespace OnlineCasino.Models.Response
{
    public class ReponseModel
    {
        public string Message { get; set; }
        public Action Action { get; set; }
    }

    public enum Action
    {
        Created,
        Updated,
        Deleted,
        Error
    }
}
