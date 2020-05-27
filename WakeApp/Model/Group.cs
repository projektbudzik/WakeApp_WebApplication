using System;
using System.Collections.Generic;

namespace WakeApp.Model
{
    public partial class Group
    {
        public Group()
        {
            User = new HashSet<User>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreateOn { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
