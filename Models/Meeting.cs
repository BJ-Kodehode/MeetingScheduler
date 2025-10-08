/// <summary>
/// Datamodell som representerer et møte i systemet
/// Inneholder all nødvendig informasjon om et møte
/// </summary>
public class Meeting
{
    /// <summary>
    /// Unik identifikator for møtet, settes automatisk av databasen
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Kommaseparert liste over møtedeltakere
    /// </summary>
    public string Participants { get; set; } = "";
    
    /// <summary>
    /// Tidspunkt når møtet starter
    /// </summary>
    public DateTime MeetingTime { get; set; }
    
    /// <summary>
    /// Møtets varighet oppgitt i minutter
    /// </summary>
    public int DurationInMinutes { get; set; }

    /// <summary>
    /// Automatisk beregnet sluttid basert på starttid og varighet
    /// Denne egenskapen beregnes dynamisk og lagres ikke i databasen
    /// </summary>
    public DateTime EndTime => MeetingTime.AddMinutes(DurationInMinutes);
}
