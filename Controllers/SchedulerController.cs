using System;
using System.Globalization;
using Compass.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace Compass.Controllers
{
    public class SchedulerController : Controller
    {
        private readonly MeetingController _meetingController;
        private readonly PersonController _personController;
        private readonly RoomController _roomController;

        public SchedulerController(MeetingController meetingController, PersonController personController, RoomController roomController)
        {
            _meetingController = meetingController;
            _personController = personController;
            _roomController = roomController;
        }


        [HttpPost]
        public async Task<IActionResult> ScheduleMeeting(Meeting model)
        {
            TempData.Clear();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(errors);

            }

            if (await CheckPersonExists(model.ParticipantsId) && await CheckRoomExists(model.RoomId) && await CheckMeetingExists(model) && CheckMeetingDateAndTime(model))
            {
                try
                {
                    await _meetingController.Create(model);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException(e.ToString());
                }
                finally
                {
                    TempData["Message"] = "Meeting booked successfully.";

                }
            }


            return RedirectToAction("Index", "Home");
        }

        private async Task<bool> CheckPersonExists(int participantsId)
        {
            var result = true;
            {
                var people = await _personController.GetPeople();
                var filteredPeople = people.Where(x => x.Id == participantsId);

                if (filteredPeople == null)
                {
                    TempData["Message"] = "selected Person is not valid";
                    return result = false; ;

                }
            }
            return result;
        }

        private async Task<bool> CheckRoomExists(int roomId)
        {
            var result = true;
            {
                var rooms = await _roomController.GetRooms();
                var filteredRooms = rooms.Where(x => x.Id == roomId);

                if (filteredRooms == null)
                {
                    TempData["Message"] = "selected Rooms is not valid.";
                    return result = false; 

                }
            }
            return result;
        }

        private async Task<bool> CheckMeetingExists(Meeting model)
        {
            var result = true;
            if (model != null)
            {
                var meetings = await _meetingController.GetMeetings();

                var bookedMeetings = meetings.Where(x => x.ParticipantsId == model.ParticipantsId && x.MeetingDate == model.MeetingDate && x.StartTime == model.StartTime && x.RoomId == model.RoomId);

                if (bookedMeetings.Count() >= 1)
                {
                    TempData["Message"] = "Meeting is already booked.";
                    return result = false;

                }
            }

            return result;
        }

        private bool CheckMeetingDateAndTime(Meeting model)
        {
            var result = true;
            var today = DateTime.Now;
            var todayTime = DateTime.Now.TimeOfDay;

            if (model.MeetingDate <= today)
            {
                TempData["Message"] = "Meeting date is in the pass please pick a future date.";
                return result = false;
            }

            if (model.StartTime.TimeOfDay  < todayTime)
            {
                TempData["Message"] = "Meeting Time is in the pass please pick a future Time.";
            }

            return result;
        }


    }
}

