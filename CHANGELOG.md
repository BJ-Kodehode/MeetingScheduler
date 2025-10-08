# CHANGELOG

Alle viktige endringer i dette prosjektet vil bli dokumentert i denne filen.

Formatet er basert på [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
og dette prosjektet følger [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Upublisert]

### Lagt til
- Ingen

### Endret
- Ingen

### Fjernet
- Ingen

### Fikset
- Ingen

## [1.0.0] - 2025-10-08

### Lagt til
- Grunnleggende konsollapplikasjon for møteplanlegging
- Mulighet til å registrere nye møter med deltakere, tidspunkt og varighet
- Visning av alle registrerte møter
- SQLite database for persistent lagring av møter
- Automatisk beregning av møtets sluttid
- Validering av dato- og varighetsformat
- Enkel meny-drevet brukergrensesnitt på norsk
- Database-initialisering ved første kjøring
- JSON-serialisering støtte (Newtonsoft.Json)
- .NET 9.0 støtte med nullable reference types

### Tekniske detaljer
- Meeting-modell med Id, Participants, MeetingTime, DurationInMinutes og EndTime
- MeetingController for forretningslogikk og database-operasjoner  
- Program.cs med hovedloop og brukergrensesnitt
- SQLite database med Meetings-tabell
- NuGet-pakker: System.Data.SQLite (1.0.119) og Newtonsoft.Json (13.0.3)

### Database skjema
```sql
CREATE TABLE IF NOT EXISTS Meetings (
    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
    Participants TEXT NOT NULL, 
    MeetingTime TEXT NOT NULL,
    DurationInMinutes INTEGER NOT NULL,
    EndTime TEXT NOT NULL
);
```

### Filstruktur
```
MeetingScheduler/
├── Program.cs
├── Models/Meeting.cs
├── Controllers/MeetingController.cs
├── MeetingScheduler.csproj
├── MeetingScheduler.sln
└── meetings.db (genereres automatisk)
```

---

## Fremtidige versjoner

### Planlagte funksjoner for v1.1.0
- [ ] Redigering av eksisterende møter
- [ ] Sletting av møter
- [ ] Bedre feilhåndtering og logging
- [ ] Input validering i modell-laget
- [ ] Async/await for database-operasjoner

### Planlagte funksjoner for v1.2.0
- [ ] Søkefunksjonalitet i møter
- [ ] Filtrering av møter etter dato
- [ ] Eksport til JSON og CSV
- [ ] Import fra eksterne filer

### Planlagte funksjoner for v2.0.0
- [ ] Web-basert brukergrensesnitt
- [ ] REST API
- [ ] Flerbruker støtte
- [ ] Påminnelser og varsler
- [ ] Kalenderintegrasjon
- [ ] Møterom-booking

---

## Notater

### Versjonsnummerering
- **MAJOR**: Inkompatible API-endringer
- **MINOR**: Ny funksjonalitet på en bakoverkompatibel måte  
- **PATCH**: Bakoverkompatible feilrettinger

### Kategorier
- **Lagt til**: For nye funksjoner
- **Endret**: For endringer i eksisterende funksjonalitet
- **Avskrevet**: For funksjonalitet som snart vil bli fjernet
- **Fjernet**: For fjernet funksjonalitet
- **Fikset**: For feilrettinger
- **Sikkerhet**: For sårbarhetsrettelser