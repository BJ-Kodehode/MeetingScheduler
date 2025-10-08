// Importerer nødvendige namespaces
// using System; - Ikke nødvendig med implicit usings i .NET 9
// using System.Collections.Generic; - Ikke nødvendig med implicit usings
using MeetingScheduler.Controllers;
// using MeetingScheduler.Models; - Ikke nødvendig da Meeting er globally available

namespace MeetingScheduler
{
    /// <summary>
    /// Hovedklasse for MeetingScheduler-applikasjonen
    /// Inneholder brukergrensesnitt og programflyt
    /// </summary>
    class Program
    {
        /// <summary>
        /// Hovedmetode som starter applikasjonen
        /// Kjører en uendelig løkke med menyvalg til brukeren velger å avslutte
        /// </summary>
        static void Main()
        {
            // Oppretter en kontroller for å håndtere møte-operasjoner
            MeetingController controller = new MeetingController();
            
            // Hovedløkke som kjører til brukeren velger å avslutte
            while (true)
            {
                // Viser hovedmeny til brukeren
                Console.WriteLine("\nVelg et alternativ:");
                Console.WriteLine("1 - Registrer nytt møte");
                Console.WriteLine("2 - Vis alle møter");
                Console.WriteLine("3 - Avslutt");

                // Leser brukerens valg fra konsollen
                string choice = Console.ReadLine() ?? "";
                
                // Håndterer registrering av nytt møte
                if (choice == "1")
                {
                    // Samler inn informasjon om deltakere
                    Console.Write("Skriv inn navn på deltakerne (kommaseparert): ");
                    string participants = Console.ReadLine() ?? "";

                    // Samler inn møtetidspunkt med validering
                    Console.Write("Skriv inn møtetidspunkt (yyyy-MM-dd HH:mm): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime meetingTime))
                    {
                        // Samler inn varighet med validering
                        Console.Write("Skriv inn møtets varighet i minutter: ");
                        if (int.TryParse(Console.ReadLine(), out int duration))
                        {
                            // Oppretter nytt Meeting-objekt med innsamlet data
                            Meeting newMeeting = new Meeting
                            {
                                Participants = participants ?? "",
                                MeetingTime = meetingTime,
                                DurationInMinutes = duration
                            };

                            // Lagrer møtet gjennom kontrolleren
                            controller.AddMeeting(newMeeting);
                            Console.WriteLine("Møtet er registrert!");
                        }
                        else
                        {
                            // Feilmelding hvis varighet ikke er et gyldig heltall
                            Console.WriteLine("Ugyldig varighet, må være et heltall.");
                        }
                    }
                    else
                    {
                        // Feilmelding hvis datoformat er ugyldig
                        Console.WriteLine("Ugyldig datoformat.");
                    }
                }

                // Håndterer visning av alle møter
                else if (choice == "2")
                {
                    // Henter alle møter fra databasen via kontrolleren
                    List<Meeting> meetings = controller.GetMeetings();
                    Console.WriteLine("\nRegistrerte møter:");
                    
                    // Itererer gjennom alle møter og viser informasjon
                    foreach (var meeting in meetings)
                    {
                        Console.WriteLine($"ID: {meeting.Id}, Deltakere: {meeting.Participants}, Start: {meeting.MeetingTime}, Slutt: {meeting.EndTime}, Varighet: {meeting.DurationInMinutes} min");
                    }
                }

                // Håndterer avslutning av programmet
                else if (choice == "3")
                {
                    // Bryter ut av hovedløkken for å avslutte programmet
                    break;
                }
                else
                {
                    // Feilmelding for ugyldig menyvalg
                    Console.WriteLine("Ugyldig valg, prøv igjen.");
                }
            }
        }
    }
}
