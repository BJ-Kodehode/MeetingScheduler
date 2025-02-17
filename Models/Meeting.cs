public class Meeting
{
    public int Id { get; set; }
    public string Participants { get; set; } = "";
    public DateTime MeetingTime { get; set; }
    public int DurationInMinutes { get; set; }

    // Automatisk beregnet sluttid (trenger ikke lagres i databasen)
    public DateTime EndTime => MeetingTime.AddMinutes(DurationInMinutes);
}
