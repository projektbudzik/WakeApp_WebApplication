using System;
using System.Collections.Generic;

namespace WakeApp.Model
{
    public partial class Device
    {
        public Device()
        {
            Alarm = new HashSet<Alarm>();
        }

        public int DeviceId { get; set; }
        public string Mac { get; set; }
        public string Name { get; set; }
        public string DeviceType { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Alarm> Alarm { get; set; }
    }
}
