/*
    Project name: Exotisch Nederland Casus
    Project group: Arch BTW
    Project members: Rick Vincken, Daan Ros, D'vaughn Dassen, Sylas Barendse | Klas B1D

    Created: 01/16/2025

    Purpose: Het programma in de vorm van een console applicatie dient als grof prototype
             voor een applicatie voor het documenteren van waarnemingen van dier- en
             plantsoorten in het wild zoals gevraagd door de opdrachtgever.
*/

using System.Globalization;

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
                        1000000, rol, naam, taal, int.Parse(geboortejaar), land, email,
                        telefoonnummer, weergavenaam, geslacht, biografie
                    );

                var bestaandeGebruikers = gebruikerService.KrijgAlleGebruikers();
                if (bestaandeGebruikers.Any(g => g.Email == email))
                {
                    throw new ArgumentException("Er bestaat al een gebruiker met dit email adres.");
                }

                gebruikerService.RegistreerGebruiker(1000000, rol, naam, taal, int.Parse(geboortejaar), land, email, telefoonnummer, weergavenaam, geslacht, biografie);

                Console.WriteLine("\nRegistratie succesvol!");
                return;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fout: {ex.Message}");
                Console.WriteLine("Probeer opnieuw.\n");
            }
            catch (FormatException)
            {
                Console.WriteLine("Fout: ongeldige invoer. Probeer opnieuw.");
            }
        }
    }

    static void WaarnemingToevoegen(Gebruiker gebruiker)
    {
        Soort soort = VraagSoort();
        Locatie locatie = VraagLocatie();
        Foto foto = VraagFoto();

        Console.WriteLine("Geef een omschrijving van de waarneming.");
        string omschrijving = NonNullInput();

        DateTime dt = DateTime.Now;

        string datum = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        string tijd = dt.ToString("HH:mm", CultureInfo.InvariantCulture);

        Console.WriteLine($"Datum: {datum} Tijd: {tijd} (Opgehaald uit systeemklok)");

        waarnemingService.RegistreerWaarneming(1000001, gebruiker, foto, locatie, soort, omschrijving, datum, tijd);

        Console.WriteLine("Waarmeming is geregistreerd.");
    }

    static Soort VraagSoort()
    {
        Console.WriteLine("Wat is de wetenschappelijke naam van de waargenomen soort?");
        string wetenschapNaam = NonNullInput();

        try
        {
            Soort soort = soortService.KrijgSoortVanWetenschapNaam(wetenschapNaam);

            Console.WriteLine("Wetenschappelijke naam staat al geregistreerd en wordt gekoppeld aan waarneming.");

            return soort;
        }
        catch(Exception)
        {
            Console.WriteLine("Soort bestaat nog niet in database. Extra informatie wordt gevraagd.");

            Console.WriteLine("Wat is de naam van de waargenomen soort?");
            string soortNaam = NonNullInput();

            Console.WriteLine("Tot welke categorie hoort de waargenomen soort? (bv. boom, knaagachtige)");
            string categorie = NonNullInput();

            Console.WriteLine("Is de waargenomen soort een plant of dier?");
            string type = KrijgKeuze(["plant", "dier"]);

            Console.WriteLine("Is de waargenomen soort inheems (i) of exoot (e) in Nederland?");
            char oorsprong = char.Parse(KrijgKeuze(["e", "i"]));

            soortService.RegistreerSoort(1000001, wetenschapNaam, soortNaam, type, categorie, oorsprong);

            Console.WriteLine("Nieuwe soort is toegevoegd aan database.");

            return soortService.KrijgSoortVanWetenschapNaam(wetenschapNaam);
        }
    }

    static Locatie VraagLocatie()
    {
        Console.WriteLine("Rond welke plaatsnaam heeft de waarneming plaatsgevonden? (Bv. Amsterdam, Maastricht)");
        string locatienaam = NonNullInput();

        try
        {
            Locatie locatie = locatieService.KrijgLocatieVanLocatieNaam(locatienaam);

            Console.WriteLine("Locatie is al geregistreerd in database en wordt gekoppeld aan waarneming.");

            return locatie;
        }
        catch(Exception)
        {
            Console.WriteLine("Locatie bestaat nog niet in database. Extra informatie wordt gevraagd.");

            Console.WriteLine("In welke provincie vond de waarneming plaats?");
            string provincie = KrijgKeuze
            ([
                "drenthe", "flevoland", "friesland", "gelderland",
                "groningen", "limburg", "noord-brabant", "noord-holland",
                "overijssel", "utrecht", "zeeland", "zuid-holland"
            ]);

            Console.WriteLine("Geef de breedtegraad van de waarneming afgekort op 6 decimalen. (coordinaat)");
            double breedtegraad = GetValidCoordinate();

            Console.WriteLine("Geef de lengtegraad van de waarneming afgekort op 6 decimalen. (coordinaat)");
            double lengtegraad = GetValidCoordinate();

            locatieService.RegistreerLocatie(1000001, locatienaam, provincie, breedtegraad, lengtegraad);

            Console.WriteLine("Nieuwe locatie is geregistreerd in database.");

            return locatieService.KrijgLocatieVanLocatieNaam(locatienaam);
        }
    }

    static Foto VraagFoto()
    {
        while (true)
        {
            Console.WriteLine("Geef het bestandspad naar een .png afbeeldingsbestand van de waarneming.");
            byte[] afbeelding = VraagLocalImage();

            try
            {
                var foto = fotoService.KrijgFotoVanByteArray(afbeelding);

                Console.WriteLine("Afbeelding bestaat al in database. Geef alsjeblieft een originele afbeelding.");

                continue;
            }
            catch (Exception)
            {
                Console.WriteLine("Deze Foto is origineel en bestaat nog niet in het database.");

                fotoService.RegistreerFoto(1000001, afbeelding);

                Console.WriteLine("Foto is toegevoegd aan database.");

                return fotoService.KrijgFotoVanByteArray(afbeelding);
            }
        }
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

    static string KrijgKeuze(string[] keuzes)
    {
        while (true)
        {
            string input = NonNullInput();

            if (keuzes.Contains(input))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Ongeldige keuze.");
            }
        }
    }

    static int GetValidInt()
    {
        while (true)
        {
            try
            {
                return int.Parse(NonNullInput());
            }
            catch (FormatException)
            {
                {
                    Console.WriteLine("Ongeldig getal gegeven.");
                }
            }
        }
    }

    static double GetValidCoordinate()
    {
        while (true)
        {
            try
            {
                double input = double.Parse(NonNullInput());

                if (IsValidCoordinate(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Ongeldige invoer.");
                }
            }
            catch
            {
                Console.WriteLine("Ongeldig getal");
            }
        }
    }

    static bool IsValidCoordinate(double coordinate)
    {
        double afgerond = Math.Round(coordinate, 6);

        return Math.Abs(coordinate - afgerond) < 1e-7;
    }

    static byte[] VraagLocalImage()
    {
        while (true)
        {
            try
            {
                string path = NonNullInput();
                return LaadAfbeeldingVanPad(path);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    static byte[] LaadAfbeeldingVanPad(string pad)
    {
        if (!File.Exists(pad))
        {
            throw new FileNotFoundException($"Het bestand '{pad}' bestaat niet.");
        }

        if (Path.GetExtension(pad).ToLower() != ".png")
        {
            throw new ArgumentException("Alleen PNG bestanden zijn toegestaan.");
        }

        return File.ReadAllBytes(pad);
    }

    // Input-ophalende functie die geen waarschuwing over mogelijke null-waardes geeft
    static string NonNullInput()
    {
        while (true)
        {
            string? input = Console.ReadLine().ToLower();

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