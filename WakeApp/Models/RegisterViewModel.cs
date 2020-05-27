using System;
using System.ComponentModel.DataAnnotations;

namespace WakeApp.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [StringLength(45, ErrorMessage = "Nazwa użytkownika nie może być dłuższa niż 45 znaków")]
        [MinLength(3, ErrorMessage = "Nazwa użytkownika nie może być krótsza niż 3 znaki")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres Email")]
        [StringLength(45, ErrorMessage = "Email nie może być dłuższy niż 45 znaków")]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [StringLength(45, MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", 
            ErrorMessage = "Hasło nie może być krótsze niż 8 znaków i musi zawierać co najmniej 3 z 4 następujących znaków: wielkie litery (A-Z), małe litery (a-z), cyfry (0-9) i znaki specjalne (np.! @ # $% ^ & *)")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło")]
        [Required(ErrorMessage = "Potwierdzenie hasła jest wymagane")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasło i hasło potwierdzające nie są zgodne.")]
        public string ConfirmPassword { get; set; }

        public bool CreateNewGroup { get; set; } = false;

        [Display(Name = "Nazwa grupy")]
        [Required(ErrorMessage = "Nazwa grupy jest wymagana")]
        [StringLength(45, ErrorMessage = "Nazwa grupy nie może być dłuższa niż 45 znaków")]
        public string GroupName { get; set; }

        [Display(Name = "Hasło grupy")]
        [Required(ErrorMessage = "Hasło grupy jest wymagane")]
        [StringLength(45, ErrorMessage = "Hasło grupy nie może być dłuższe niż 45 znaków")]
        [MinLength(3, ErrorMessage = "Hasło grupy nie może być krótsze niż 3 znaki")]
        [DataType(DataType.Password)]
        public string GroupPassword { get; set; }

        [Display(Name = "Potwierdź hasło grupy")]
        [DataType(DataType.Password)]
        public string GroupConfirmPassword { get; set; }
    }
}
