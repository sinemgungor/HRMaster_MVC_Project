namespace HRMaster_MVC_Project.Models.ViewModels.LoginViewModel
{
    public class LoginResult
    {
        public UserDTO User { get; set; }
        public List<string> Roles { get; set; }
    }
}
