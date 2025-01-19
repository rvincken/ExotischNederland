using System.Globalization;

namespace ExotischNederland.Model;

internal class Waarneming
{
    private int _id;
    private string _omschrijving;
    private string _datum;
    private string _tijd;
    private Gebruiker _waarnemer;
    private Foto _afbeelding;
    private Locatie _waarnemingLocatie;
    private Soort _waarnemingSoort;

    public int Id
    {
        get { return _id; }
        private set 
        {
            if (value.ToString().Length == 7)
            {
                _id = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Waarneming.Id moet 7 getallen lang zijn, " +
                    $"Maar een getal van {value} is gegeven.");
            }
        }
    }

    public string Omschrijving
    {
        get { return _omschrijving; }
        private set
        {
            if (value.Length <= 200)
            {
                _omschrijving = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Waarneming.Omschrijving kan niet langer zijn dan 200 karakters, " +
                    $"Maar een string van:\n{value}\n... is gegeven.");
            }
        }
    }
    public string Datum
    {
        get { return _datum; }
        private set
        {
            string dateFormat = "dd/MM/yyyy";
            if (IsValidDateOrTime(value, dateFormat))
            {
                _datum = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Waarneming.Datum moet een geldige datum zijn (dd/MM/yyyy), " +
                    $"maar een string van {value} is gegeven.");
            }
        }
    }
    public string Tijd
    {
        get { return _tijd; }
        private set
        {
            string timeFormat = "HH/mm";
            if ( IsValidDateOrTime(value, timeFormat))
            {
                _tijd = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Waarneming.Tijd moet een geldige tijd zijn (HH/mm), " +
                    $"maar een string van {value} is gegeven.");
            }
        }
    }
    public Gebruiker Waarnemer
    {
        get { return _waarnemer; }
        private set { _waarnemer = value; }
    }
    public Foto Afbeelding
    {
        get { return _afbeelding; }
        private set { _afbeelding = value; }
    }
    public Locatie WaarnemingLocatie
    {
        get { return _waarnemingLocatie; }
        private set { _waarnemingLocatie = value; }
    }
    public Soort WaarnemingSoort
    {
        get { return _waarnemingSoort; }
        private set { _waarnemingSoort = value; }
    }

    public Waarneming
        (int id, int aantal, string omschrijving, string toelichting,
        string datum, string tijd, char geslacht, char manierDelen,
        bool zekerheid, Gebruiker waarnemer, Foto afbeelding,
        Locatie waarnemingLocatie, Soort waarnemingSoort)
    {
        Id = id;
        Omschrijving = omschrijving;
        Datum = datum;
        Tijd = tijd;
        Waarnemer = waarnemer;
        Afbeelding = afbeelding;
        WaarnemingLocatie = waarnemingLocatie;
        WaarnemingSoort = waarnemingSoort;
    }

    private bool IsValidDateOrTime(string value, string format)
    {
        return DateTime.TryParseExact(
            value,
            format,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out _);
    }

    public override string ToString()
    {
        // Verbeter later:
        // hoe waarnemer, afbeelding, waarnemingLocatie en waarnemingSoort getoond worden
        return $"""
                ID: {Id}
                Omschrijving: {Omschrijving}
                Datum: {Datum}
                Tijd: {Tijd}
                Afbeelding toegevoegd: {(Afbeelding != null ? "Ja" : "Nee")}
                Waarnemer toegevoegd: {(Waarnemer != null ? "Ja" : "Nee")}
                Locatie toegevoegd: {(WaarnemingLocatie != null ? "Ja" : "Nee")}
                Soort toegevoegd: {(WaarnemingSoort != null ? "Ja" : "Nee")}
                """;
    }
}
