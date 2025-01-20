/*
    Project name: Exotisch Nederland Casus
    Project group: Arch BTW
    Project members: Rick Vincken, Daan Ros, D'vaughn Dassen, Sylas Barendse | Klas B1D

    Created: 01/16/2025

    Purpose: Het programma in de vorm van een console applicatie dient als grof prototype
             voor een applicatie voor het documenteren van waarnemingen van dier- en
             plantsoorten in het wild zoals gevraagd door de opdrachtgever.
*/

using ExotischNederland.Model;

namespace ExotischNederland.PresentationLayer;

internal class Program
{
    static void Main()
    {
        Random rnd = new Random();
        var soortService = new BusinessLayer.SoortService();
        soortService.RegistreerSoort
        (
            rnd.Next(1000000, 9999999),
            "anus soortius",
            "anusdier",
            "plant",
            "boom",
            5,
            'e'
        );
        var soorten = soortService.KrijgAlleSoorten();
        foreach (var soort in soorten)
        {
            Console.WriteLine(soort.ToString());
        }

        var gebruikerService = new BusinessLayer.GebruikerService();
        gebruikerService.RegistreerGebruiker
        (
            rnd.Next(1000000, 9999999),
            "vrijwilliger",
            "sjaak sjaak",
            "en",
            1963,
            "listenbourg",
            "@",
            1234567890,
            "sjaakdegoat",
            'm',
            "ik was geboren vanaf een heel jonge leeftijd"
        );
        var gebruikers = gebruikerService.KrijgAlleGebruikers();
        foreach (var gebruiker in gebruikers)
        {
            Console.WriteLine(gebruiker.ToString());
        }

        var fotoService = new BusinessLayer.FotoService();
        fotoService.RegistreerFoto(rnd.Next(1000000, 9999999), "C:/Users/rickv/Downloads/muis.png");
        var fotos = fotoService.KrijgAlleFotos();
        foreach (var foto in fotos)
        {
            Console.WriteLine(foto.ToString());
        }

        var locatieService = new BusinessLayer.LocatieService();
        locatieService.RegistreerLocatie
        (
            rnd.Next(1000000, 9999999),
            "urk",
            "Gelderland",
            4.752952,
            5.864214
        );
        var locaties = locatieService.KrijgAlleLocaties();
        foreach (var locatie in locaties)
        {
            Console.WriteLine(locatie.ToString());
        }
        var waarnemingService = new BusinessLayer.WaarnemingService();
        waarnemingService.RegistreerWaarneming
        (
            rnd.Next(1000000, 9999999),
            rnd.Next(1000000, 9999999),
            rnd.Next(1000000, 9999999),
            rnd.Next(1000000, 9999999),
            rnd.Next(1000000, 9999999),
            "halelujah",
            "01/01/2012",
            "12:30"
        );
        var waarnemingen = waarnemingService.KrijgAlleWaarnemingen();
        foreach (var waarneming in waarnemingen)
        {
            Console.WriteLine(waarneming.ToString());
        }

    }
}