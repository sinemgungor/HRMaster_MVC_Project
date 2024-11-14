namespace HRMaster_MVC_Project.Models
{
    public class ApiResponse<T>
    {
        
            public bool Success { get; set; }
            public string Message { get; set; }
            public List<string> Errors { get; set; }
            public T Data { get; set; }
            public string RedirectUrl { get; set; }
        
    }
}
