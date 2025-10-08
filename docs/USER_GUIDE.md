# Brukerveiledning - MeetingScheduler

## Oversikt

MeetingScheduler er en enkel konsollapplikasjon for å registrere og administrere møter. Denne veiledningen vil hjelpe deg med å komme i gang.

## Oppstart

1. Åpne terminal eller kommandoprompt
2. Naviger til MeetingScheduler-mappen
3. Kjør kommandoen: `dotnet run`

Applikasjonen starter og viser hovedmenyen.

## Hovedmeny

```
Velg et alternativ:
1 - Registrer nytt møte
2 - Vis alle møter
3 - Avslutt
```

Skriv inn tallet som tilsvarer ønsket handling og trykk Enter.

## Funksjoner

### 1. Registrer nytt møte

Velg alternativ `1` for å legge til et nytt møte.

#### Steg-for-steg:

1. **Deltakere**: 
   ```
   Skriv inn navn på deltakerne (kommaseparert): 
   ```
   Eksempel: `John Doe, Jane Smith, Bob Johnson`

2. **Møtetidspunkt**:
   ```
   Skriv inn møtetidspunkt (yyyy-MM-dd HH:mm):
   ```
   Eksempel: `2025-10-08 14:30`
   
   **Format forklaring:**
   - `yyyy` = År (4 siffer)
   - `MM` = Måned (2 siffer)
   - `dd` = Dag (2 siffer)  
   - `HH` = Time (24-timers format, 2 siffer)
   - `mm` = Minutt (2 siffer)

3. **Varighet**:
   ```
   Skriv inn møtets varighet i minutter:
   ```
   Eksempel: `60` (for 1 time)

#### Eksempel på komplett registrering:

```
Velg et alternativ: 1
Skriv inn navn på deltakerne (kommaseparert): Alice Hansen, Bob Nilsen
Skriv inn møtetidspunkt (yyyy-MM-dd HH:mm): 2025-10-10 09:00
Skriv inn møtets varighet i minutter: 90
Møtet er registrert!
```

### 2. Vis alle møter

Velg alternativ `2` for å se en oversikt over alle registrerte møter.

#### Visning:

```
Registrerte møter:
ID: 1, Deltakere: Alice Hansen, Bob Nilsen, Start: 10.10.2025 09:00:00, Slutt: 10.10.2025 10:30:00, Varighet: 90 min
ID: 2, Deltakere: John Doe, Jane Smith, Start: 08.10.2025 14:30:00, Slutt: 08.10.2025 15:30:00, Varighet: 60 min
```

#### Informasjon som vises:
- **ID**: Unik identifikator for møtet
- **Deltakere**: Liste over alle deltakere
- **Start**: Når møtet starter
- **Slutt**: Når møtet slutter (beregnes automatisk)
- **Varighet**: Møtets lengde i minutter

### 3. Avslutt

Velg alternativ `3` for å lukke applikasjonen.

## Tips og triks

### Datoformat

**Riktige formater:**
- `2025-10-08 14:30` ✅
- `2025-12-25 08:00` ✅
- `2025-01-01 23:59` ✅

**Feil formater:**
- `08.10.2025 14:30` ❌
- `10/08/2025 2:30 PM` ❌
- `2025-10-8 14:30` ❌ (mangler 0 foran dag)

### Deltakere

- Skill deltakere med komma: `Alice, Bob, Charlie`
- Bruk fulle navn for klarhet: `Alice Hansen, Bob Nilsen`
- Unngå ekstra mellomrom: `Alice,Bob,Charlie` eller `Alice, Bob, Charlie`

### Varighet

- Oppgi varighet i minutter
- Vanlige varigheter:
  - 15 minutter (kort møte)
  - 30 minutter (standard møte)
  - 60 minutter (1 time)
  - 90 minutter (1.5 timer)
  - 120 minutter (2 timer)

## Feilmeldinger og løsninger

### "Ugyldig datoformat"

**Problem**: Datoen ble ikke skrevet i riktig format.

**Løsning**: Bruk formatet `yyyy-MM-dd HH:mm`
- Eksempel: `2025-10-08 14:30`

### "Ugyldig varighet, må være et heltall"

**Problem**: Du skrev inn noe som ikke er et tall for varighet.

**Løsning**: Skriv inn bare tall
- Riktig: `60`
- Feil: `60 minutter`, `en time`, `1.5`

### "Ugyldig valg, prøv igjen"

**Problem**: Du valgte et alternativ som ikke finnes i menyen.

**Løsning**: Velg kun `1`, `2`, eller `3`

## Lagring

### Database

Alle møter lagres automatisk i filen `meetings.db` i samme mappe som applikasjonen. Denne filen opprettes automatisk første gang du registrerer et møte.

### Backup

Du kan trygt kopiere `meetings.db`-filen for å ta backup av alle møtene dine.

## Vanlige spørsmål

### Kan jeg redigere et møte?

Nei, i nåværende versjon kan du kun legge til og vise møter. For å "endre" et møte må du registrere et nytt.

### Kan jeg slette møter?

Nei, sletting av møter er ikke implementert i denne versjonen.

### Hvor mange møter kan jeg registrere?

Det er ingen praktisk begrensning på antall møter du kan registrere.

### Kan jeg søke etter spesifikke møter?

Nei, du kan kun vise alle møter samtidig.

### Kan jeg eksportere møtene?

Møtene lagres i en SQLite-database. Du kan bruke SQLite-verktøy for å eksportere dataene hvis nødvendig.

## Fremtidige versjoner

Planlagte forbedringer inkluderer:
- Redigering av eksisterende møter
- Sletting av møter
- Søkefunksjonalitet
- Eksport til forskjellige formater
- Påminnelser og varsler
- Kalendervisning

## Support

Hvis du opplever problemer eller har spørsmål:
1. Sjekk denne brukerveiledningen først
2. Se installasjonsveiledningen for tekniske problemer
3. Opprett en issue i GitHub-repositoriet