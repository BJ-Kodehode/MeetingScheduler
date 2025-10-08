# API Dokumentasjon - MeetingScheduler

## Oversikt

Denne dokumentasjonen beskriver de interne API-ene og klassene i MeetingScheduler-applikasjonen.

## Modeller

### Meeting

Representerer et møte i systemet.

```csharp
public class Meeting
{
    public int Id { get; set; }
    public string Participants { get; set; } = "";
    public DateTime MeetingTime { get; set; }
    public int DurationInMinutes { get; set; }
    public DateTime EndTime => MeetingTime.AddMinutes(DurationInMinutes);
}
```

#### Egenskaper

| Egenskap | Type | Beskrivelse |
|----------|------|-------------|
| `Id` | `int` | Unik identifikator for møtet (auto-generert) |
| `Participants` | `string` | Kommaseparert liste over deltakere |
| `MeetingTime` | `DateTime` | Starttidspunkt for møtet |
| `DurationInMinutes` | `int` | Møtets varighet i minutter |
| `EndTime` | `DateTime` | Beregnet sluttidspunkt (read-only) |

#### Eksempel

```csharp
var meeting = new Meeting
{
    Participants = "John Doe, Jane Smith",
    MeetingTime = DateTime.Parse("2025-10-08 14:30"),
    DurationInMinutes = 60
};
// EndTime vil automatisk være 2025-10-08 15:30
```

## Controllers

### MeetingController

Håndterer all forretningslogikk og database-operasjoner for møter.

```csharp
public class MeetingController
{
    public MeetingController()
    public void AddMeeting(Meeting meeting)
    public List<Meeting> GetMeetings()
}
```

#### Konstruktør

```csharp
public MeetingController()
```

Initialiserer kontrolleren og oppretter database hvis den ikke eksisterer.

#### Metoder

##### AddMeeting

```csharp
public void AddMeeting(Meeting meeting)
```

Legger til et nytt møte i databasen.

**Parametere:**
- `meeting` - Møteobjekt som skal lagres

**Eksempel:**
```csharp
var controller = new MeetingController();
var meeting = new Meeting
{
    Participants = "Alice, Bob",
    MeetingTime = DateTime.Now.AddDays(1),
    DurationInMinutes = 30
};
controller.AddMeeting(meeting);
```

##### GetMeetings

```csharp
public List<Meeting> GetMeetings()
```

Henter alle møter fra databasen.

**Returverdi:**
- `List<Meeting>` - Liste over alle lagrede møter

**Eksempel:**
```csharp
var controller = new MeetingController();
var meetings = controller.GetMeetings();
foreach (var meeting in meetings)
{
    Console.WriteLine($"Møte: {meeting.Participants} på {meeting.MeetingTime}");
}
```

## Database-skjema

### Meetings-tabell

```sql
CREATE TABLE IF NOT EXISTS Meetings (
    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
    Participants TEXT NOT NULL, 
    MeetingTime TEXT NOT NULL,
    DurationInMinutes INTEGER NOT NULL,
    EndTime TEXT NOT NULL
);
```

#### Kolonner

| Kolonne | Type | Beskrivelse |
|---------|------|-------------|
| `Id` | `INTEGER` | Primærnøkkel, auto-increment |
| `Participants` | `TEXT` | Deltakere (kommaseparert) |
| `MeetingTime` | `TEXT` | Starttid (ISO format) |
| `DurationInMinutes` | `INTEGER` | Varighet i minutter |
| `EndTime` | `TEXT` | Sluttid (ISO format) |

## Feilhåndtering

### Database-feil

Hvis database-operasjoner feiler, vil applikasjonen kaste en `SQLiteException`. Dette håndteres ikke eksplisitt i nåværende implementasjon.

### Validering

Applikasjonen validerer:
- Datoformat ved parsing
- Numerisk input for varighet
- Menyvalg

### Anbefalte forbedringer

1. **Explicit exception handling** i MeetingController
2. **Input validation** i Meeting-modellen
3. **Async/await** for database-operasjoner
4. **Logging** av feil og operasjoner

## Avhengigheter

### System.Data.SQLite (1.0.119)

Brukes for SQLite database-operasjoner:
- `SQLiteConnection` - Database-tilkobling
- `SQLiteCommand` - SQL-kommandoer
- `SQLiteDataReader` - Lesing av resultater

### Newtonsoft.Json (13.0.3)

Brukes for JSON-serialisering (for fremtidig funksjonalitet):
- `JsonConvert.SerializeObject()` - Serialisering til JSON
- `JsonConvert.DeserializeObject()` - Deserialisering fra JSON