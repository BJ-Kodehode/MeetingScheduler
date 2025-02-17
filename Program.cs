using System;
using System.Collections.Generic;
using MeetingScheduler.Controllers;
// using MeetingScheduler.Models;

namespace MeetingScheduler
{
    class Program
    {
        static void Main()
        {
            MeetingController controller = new MeetingController();
            while (true)
            {
                Console.WriteLine("\nVelg et alternativ:");
                Console.WriteLine("1 - Registrer nytt møte");
                Console.WriteLine("2 - Vis alle møter");
                Console.WriteLine("3 - Avslutt");

                string choice = Console.ReadLine() ?? "";
                if (choice == "1")
                {
                    Console.Write("Skriv inn navn på deltakerne (kommaseparert): ");
                    string participants = Console.ReadLine() ?? "";

                    Console.Write("Skriv inn møtetidspunkt (yyyy-MM-dd HH:mm): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime meetingTime))
                    {
                        Console.Write("Skriv inn møtets varighet i minutter: ");
                        if (int.TryParse(Console.ReadLine(), out int duration))
                        {
                            Meeting newMeeting = new Meeting
                            {
                                Participants = participants ?? "",
                                MeetingTime = meetingTime,
                                DurationInMinutes = duration
                            };

                            controller.AddMeeting(newMeeting);
                            Console.WriteLine("Møtet er registrert!");
                        }
                        else
                        {
                            Console.WriteLine("Ugyldig varighet, må være et heltall.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ugyldig datoformat.");
                    }
                }

                else if (choice == "2")
                {
                    List<Meeting> meetings = controller.GetMeetings();
                    Console.WriteLine("\nRegistrerte møter:");
                    foreach (var meeting in meetings)
                    {
                        Console.WriteLine($"ID: {meeting.Id}, Deltakere: {meeting.Participants}, Start: {meeting.MeetingTime}, Slutt: {meeting.EndTime}, Varighet: {meeting.DurationInMinutes} min");
                    }
                }



                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ugyldig valg, prøv igjen.");
                }
            }
        }
    }
}
