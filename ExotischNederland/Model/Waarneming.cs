using System.Globalization;

namespace ExotischNederland.Model;

internal class Waarneming
{
    private int _id;
    private Gebruiker _waarnemer;
    private Foto _foto;
    private Locatie _locatie;
    private Soort _soort;
    private string _omschrijving;
    private string _datum;
    private string _tijd;

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
            string timeFormat = "HH:mm";
            if (IsValidDateOrTime(value, timeFormat))
            {
                _tijd = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Waarneming.Tijd moet een geldige tijd zijn (HH:mm), " +
                    $"maar een string van {value} is gegeven.");
            }
        }
    }
    public Gebruiker Waarnemer
    {
        get { return _waarnemer; }
        private set { _waarnemer = value; }
    }
    public Foto Foto
    {
        get { return _foto; }
        private set { _foto = value; }
    }
    public Locatie Locatie
    {
        get { return _locatie; }
        private set { _locatie = value; }
    }
    public Soort Soort
    {
        get { return _soort; }
        private set { _soort = value; }
    }

    public Waarneming
        (int id, Foto foto, Gebruiker waarnemer, Locatie locatie, Soort soort,
        string omschrijving, string datum, string tijd)
    {
        Id = id;
        Omschrijving = omschrijving;
        Datum = datum;
        Tijd = tijd;
        Waarnemer = waarnemer;
        Foto = foto;
        Locatie = locatie;
        Soort = soort;
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
        return $"""
                ID: {Id}
                Omschrijving: {Omschrijving}
                Datum: {Datum}
                Tijd: {Tijd}
                Waarnemer ID: {Waarnemer.Id}
                Foto ID: {Foto.Id}
                Locatie ID: {Locatie.Id}
                Soort ID: {Soort.Id}
                """;
    }
}
