/*
    Project name: Exotisch Nederland Casus
    Project group: Arch BTW
    Project members: Rick Vincken, Daan Ros, D'vaughn Dassen, Silas Barendse | Klas B1D

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
        bool Inloggen = true;
        while (Inloggen)
        {
            Console.WriteLine("""
                Kies een actie:
                (1) Inloggen
                (2) Registreren
                (3) Afsluiten
                """);

            String keuze = NonNullInput();

            switch (keuze)
            {
                case "1":
                    Console.Clear();
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
                case "3":
                    try
                    {
                        Console.WriteLine("Bedankt voor het gebruiken van het Exotisch Nederland programma.");
                        Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fout bij afsluiten: {ex.Message}");
                    }
                    break;
            }
        }
        return null;
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
                        1, rol, naam, taal, int.Parse(geboortejaar), land, email,
                        telefoonnummer, weergavenaam, geslacht, biografie
                    );

                var bestaandeGebruikers = gebruikerService.KrijgAlleGebruikers();
                if (bestaandeGebruikers.Any(g => g.Email == email))
                {
                    throw new ArgumentException("Er bestaat al een gebruiker met dit email adres.");
                }

                gebruikerService.RegistreerGebruiker(
                    rol,
                    naam,
                    taal,
                    int.Parse(geboortejaar),
                    land,
                    email,
                    telefoonnummer,
                    weergavenaam,
                    geslacht,
                    biografie
                );

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

        Console.WriteLine(new Waarneming(1, foto, gebruiker, locatie, soort, omschrijving, datum, tijd).ToString());
        Console.WriteLine("Klopt de volgende informatie? (ja/nee) (Waarneming ID is tijdelijk)");

        if (KrijgKeuze(["ja", "nee"]) == "ja")
        {
            waarnemingService.RegistreerWaarneming(gebruiker, foto, locatie, soort, omschrijving, datum, tijd);

            Console.WriteLine("Waarneming is geregistreerd.");
        }
        else
        {
            Console.WriteLine("Waarneming is niet geregistreerd.");
        }
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
            Console.WriteLine("Soort is nog niet geregistreerd. Extra informatie wordt gevraagd.");

            Console.WriteLine("Wat is de naam van de waargenomen soort?");
            string soortNaam = NonNullInput();

            Console.WriteLine("Tot welke categorie hoort de waargenomen soort? (bv. boom, knaagachtige)");
            string categorie = NonNullInput();

            Console.WriteLine("Is de waargenomen soort een plant of dier?");
            string type = KrijgKeuze(["plant", "dier"]);

            Console.WriteLine("Is de waargenomen soort inheems (i) of exoot (e) in Nederland?");
            char oorsprong = char.Parse(KrijgKeuze(["e", "i"]));

            soortService.RegistreerSoort(wetenschapNaam, soortNaam, type, categorie, oorsprong);

            Console.WriteLine("Nieuwe soort is geregistreerd.");

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

            Console.WriteLine("Locatie is al geregistreerd en wordt gekoppeld aan waarneming.");

            return locatie;
        }
        catch(Exception)
        {
            Console.WriteLine("Locatie is nog niet geregistreerd. Extra informatie wordt gevraagd.");

            Console.WriteLine("In welke provincie is deze locatie?");
            string provincie = KrijgKeuze
            ([
                "drenthe", "flevoland", "friesland", "gelderland",
                "groningen", "limburg", "noord-brabant", "noord-holland",
                "overijssel", "utrecht", "zeeland", "zuid-holland"
            ]);

            locatieService.RegistreerLocatie(locatienaam, provincie);

            Console.WriteLine("Nieuwe locatie is geregistreerd.");

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

                Console.WriteLine("Afbeelding is al gebruikt. Geef alsjeblieft een originele afbeelding.");

                continue;
            }
            catch (Exception)
            {
                Console.WriteLine("Deze Foto is origineel.");

                fotoService.RegistreerFoto(afbeelding);

                Console.WriteLine("Foto is geregistreerd.");

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
                              (2) Filteren op waarnemer email
                              (3) Filteren op wetenschap. naam
                              (4) Filteren op type
                              (5) Filteren op oorsprong
                              (6) Filteren op provincie
                              (7) Filteren op plaatsnaam
                              (8) Waarneming met ID zoeken
                              (9) Stoppen
                              """);
            string actie = NonNullInput();

            switch (actie)
            {
                // Implementeer waarneming bekijken acties
                // Schuif "(2) Stoppen" naar een verder getal indien meer acties zijn toegevoegd
                case "1":
                    AlleWaarnemingenBekijken();
                    break;
                case "2":
                    WaarnemingenVanEmailBekijken();
                    break;
                case "3":
                    WaarnemingenVanWetenschapNaamBekijken();
                    break;
                case "4":
                    WaarnemingenVanTypeBekijken();
                    break;
                case "5":
                    WaarnemingenVanOorsprongBekijken();
                    break;
                case "6":
                    WaarnemingenVanProvincieBekijken();
                    break;
                case "7":
                    WaarnemingenVanPlaatsNaamBekijken();
                    break;
                case "8":
                    WaarnemingVanIdBekijken();
                    break;
                case "9":
                    blijfVragen = false;
                    break;
                default:
                    Console.WriteLine("Ongeldige keuze. Probeer alsjeblieft opnieuw.");
                    break;
            }
        }
    }

    static void AlleWaarnemingenBekijken()
    {
        List<Waarneming> waarnemingen = waarnemingService.KrijgAlleWaarnemingen();

        int e = 0;
        int i = 0;

        foreach (var waarneming in waarnemingen)
        {
            Console.WriteLine(waarneming.ToString());
            if (waarneming.Soort.Oorsprong == 'e')
            {
                e++;
            }
            else
            {
                i++;
            }
        }

        Console.WriteLine($"De gehalte van exoten en inheemsen is: {KrijgProcentueelGehalte(e, i)}");
    }

    static void WaarnemingenVanEmailBekijken()
    {
        Console.WriteLine("Geef een gebruikers Email adres om op te filteren.");
        string email = NonNullInput();

        List<Waarneming> waarnemingen = waarnemingService.KrijgAlleWaarnemingen();

        int e = 0;
        int i = 0;

        foreach (var waarneming in waarnemingen)
        {
            if (waarneming.Waarnemer.Email == email)
            {
                Console.WriteLine(waarneming.ToString());
                if (waarneming.Soort.Oorsprong == 'e')
                {
                    e++;
                }
                else
                {
                    i++;
                }
            }
        }

        Console.WriteLine($"De gehalte van exoten en inheemsen is: {KrijgProcentueelGehalte(e, i)}");
    }

    static void WaarnemingenVanWetenschapNaamBekijken()
    {
        Console.WriteLine("Geef een wetenschappelijke soortnaam om op te filteren.");
        string wetenschapNaam = NonNullInput();

        List<Waarneming> waarnemingen = waarnemingService.KrijgAlleWaarnemingen();

        int e = 0;
        int i = 0;

        foreach (var waarneming in waarnemingen)
        {
            if (waarneming.Soort.WetenschappelijkeNaam == wetenschapNaam)
            {
                Console.WriteLine(waarneming.ToString());
                if (waarneming.Soort.Oorsprong == 'e')
                {
                    e++;
                }
                else
                {
                    i++;
                }
            }
        }

        Console.WriteLine($"De gehalte van exoten en inheemsen is: {KrijgProcentueelGehalte(e, i)}");
    }

    static void WaarnemingenVanTypeBekijken()
    {
        Console.WriteLine("Geef een type (plant/dier) om op te filteren.");
        string type = KrijgKeuze(["plant", "dier"]);

        List<Waarneming> waarnemingen = waarnemingService.KrijgAlleWaarnemingen();

        int e = 0;
        int i = 0;

        foreach (var waarneming in waarnemingen)
        {
            if (waarneming.Soort.Type == type)
            {
                Console.WriteLine(waarneming.ToString());
                if (waarneming.Soort.Oorsprong == 'e')
                {
                    e++;
                }
                else
                {
                    i++;
                }
            }
        }

        Console.WriteLine($"De gehalte van exoten en inheemsen is: {KrijgProcentueelGehalte(e, i)}");
    }

    static void WaarnemingenVanPlaatsNaamBekijken()
    {
        Console.WriteLine("Geef een plaatsnaam om op te filteren.");
        string plaatsnaam = NonNullInput();

        List<Waarneming> waarnemingen = waarnemingService.KrijgAlleWaarnemingen();

        int e = 0;
        int i = 0;

        foreach (var waarneming in waarnemingen)
        {
            if (waarneming.Locatie.Locatienaam == plaatsnaam)
            {
                Console.WriteLine(waarneming.ToString());
                if (waarneming.Soort.Oorsprong == 'e')
                {
                    e++;
                }
                else
                {
                    i++;
                }
            }
        }

        Console.WriteLine($"De gehalte van exoten en inheemsen is: {KrijgProcentueelGehalte(e, i)}");
    }

    static void WaarnemingenVanProvincieBekijken()
    {
        Console.WriteLine("Geef een provincie om op te filteren.");
        string provincie = KrijgKeuze
       ([
                "drenthe", "flevoland", "friesland", "gelderland",
                "groningen", "limburg", "noord-brabant", "noord-holland",
                "overijssel", "utrecht", "zeeland", "zuid-holland"
       ]);

        List<Waarneming> waarnemingen = waarnemingService.KrijgAlleWaarnemingen();

        int e = 0;
        int i = 0;

        foreach (var waarneming in waarnemingen)
        {
            if (waarneming.Locatie.Provincie == provincie)
            {
                Console.WriteLine(waarneming.ToString());
                if (waarneming.Soort.Oorsprong == 'e')
                {
                    e++;
                }
                else
                {
                    i++;
                }
            }
        }

        Console.WriteLine($"De gehalte van exoten en inheemsen is: {KrijgProcentueelGehalte(e, i)}");
    }

    static void WaarnemingVanIdBekijken()
    {
        Console.WriteLine("Geef het waarneming ID om op te zoeken.");
        int id = GetValidInt();

        try
        {
            Console.WriteLine(waarnemingService.KrijgWaarnemingVanId(id).ToString());
        }
        catch (ArgumentException)
        {
            Console.WriteLine($"Geen waarneming gevonden voor ID: {id}");
        }
    }

    static void WaarnemingenVanOorsprongBekijken()
    {
        List<Waarneming> waarnemingen = waarnemingService.KrijgAlleWaarnemingen();

        Console.WriteLine("Wilt u filteren op exoot (e) of inheems (i)?");
        char oorsprong = char.Parse(KrijgKeuze(["e", "i"]));

        foreach (var waarneming in waarnemingen)
        {
            if (waarneming.Soort.Oorsprong == oorsprong)
            {
                Console.WriteLine(waarneming.ToString());
            }
        }
    }

    public static string KrijgProcentueelGehalte(int groepA, int groepB)
    {
        int totaal = groepA + groepB;
        if (totaal == 0)
        {
            return "0%, 0%"; // als beide groepen leeg zijn, is het 0%
        }

        double percentageA = (double)groepA / totaal * 100;
        double percentageB = (double)groepB / totaal * 100;

        return $"{Math.Round(percentageA)}%, {Math.Round(percentageB)}%";
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

    static int GetValidInt()
    {
        while (true)
        {
            try
            {
                return int.Parse(NonNullInput());
            }
            catch
            {
                Console.WriteLine("Ongeldig getal.");
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
