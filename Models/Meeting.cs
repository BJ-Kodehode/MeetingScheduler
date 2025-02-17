using System;

namespace MeetingScheduler.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public required string Participants { get; set; }
        public DateTime MeetingTime { get; set; }
        public int DurationInMinutes { get; set; }

        // Getters & Setters
        public string GetParticipants() => Participants;
        public void SetParticipants(string participants) => Participants = participants;

        public DateTime GetMeetingTime() => MeetingTime;
        public void SetMeetingTime(DateTime meetingTime) => MeetingTime = meetingTime;

        public int GetDuration() => DurationInMinutes;
        public void SetDuration(int duration) => DurationInMinutes = duration;
    }
}
