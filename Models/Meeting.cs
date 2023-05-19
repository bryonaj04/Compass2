using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Compass.Models
{
	public class Meeting
	{
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime MeetingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ParticipantsId { get; set; }
        public int RoomId { get; set; }     

    }
}

