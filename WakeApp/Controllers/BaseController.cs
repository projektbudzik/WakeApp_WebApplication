using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WakeApp.Model;
using Microsoft.EntityFrameworkCore;


namespace WakeApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected WakeAppContext wakeAppContext;

        public BaseController(WakeAppContext wakeAppContext)
        {
            this.wakeAppContext = wakeAppContext;
        }

        protected List<Alarm> GetUserAlarms()
        {
            int groupId = int.Parse(User.FindFirstValue(ClaimTypes.GroupSid));
            List<Alarm> alarms = null;
            if (User.IsInRole("SuperUser"))
            {
                alarms = wakeAppContext
                    .Alarm
                    .Include(a => a.Device)
                    .ThenInclude(d => d.User)
                    .Where(a => a.Device.User.GroupId == groupId)
                    .ToList();
            }
            else
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                alarms = wakeAppContext
                    .Alarm
                    .Include(a => a.Device)
                    .ThenInclude(d => d.User)
                    .Where(a => a.Device.User.UserId == userId)
                    .ToList();
            }

            return alarms;
        }

        protected List<Device> GetUserDevices()
        {
            int groupId = int.Parse(User.FindFirstValue(ClaimTypes.GroupSid));

            List<Device> devices = null;
            if (User.IsInRole("SuperUser"))
            {
                devices = wakeAppContext
                    .Device
                    .Include(d => d.User)
                    .Where(d => d.User.GroupId == groupId)
                    .ToList();
            }
            else
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                devices = wakeAppContext
                    .Device
                    .Include(d => d.User)
                    .Where(d => d.UserId == userId)
                    .ToList();
            }
            return devices;
        }

        protected List<User> GetGroupUsers()
        {
            int groupId = int.Parse(User.FindFirstValue(ClaimTypes.GroupSid));
            return this.wakeAppContext
                .User
                .Where(u => u.GroupId == groupId)
                .ToList();
        }
    }
}
