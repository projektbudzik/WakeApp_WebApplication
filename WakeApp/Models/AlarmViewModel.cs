using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WakeApp.Model;
using System.Linq;
using System.Collections;
using Microsoft.AspNetCore.Diagnostics;

namespace WakeApp.Models
{
    public class AlarmViewModel
    {
        public int AlarmId { get; set; }

        [Display(Name = "Nazwa urządzenia")]
        [Required(ErrorMessage = "Wybierz nazwę urządzenia")]
        public int DeviceId { get; set; }

        [Display(Name = "Data rozpoczęcia alarmu")]
        [Required(ErrorMessage = "Wprowadź datę rozpoczęcia alarmu")]
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; } = DateTime.Now;

        [Display(Name = "Data zakończenia powtórzeń alarmu")]
        [DataType(DataType.Date)]
        public DateTime? DateEnd { get; set; }

        [Display(Name = "Godzina alarmu")]
        [Required(ErrorMessage = "Wprowadź godzinę wywołania alarmu")]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        public bool Repeat { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public bool Sunday { get; set; }

        [Display(Name = "Komentarz")]
        [StringLength(255, ErrorMessage = "Komentarz nie może być dłuższy niż 255 znaków")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public List<SelectListItem> Devices { get; set; }

        public AlarmViewModel() { }

        public AlarmViewModel(Alarm alarm)
        {
            if (alarm != null)
            {
                this.AlarmId = alarm.AlarmId;
                this.DateStart = alarm.DateStart;
                this.DateEnd = alarm.DateEnd;
                this.Comment = alarm.Comment;
                this.Time = alarm.Time;
                this.Repeat = !(alarm.Sequence == null);
                if (this.Repeat)
                {
                    this.Monday = alarm.Sequence.ToString().Contains("1");
                    this.Tuesday = alarm.Sequence.ToString().Contains("2");
                    this.Wednesday = alarm.Sequence.ToString().Contains("3");
                    this.Thursday = alarm.Sequence.ToString().Contains("4");
                    this.Friday = alarm.Sequence.ToString().Contains("5");
                    this.Saturday = alarm.Sequence.ToString().Contains("6");
                    this.Sunday = alarm.Sequence.ToString().Contains("7");
                }
            }
        }

        public int? Sequence
        {
            get
            {
                int? sequence = null;
                if (Repeat)
                {
                    try
                    {
                        string strSequence = string.Empty;
                        strSequence += Monday ? "1" : "";
                        strSequence += Tuesday ? "2" : "";
                        strSequence += Wednesday ? "3" : "";
                        strSequence += Thursday ? "4" : "";
                        strSequence += Friday ? "5" : "";
                        strSequence += Saturday ? "6" : "";
                        strSequence += Sunday ? "7" : "";
                        sequence = int.Parse(strSequence);
                    }
                    catch (FormatException)
                    {
                        Console.Write("Input string was not in a correct format.");
                    }
                }
                else
                {
                    DateEnd = null;
                }

                return sequence;
            }
        }
    }
}
