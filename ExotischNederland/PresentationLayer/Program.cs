﻿/*
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
        while (true)
        {
            Console.WriteLine("""
                Kies een actie:
                (1) Inloggen
                (2) Registreren
                """);

            String keuze = NonNullInput();

            switch (keuze)
            {
                case "1":
                    Console.WriteLine("\nLOGIN");
                    Console.WriteLine("-----");
                    try
                    {
                        var gebruikers = gebruikerService.KrijgAlleGebruikers();

                        Console.WriteLine("Email: ");
                        String email = NonNullInput();

                        var gebruiker = gebruikers.FirstOrDefault(g => g.Email == email);
                        if (gebruiker == null)
                        {
                            Console.WriteLine("Gebruiker niet gevonden.");
                            break;
                        }

                        Console.WriteLine($"Welkom terug, {gebruiker.Weergavenaam}!");
                        return gebruiker;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fout bij inloggen: {ex.Message}");
                    }
                    break;

                case "2":
                    try
                    {
                        RegistreerNieuweGebruiker();
                        Console.WriteLine("Registratie succesvol! Log nu in om verder te gaan.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Registratie mislukt: {ex.Message}");
                    }
                    break;
            }
        }
    }

    static void RegistreerNieuweGebruiker()
    {
        Console.WriteLine("\nREGISTRATIE");
        Console.WriteLine("-----------");

        while (true)
        {
            try
            {
                Console.WriteLine("Wat is je rol?: (vrijwilliger/medewerker)");
                String rol = NonNullInput();
                Console.WriteLine("Geef je volledige naam: (2 tot 40 tekens.)");
                String naam = NonNullInput();
                Console.WriteLine("Geef je email adres: ");
                String email = NonNullInput();
                Console.WriteLine("Geef je geslacht: (m/v)");
                char geslacht = NonNullInput()[0];
                Console.WriteLine("Geef je geboortejaar:");
                String geboortejaar = NonNullInput();
                Console.WriteLine("Geef je taal: (nl/en)");
                String taal = NonNullInput();
                Console.WriteLine("Geef je telefoonnummer (bijv. 0612345678):");
                string telefoonnummer = NonNullInput();
                Console.WriteLine("Geef je weergavenaam: (Maximaal 20 tekens)");
                String weergavenaam = NonNullInput();
                Console.WriteLine("Geef je land: ");
                String land = NonNullInput();
                Console.WriteLine("Schrijf een bio: (Maximaal 200 tekens)");
                String biografie = NonNullInput();

                var nieuweGebruiker = new Gebruiker
                    (
                        rol, naam, taal, int.Parse(geboortejaar), land, email,
                        telefoonnummer, weergavenaam, geslacht, biografie
                    );

                var bestaandeGebruikers = gebruikerService.KrijgAlleGebruikers();
                if (bestaandeGebruikers.Any(g => g.Email == email))
                {
                    throw new ArgumentException("Er bestaat al een gebruiker met dit email adres.");
                }

                gebruikerService.RegistreerGebruiker(rol, naam, taal, int.Parse(geboortejaar), land, email, telefoonnummer, weergavenaam, geslacht, biografie);

                Console.WriteLine("\nRegistratie succesvol!");
                return;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fout: {ex.Message}");
                Console.WriteLine("Probeer opnieuw.\n");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Fout: ongeldige invoer. Probeer opnieuw.");
            }
        }
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