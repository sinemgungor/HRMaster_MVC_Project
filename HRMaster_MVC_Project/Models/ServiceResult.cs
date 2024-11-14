namespace HRMaster_MVC_Project.Models
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public object? Data { get; set; }
        public string RedirectUrl { get; set; }
    }
}
