using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WakeApp.Models
{
    public class DeviceViewModel
    {
        [Display(Name = "Nazwa urządzenia")]
        [Required(ErrorMessage = "Wprowadź nazwę urządzenia")]
        [StringLength(45, ErrorMessage = "Nazwa nie może być dłuższa niż 45 znaków")]
        public string Name { get; set; }

        [Display(Name = "Typ urządzenia")]
        public string DeviceType { get; set; }

        [Display(Name = "Użytkownik urządzenia")]
        public int UserId { get; set; }

        [Display(Name = "Numer telefonu / Adres Mac Arduino")]
        [Required(ErrorMessage = "Wprowadź numer telefonu / adres MAC Arduino")]
        public string Mac { get; set; }

        public List<SelectListItem> DeviceTypes { get; } = new List<SelectListItem>
           {
              new SelectListItem { Value = "arduino", Text = "Urządzenie Arduino" },
              new SelectListItem { Value = "telefon", Text = "Telefon" },
           };

        public List<SelectListItem> Users { get; set; }
    }
}
