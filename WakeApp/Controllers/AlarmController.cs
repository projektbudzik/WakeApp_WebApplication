using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WakeApp.Model;
using WakeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WakeApp.Controllers
{
    public class AlarmController : BaseController
    {
        public AlarmController(WakeAppContext wakeAppContext) : base(wakeAppContext)
        { }

        [HttpGet]
        public IActionResult Index([FromQuery] int? deviceId)
        {
            List<Alarm> alarms = deviceId == null ?
                 GetUserAlarms() :
                 this.wakeAppContext.Alarm
                    .Include(a => a.Device)
                    .ThenInclude(d => d.User)
                    .Where(a => a.DeviceId == deviceId)
                    .ToList();
                
            return View(alarms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AlarmViewModel()
            {
                Devices = GetDevicesSelectItems(),
            });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var alarm = this.wakeAppContext.Alarm.Find(id);

            return View(new AlarmViewModel(alarm));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var alarm = wakeAppContext.Alarm.Find(id);
            wakeAppContext.Alarm.Remove(alarm);
            wakeAppContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AlarmViewModel alarmModel, [FromRoute] int id)
        {
            // Alarm repeat validation
            if (alarmModel.Repeat)
            {
                if (
                    !alarmModel.Monday &&
                    !alarmModel.Tuesday &&
                    !alarmModel.Wednesday &&
                    !alarmModel.Thursday &&
                    !alarmModel.Friday &&
                    !alarmModel.Saturday &&
                    !alarmModel.Sunday)
                {
                    ModelState.TryAddModelError("Repeat", "Proszę wybrać dni tygodnia");
                }

                if (alarmModel.DateEnd == null)
                {
                    ModelState.TryAddModelError("DateEnd", "Proszę wybrać datę zakończenia powtórzeń alarmu");
                }
            }

            // Date validation 
            if (alarmModel.DateStart < DateTime.Today)
            {
                ModelState.TryAddModelError("DateStart", "Proszę wybrać minimum dzisiejszą datę rozpoczęcia");
            }
            if (alarmModel.DateEnd < alarmModel.DateStart)
            {
                ModelState.TryAddModelError("DateEnd", "Proszę wybrać datę zakończenia nie wcześniejszą niż data rozpoczęcia");
            }           


            if (!ModelState.IsValid)
            {
                alarmModel.Devices = GetDevicesSelectItems();
                return View(alarmModel);
            }
            var alarm = this.wakeAppContext.Alarm.Find(id);
            alarm.Comment = alarmModel.Comment;
            alarm.DateStart = alarmModel.DateStart;
            alarm.DateEnd = alarmModel.DateEnd;
            alarm.Sequence = alarmModel.Sequence;
            alarm.Time = alarmModel.Time;
          
            this.wakeAppContext.Alarm.Update(alarm);
            this.wakeAppContext.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AlarmViewModel alarmModel)
        {
            if (alarmModel.Repeat)
            {
                if (
                    !alarmModel.Monday &&
                    !alarmModel.Tuesday &&
                    !alarmModel.Wednesday &&
                    !alarmModel.Thursday &&
                    !alarmModel.Friday &&
                    !alarmModel.Saturday &&
                    !alarmModel.Sunday)
                {
                    ModelState.TryAddModelError("Repeat", "Proszę wybrać dni tygodnia");
                }

                if (alarmModel.DateEnd == null)
                {
                    ModelState.TryAddModelError("DateEnd", "Proszę wybrać datę zakończenia powtórzeń alarmu");
                }
            }

            // Date validation 
            if (alarmModel.DateStart < DateTime.Today)
            {
                ModelState.TryAddModelError("DateStart", "Proszę wybrać minimum dzisiejszą datę rozpoczęcia");
            }
            if (alarmModel.DateEnd < alarmModel.DateStart)
            {
                ModelState.TryAddModelError("DateEnd", "Proszę wybrać datę zakończenia nie wcześniejszą niż data rozpoczęcia");
            }


            if (!ModelState.IsValid)
            {
                alarmModel.Devices = GetDevicesSelectItems();
                return View(alarmModel);
            }

            var alarm = new Alarm()
            {
                AlarmId = alarmModel.AlarmId,
                DeviceId = alarmModel.DeviceId,
                DateStart = alarmModel.DateStart,
                DateEnd = alarmModel.DateEnd,
                Comment = alarmModel.Comment,
                Time = alarmModel.Time,
                Sequence = alarmModel.Sequence,
            };

            this.wakeAppContext.Alarm.Add(alarm);
            this.wakeAppContext.SaveChanges();

            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetDevicesSelectItems()
        {
            var devices = this.GetUserDevices();
            return devices.Select(d => new SelectListItem
                {
                    Value = d.DeviceId.ToString(),
                    Text = $"{d.User.Name} - {d.DeviceType}"
                })
               .ToList();
        }
    }
}
