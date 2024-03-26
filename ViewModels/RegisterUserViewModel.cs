using System.ComponentModel.DataAnnotations;

namespace InstitueProject.ViewModels
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }

        public string Adress { get; set; }

        public string Role { get; set; }

        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [DataType(DataType.Password)]
        [Compare("PassWord")]
        public string ConfirmPassWord { get; set; }
    }
}
