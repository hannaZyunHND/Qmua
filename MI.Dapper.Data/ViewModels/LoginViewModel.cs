using System.ComponentModel.DataAnnotations;

namespace MI.Dapper.Data.ViewModels
{
    public class LoginViewModel
    {
        [Required] public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}