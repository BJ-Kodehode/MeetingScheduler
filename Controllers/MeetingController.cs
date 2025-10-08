// Importerer nødvendige namespaces
// using System; - Ikke nødvendig med implicit usings i .NET 9
// using System.Collections.Generic; - Ikke nødvendig med implicit usings
using System.Data.SQLite; // For SQLite database-operasjoner
// using System.IO; - Ikke nødvendig med implicit usings
using Newtonsoft.Json; // For JSON-serialisering
// using MeetingScheduler.Models; - Ikke nødvendig da Meeting er globally available

namespace MeetingScheduler.Controllers
{
    /// <summary>
    /// Kontroller-klasse som håndterer all forretningslogikk og database-operasjoner for møter
    /// Implementerer CRUD-operasjoner (Create, Read, Update, Delete) for Meeting-objekter
    /// </summary>
    public class MeetingController
    {
        // Database-tilkoblingsstreng for SQLite-databasen
        private readonly string _dbPath = "Data Source=meetings.db";

        /// <summary>
        /// Konstruktør som initialiserer kontrolleren
        /// Kaller InitializeDatabase() for å sikre at database og tabeller eksisterer
        /// </summary>
        public MeetingController()
        {
            InitializeDatabase();
        }

        /// <summary>
        /// Initialiserer SQLite-databasen og oppretter Meetings-tabellen hvis den ikke eksisterer
        /// Kalles automatisk ved opprettelse av MeetingController-objekter
        /// </summary>
        private void InitializeDatabase()
        {
            // Oppretter tilkobling til SQLite-databasen (databasefilen opprettes automatisk hvis den ikke finnes)
            using (var connection = new SQLiteConnection(_dbPath))
            {
                // Åpner tilkoblingen til databasen
                connection.Open();
                
                // SQL-kommando for å opprette Meetings-tabell med alle nødvendige kolonner
                string tableCommand = @"
        CREATE TABLE IF NOT EXISTS Meetings (
            Id INTEGER PRIMARY KEY AUTOINCREMENT, 
            Participants TEXT NOT NULL, 
            MeetingTime TEXT NOT NULL,
            DurationInMinutes INTEGER NOT NULL,
            EndTime TEXT NOT NULL
        );";
                
                // Utfører SQL-kommandoen for å opprette tabellen
                using (var cmd = new SQLiteCommand(tableCommand, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Legger til et nytt møte i SQLite-databasen
        /// Konverterer Meeting-objekt til database-format og utfører INSERT-operasjon
        /// </summary>
        /// <param name="meeting">Meeting-objektet som skal lagres i databasen</param>
        public void AddMeeting(Meeting meeting)
        {
            // Oppretter ny tilkobling til databasen
            using (var connection = new SQLiteConnection(_dbPath))
            {
                // Åpner tilkoblingen
                connection.Open();
                
                // SQL INSERT-kommando med parameterized queries for å forhindre SQL-injection
                string insertCommand = "INSERT INTO Meetings (Participants, MeetingTime, DurationInMinutes, EndTime) VALUES (@Participants, @MeetingTime, @DurationInMinutes, @EndTime);";
                
                // Oppretter SQLite-kommando med parametere
                using (var cmd = new SQLiteCommand(insertCommand, connection))
                {
                    // Legger til parametere med sikre verdier fra Meeting-objektet
                    cmd.Parameters.AddWithValue("@Participants", meeting.Participants);
                    cmd.Parameters.AddWithValue("@MeetingTime", meeting.MeetingTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@DurationInMinutes", meeting.DurationInMinutes);
                    cmd.Parameters.AddWithValue("@EndTime", meeting.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    
                    // Utfører INSERT-kommandoen
                    cmd.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Henter alle møter fra SQLite-databasen og konverterer dem til Meeting-objekter
        /// </summary>
        /// <returns>Liste med alle Meeting-objekter fra databasen</returns>
        public List<Meeting> GetMeetings()
        {
            // Initialiserer tom liste for å holde møte-objektene
            List<Meeting> meetings = new List<Meeting>();
            
            // Oppretter tilkobling til databasen
            using (var connection = new SQLiteConnection(_dbPath))
            {
                // Åpner tilkoblingen
                connection.Open();
                
                // SQL SELECT-kommando for å hente alle møter
                string selectCommand = "SELECT * FROM Meetings;";
                
                // Oppretter kommando og DataReader for å lese resultater
                using (var cmd = new SQLiteCommand(selectCommand, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    // Itererer gjennom alle rader i resultatet
                    while (reader.Read())
                    {
                        // Konverterer database-verdier til riktige datatyper
                        DateTime meetingTime = DateTime.Parse(reader.GetString(2)); // Kolonne 2: MeetingTime
                        int duration = reader.GetInt32(3); // Kolonne 3: DurationInMinutes
                        DateTime endTime = DateTime.Parse(reader.GetString(4)); // Kolonne 4: EndTime

                        // Oppretter nytt Meeting-objekt og legger til i listen
                        meetings.Add(new Meeting
                        {
                            Id = reader.GetInt32(0), // Kolonne 0: Id
                            Participants = reader.GetString(1), // Kolonne 1: Participants
                            MeetingTime = meetingTime,
                            DurationInMinutes = duration,
                        });
                    }
                }
            }
            
            // Returnerer listen med alle møter
            return meetings;
        }



        /// <summary>
        /// Lagrer møter til en ekstern JSON-fil som backup eller for eksport
        /// MERK: Denne metoden er privat og kalles ikke i nåværende implementasjon
        /// Kan brukes for fremtidig funksjonalitet som eksport eller backup
        /// </summary>
        /// <param name="meeting">Meeting-objektet som skal lagres til JSON</param>
        private void SaveMeetingToJson(Meeting meeting)
        {
            // Definerer filbanen for JSON-filen
            string filePath = "meetings.json";
            List<Meeting> meetings = new List<Meeting>();

            // Sjekker om JSON-filen allerede eksisterer
            if (File.Exists(filePath))
            {
                // Leser eksisterende data fra JSON-filen
                string existingData = File.ReadAllText(filePath);
                // Deserialiserer JSON til Meeting-objekter (eller oppretter tom liste hvis deserialisering feiler)
                meetings = JsonConvert.DeserializeObject<List<Meeting>>(existingData) ?? new List<Meeting>();
            }

            // Legger til det nye møtet i listen
            meetings.Add(meeting);
            
            // Serialiserer listen til JSON og skriver til fil med formatering
            File.WriteAllText(filePath, JsonConvert.SerializeObject(meetings, Formatting.Indented));
        }
    }
}
