using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Newtonsoft.Json;
using MeetingScheduler.Models;

namespace MeetingScheduler.Controllers
{
    public class MeetingController
    {
        private readonly string _dbPath = "Data Source=meetings.db";

        public MeetingController()
        {
            InitializeDatabase();
        }

        // Oppretter database og tabell hvis den ikke finnes
        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_dbPath))
            {
                connection.Open();
                string tableCommand = @"
        CREATE TABLE IF NOT EXISTS Meetings (
            Id INTEGER PRIMARY KEY AUTOINCREMENT, 
            Participants TEXT NOT NULL, 
            MeetingTime TEXT NOT NULL,
            DurationInMinutes INTEGER NOT NULL,
            EndTime TEXT NOT NULL
        );";
                using (var cmd = new SQLiteCommand(tableCommand, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }



        // Legger til møte i databasen
        public void AddMeeting(Meeting meeting)
        {
            using (var connection = new SQLiteConnection(_dbPath))
            {
                connection.Open();
                string insertCommand = "INSERT INTO Meetings (Participants, MeetingTime, DurationInMinutes, EndTime) VALUES (@Participants, @MeetingTime, @DurationInMinutes, @EndTime);";
                using (var cmd = new SQLiteCommand(insertCommand, connection))
                {
                    cmd.Parameters.AddWithValue("@Participants", meeting.Participants);
                    cmd.Parameters.AddWithValue("@MeetingTime", meeting.MeetingTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@DurationInMinutes", meeting.DurationInMinutes);
                    cmd.Parameters.AddWithValue("@EndTime", meeting.EndTime.ToString("yyyy-MM-dd HH:mm:ss")); // Lagre sluttid
                    cmd.ExecuteNonQuery();
                }
            }
        }



        // Leser møter fra databasen
        public List<Meeting> GetMeetings()
        {
            List<Meeting> meetings = new List<Meeting>();
            using (var connection = new SQLiteConnection(_dbPath))
            {
                connection.Open();
                string selectCommand = "SELECT * FROM Meetings;";
                using (var cmd = new SQLiteCommand(selectCommand, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime meetingTime = DateTime.Parse(reader.GetString(2));
                        int duration = reader.GetInt32(3);
                        DateTime endTime = DateTime.Parse(reader.GetString(4));

                        meetings.Add(new Meeting
                        {
                            Id = reader.GetInt32(0),
                            Participants = reader.GetString(1),
                            MeetingTime = meetingTime,
                            DurationInMinutes = duration,
                        });
                    }
                }
            }
            return meetings;
        }



        // Logger møter til en ekstern JSON-fil
        private void SaveMeetingToJson(Meeting meeting)
        {
            string filePath = "meetings.json";
            List<Meeting> meetings = new List<Meeting>();

            if (File.Exists(filePath))
            {
                string existingData = File.ReadAllText(filePath);
                meetings = JsonConvert.DeserializeObject<List<Meeting>>(existingData) ?? new List<Meeting>();
            }

            meetings.Add(meeting);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(meetings, Formatting.Indented));
        }
    }
}
