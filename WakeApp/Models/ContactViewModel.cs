using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WakeApp.Models
{
    public class ContactViewModel
    {
        [Display(Name = "Imię *")]
        [Required(ErrorMessage = "Imię jest wymagane")]
        [StringLength(45, ErrorMessage = "Imię nie może być dłuższe niż 45 znaków")]
        [MinLength(3, ErrorMessage = "Imię nie może być krótsze niż 3 znaki")]
        public string Name { get; set; }

        [Display(Name = "Email *")]
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres Email")]
        [StringLength(45, ErrorMessage = "Email nie może być dłuższy niż 45 znaków")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        [Phone(ErrorMessage = "Niepoprawny numer telefonu")]
        public string Phone { get; set; }

        [Display(Name = "Temat *")]
        [Required(ErrorMessage = "Temat jest wymagany")]
        [StringLength(45, ErrorMessage = "Imię nie może być dłuższe niż 45 znaków")]
        public string Subject { get; set; }

        [Display(Name = "Treść wiadomości *")]
        [Required(ErrorMessage = "Treść wiadomości jest wymagana")]
        [StringLength(1000, ErrorMessage = "Wiadomość nie może być dłuższa niż 1000 znaków")]
        public string Message { get; set; }
    }

}
