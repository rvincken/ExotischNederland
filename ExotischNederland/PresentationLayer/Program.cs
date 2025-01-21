/*
    Project name: Exotisch Nederland Casus
    Project group: Arch BTW
    Project members: Rick Vincken, Daan Ros, D'vaughn Dassen, Sylas Barendse | Klas B1D

    Created: 01/16/2025

    Purpose: Het programma in de vorm van een console applicatie dient als grof prototype
             voor een applicatie voor het documenteren van waarnemingen van dier- en
             plantsoorten in het wild zoals gevraagd door de opdrachtgever.
*/

using ExotischNederland.BusinessLayer;
using ExotischNederland.Model;

namespace ExotischNederland.PresentationLayer;

internal class Program
{
    private static readonly SoortService soortService = new SoortService();
    private static readonly FotoService fotoService = new FotoService();
    private static readonly LocatieService locatieService = new LocatieService();
    private static readonly GebruikerService gebruikerService = new GebruikerService();
    private static readonly WaarnemingService waarnemingService = new WaarnemingService();

    // Gebruik de functie NonNullInput in plaats van Console.ReadLine().
    // Dit is om null-waarschuwingen te voorkomen.

    static void Main()
    {
        Console.WriteLine("EXOTISCH NEDERLAND");
        Console.WriteLine("------------------");

        Gebruiker gebruiker = Login();

        bool blijfVragen = true;

        while (blijfVragen)
        {
            Console.WriteLine("""
                              Kies een actie:
                              (1) Waarneming toevoegen
                              (2) Waarnemingen bekijken
                              (3) Stoppen
                              """);
            string actie = NonNullInput();

            switch (actie)
            {
                case "1":
                    if (gebruiker.Rol == "vrijwilliger" || gebruiker.Rol == "medewerker")
                    {
                        WaarnemingToevoegen(gebruiker);
                    }
                    else
                    {
                        Console.WriteLine("U heeft geen rechten om deze actie uit te voeren.");
                    }
                    break;
                case "2":
                    if (gebruiker.Rol == "medewerker")
                    {
                        WaarnemingenBekijken();
                    }
                    else
                    {
                        Console.WriteLine("U heeft geen rechten om deze actie uit te voeren.");
                    }
                    break;
                case "3":
                    Console.WriteLine("Bedankt voor het gebruiken van het Exotisch Nederland programma.");
                    blijfVragen = false;
                    break;
                default:
                    Console.WriteLine("Onbekende actie, probeer alsjeblieft opnieuw.");
                    break;
            }
        }
    }

    static Gebruiker Login()
    {
        // Implementeer login-systeem.
        // Deze return is tijdelijk totdat login is afgemaakt.
        // Na implementatie van Login(), verwijder deze.
        // Verwijder ook Random rnd, tenzij je deze wilt gebruiken.
        Random rnd = new Random();
        return new Gebruiker
        (
            rnd.Next(1000000, 9999999),
            "medewerker",
            "sjaak sjaak",
            "nl",
            2000,
            "nederland",
            "email@email.com",
            1234567890,
            "sjaakdegoat",
            'm',
            "ik was geboren vanaf een heel jonge leeftijd"
        );
    }

    static void WaarnemingToevoegen(Gebruiker gebruiker)
    {
        // Implementeer waarneming toevoegen systeem
    }

    static void WaarnemingenBekijken()
    {
        bool blijfVragen = true;

        while (blijfVragen)
        {
            Console.WriteLine("""
                              Kies een actie:
                              (1) Alle waarnemingen bekijken
                              (2) Stoppen
                              """);
            string actie = NonNullInput();

            switch (actie)
            {
                // Implementeer waarneming bekijken acties
                // Schuif "(2) Stoppen" naar een verder getal indien meer acties zijn toegevoegd
                case "2":
                    blijfVragen = false;
                    break;
            }
        }
    }

    

    // Input-ophalende functie die geen waarschuwing over mogelijke null-waardes geeft
    static string NonNullInput()
    {
        while (true)
        {
            string? input = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input is leeg, probeer alsjeblieft opnieuw.");
            }
            else
            {
                return input;
            }
        }
    }
}