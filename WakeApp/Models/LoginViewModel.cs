using System;
using System.ComponentModel.DataAnnotations;

namespace WakeApp.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres Email")]
        [StringLength(45, ErrorMessage = "Email nie może być dłuższy niż 45 znaków")]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [StringLength(45, ErrorMessage = "Hasło nie może być dłuższe niż 45 znaków")]
        [MinLength(8, ErrorMessage = "Hasło nie może być krótsze niż 8 znaki")]
        public string Password { get; set; }
    }
}
