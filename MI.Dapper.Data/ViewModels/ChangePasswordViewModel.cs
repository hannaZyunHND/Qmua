namespace MI.Dapper.Data.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}