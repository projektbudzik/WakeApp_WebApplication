using System;
using System.Collections.Generic;

namespace WakeApp.Model
{
    public partial class Alarm
    {
        public int AlarmId { get; set; }
        public DateTime DateStart { get; set; }
        public int? Sequence { get; set; }
        public DateTime? DateEnd { get; set; }
        public TimeSpan Time { get; set; }
        public string Comment { get; set; }
        public int? DeviceId { get; set; }

        public virtual Device Device { get; set; }

        public string DecodedSequence
        {
            get
            {
                if (this.Sequence == null)
                {
                    return string.Empty;
                }

                string strSequence = this.Sequence.ToString();
                strSequence = strSequence.Replace("1", "Pon|");
                strSequence = strSequence.Replace("2", "Wt|");
                strSequence = strSequence.Replace("3", "Śr|");
                strSequence = strSequence.Replace("4", "Czw|");
                strSequence = strSequence.Replace("5", "Pt|");
                strSequence = strSequence.Replace("6", "Sob|");
                strSequence = strSequence.Replace("7", "Nd|");

                strSequence = strSequence.TrimEnd('|');

                return strSequence;
            }
        }
    }
}
