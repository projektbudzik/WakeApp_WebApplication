using System;
using System.Collections.Generic;

namespace WakeApp.Model
{
    public partial class User
    {
        public User()
        {
            Device = new HashSet<Device>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public DateTime CreateOn { get; set; }
        public string UserRole { get; set; }
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Device> Device { get; set; }
    }
}
