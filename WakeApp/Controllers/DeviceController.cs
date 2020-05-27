using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WakeApp.Model;
using WakeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WakeApp.Controllers
{
    [Authorize]
    public class DeviceController : BaseController
    {
        public DeviceController(WakeAppContext wakeAppContext) : base(wakeAppContext)
        { }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Device> devices = GetUserDevices();
            return View(devices);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userItems = GetGroupUsers()
                .Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.UserId.ToString(),
                })
                .ToList();


            return View(new DeviceViewModel() { Users = userItems });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var device = wakeAppContext.Device.Find(id);
            wakeAppContext.Device.Remove(device);
            wakeAppContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeviceViewModel deviceModel)
        {
            if (!ModelState.IsValid)
            {
                return View(deviceModel);
            }

            var userId = User.IsInRole("SuperUser") ?
                deviceModel.UserId :
                int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var device = new Device()
            {
                Name = deviceModel.Name,
                Mac = deviceModel.Mac,
                DeviceType = deviceModel.DeviceType,
                UserId = userId,
            };

            wakeAppContext.Device.Add(device);
            wakeAppContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
