# MeetingScheduler

En enkel konsollapplikasjon for møteplanlegging skrevet i C# med .NET 9.0.

## 📋 Oversikt

MeetingScheduler er en konsollbasert applikasjon som lar brukere registrere og administrere møter. Applikasjonen bruker SQLite-database for lagring og tilbyr et enkelt tekstbasert brukergrensesnitt.

## ✨ Funksjoner

- **Registrer nye møter**: Legg til møter med deltakere, tidspunkt og varighet
- **Vis alle møter**: Se en oversikt over alle registrerte møter
- **Automatisk sluttid**: Beregner automatisk møtets sluttid basert på starttid og varighet
- **Persistent lagring**: Alle møter lagres i en SQLite-database
- **JSON-logging**: Møter kan også eksporteres til JSON-format

## 🛠️ Teknisk stack

- **Framework**: .NET 9.0
- **Språk**: C#
- **Database**: SQLite
- **Dependencies**:
  - System.Data.SQLite (1.0.119)
  - Newtonsoft.Json (13.0.3)

## 📁 Prosjektstruktur

```
MeetingScheduler/
├── Program.cs                  # Hovedapplikasjon og brukergrensesnitt
├── Models/
│   └── Meeting.cs             # Meeting datamodell
├── Controllers/
│   └── MeetingController.cs   # Forretningslogikk og database-operasjoner
├── MeetingScheduler.csproj    # Prosjektfil
├── MeetingScheduler.sln       # Solution-fil
├── meetings.db                # SQLite database (genereres automatisk)
├── meetings.json              # JSON backup (genereres automatisk)
└── bin/                       # Bygde filer
```

## 🚀 Installasjon og kjøring

### Forutsetninger

- .NET 9.0 SDK eller nyere
- Windows, macOS eller Linux

### Kjøring

1. **Klon eller last ned prosjektet**
   ```bash
   git clone <repository-url>
   cd MeetingScheduler
   ```

2. **Bygg prosjektet**
   ```bash
   dotnet build
   ```

3. **Kjør applikasjonen**
   ```bash
   dotnet run
   ```

## 💻 Bruk

Når applikasjonen starter, får du følgende meny:

```
Velg et alternativ:
1 - Registrer nytt møte
2 - Vis alle møter
3 - Avslutt
```

### Registrere et nytt møte

1. Velg alternativ `1`
2. Skriv inn deltakere (kommaseparert): `John Doe, Jane Smith`
3. Skriv inn møtetidspunkt: `2025-10-08 14:30`
4. Skriv inn varighet i minutter: `60`

### Vise alle møter

Velg alternativ `2` for å se en liste over alle registrerte møter med:
- Møte-ID
- Deltakere
- Starttid
- Sluttid
- Varighet

## 🗄️ Database

Applikasjonen bruker SQLite-database med følgende skjema:

```sql
CREATE TABLE Meetings (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Participants TEXT NOT NULL,
    MeetingTime TEXT NOT NULL,
    DurationInMinutes INTEGER NOT NULL,
    EndTime TEXT NOT NULL
);
```

Database-filen (`meetings.db`) opprettes automatisk første gang applikasjonen kjøres.

## 📄 Filformater

### Datoformat
- **Input**: `yyyy-MM-dd HH:mm` (f.eks. `2025-10-08 14:30`)
- **Lagring**: `yyyy-MM-dd HH:mm:ss`

### JSON-eksport
Møter kan også lagres i JSON-format i `meetings.json`:

```json
[
  {
    "Id": 1,
    "Participants": "John Doe, Jane Smith",
    "MeetingTime": "2025-10-08T14:30:00",
    "DurationInMinutes": 60
  }
]
```

## 🔧 Utvikling

### Arkitektur

Prosjektet følger en enkel MVC-lignende arkitektur:

- **Model** (`Meeting.cs`): Datamodell for møter
- **Controller** (`MeetingController.cs`): Forretningslogikk og database-operasjoner
- **View** (`Program.cs`): Konsoll-basert brukergrensesnitt

### Kodestil

- Norske kommentarer og meldinger
- Engelske variabel- og metodenavn
- Standard C# konvensjoner

## 🐛 Feilhåndtering

Applikasjonen håndterer følgende feilscenarier:

- Ugyldig datoformat
- Ugyldig varighet (ikke-numerisk input)
- Database-tilkoblingsfeil
- Ugyldig menyvalg

## 📝 Lisens

Dette prosjektet er utviklet for læring og demonstrasjon.

## 🤝 Bidrag

For å bidra til prosjektet:

1. Fork repositoryet
2. Opprett en feature branch
3. Commit endringene dine
4. Push til branchen
5. Opprett en Pull Request

## 📞 Support

For spørsmål eller problemer, opprett en issue i repositoryet.