# Installasjonsveiledning - MeetingScheduler

## Systemkrav

### Operativsystem
- Windows 10/11
- macOS 10.15 (Catalina) eller nyere
- Linux (Ubuntu 20.04+, RHEL 8+, eller tilsvarende)

### Software-krav
- .NET 9.0 SDK eller nyere
- Git (for kloning av repository)
- Valgfri: Visual Studio Code eller Visual Studio

## Installasjon

### Steg 1: Installer .NET 9.0 SDK

#### Windows
1. Gå til [Microsoft .NET Download](https://dotnet.microsoft.com/download/dotnet/9.0)
2. Last ned ".NET 9.0 SDK" for Windows
3. Kjør installasjonsfilen og følg instruksjonene

#### macOS
```bash
# Bruke Homebrew
brew install --cask dotnet-sdk

# Eller last ned fra Microsoft
# Gå til https://dotnet.microsoft.com/download/dotnet/9.0
```

#### Linux (Ubuntu/Debian)
```bash
# Legg til Microsoft package repository
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb

# Installer .NET SDK
sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-9.0
```

### Steg 2: Verifiser installasjon

Åpne terminal/kommandoprompt og kjør:

```bash
dotnet --version
```

Du skal se versjonsnummer 9.0.x eller høyere.

### Steg 3: Last ned prosjektet

#### Fra Git (anbefalt)
```bash
git clone https://github.com/BJ-Kodehode/MeetingScheduler.git
cd MeetingScheduler
```

#### Fra ZIP-fil
1. Last ned ZIP-filen fra GitHub
2. Pakk ut til ønsket mappe
3. Åpne terminal i prosjektmappen

### Steg 4: Bygg prosjektet

```bash
# Gjenopprett NuGet-pakker
dotnet restore

# Bygg prosjektet
dotnet build
```

Hvis bygget er vellykket, vil du se:
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

### Steg 5: Kjør applikasjonen

```bash
dotnet run
```

## Feilsøking

### Vanlige problemer

#### "dotnet command not found"
**Problem**: .NET SDK er ikke installert eller ikke i PATH.

**Løsning**:
1. Verifiser at .NET SDK er installert
2. Start terminal på nytt
3. På Windows: Sjekk at PATH inneholder .NET-installasjonsmappen

#### "Project file not found"
**Problem**: Du er ikke i riktig mappe.

**Løsning**:
```bash
# Naviger til prosjektmappen
cd path/to/MeetingScheduler
ls -la  # (Linux/macOS) eller dir (Windows) - skal vise .csproj-fil
```

#### "Package restore failed"
**Problem**: Nettverksproblemer eller proxy-innstillinger.

**Løsning**:
```bash
# Fjern NuGet cache
dotnet nuget locals all --clear

# Prøv restore igjen
dotnet restore --verbose
```

#### "SQLite.Interop.dll not found"
**Problem**: SQLite native libraries mangler.

**Løsning**:
```bash
# Rebuild med full restore
dotnet clean
dotnet restore
dotnet build
```

### Database-tillatelser

På Linux/macOS, sørg for at applikasjonen har skrivetilgang til mappen:

```bash
chmod 755 .
```

## Utvikling

### Anbefalt verktøy

#### Visual Studio Code
1. Installer [Visual Studio Code](https://code.visualstudio.com/)
2. Installer C# extension:
   - Åpne VS Code
   - Gå til Extensions (Ctrl+Shift+X)
   - Søk etter "C# Dev Kit" og installer

#### Visual Studio (Windows)
1. Last ned [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/)
2. Velg ".NET desktop development" workload under installasjon

### Debugging

#### Visual Studio Code
1. Åpne prosjektmappen i VS Code
2. Trykk F5 eller gå til Run > Start Debugging
3. Velg "C#" når prompted

#### Kommandolinje
```bash
# Kjør i debug-modus
dotnet run --configuration Debug

# Bygge debug-versjon
dotnet build --configuration Debug
```

### Testing

```bash
# Kjør alle tester (hvis testp prosjekt finnes)
dotnet test

# Kjør med verbose output
dotnet test --verbosity normal
```

## Produksjon

### Bygge for distribusjon

```bash
# Bygge release-versjon
dotnet build --configuration Release

# Publisere for spesifikk plattform
dotnet publish -c Release -r win-x64 --self-contained
dotnet publish -c Release -r linux-x64 --self-contained
dotnet publish -c Release -r osx-x64 --self-contained
```

### Deployment

Den publiserte applikasjonen finnes i:
```
bin/Release/net9.0/publish/
```

Kopier denne mappen til målsystemet og kjør executable-filen.

## Support

### Loggfiler

Applikasjonen oppretter følgende filer:
- `meetings.db` - SQLite database
- `meetings.json` - JSON backup (hvis generert)

### Feilrapportering

Når du rapporterer feil, inkluder:
1. Operativsystem og versjon
2. .NET versjon (`dotnet --version`)
3. Feilmelding eller stack trace
4. Steg for å reprodusere feilen

### Ytterligere hjelp

- [.NET dokumentasjon](https://docs.microsoft.com/en-us/dotnet/)
- [SQLite dokumentasjon](https://www.sqlite.org/docs.html)
- [Newtonsoft.Json dokumentasjon](https://www.newtonsoft.com/json/help/html/Introduction.htm)