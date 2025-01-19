using System.Globalization;

namespace ExotischNederland.Model;

internal class Waarneming
{
    private int _id;
    private int _waarnemerId;
    private int _fotoId;
    private int _locatieId;
    private int _soortId;
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
            if ( IsValidDateOrTime(value, timeFormat))
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
    public int WaarnemerId
    {
        get { return _waarnemerId; }
        private set
        {
            if (value.ToString().Length == 7)
            {
                _waarnemerId = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Waarneming.WaarnemerId moet 7 getallen lang zijn, " +
                    $"Maar een getal van {value} is gegeven.");
            }
        }
    }
    public int FotoId
    {
        get { return _fotoId; }
        private set
        {
            if (value.ToString().Length == 7)
            {
                _fotoId = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Waarneming.FotoId moet 7 getallen lang zijn, " +
                    $"Maar een getal van {value} is gegeven.");
            }
        }
    }
    public int LocatieId
    {
        get { return _locatieId; }
        private set
        {
            if (value.ToString().Length == 7)
            {
                _locatieId = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Waarneming.LocatieId moet 7 getallen lang zijn, " +
                    $"Maar een getal van {value} is gegeven.");
            }
        }
    }
    public int SoortId
    {
        get { return _soortId; }
        private set
        {
            if (value.ToString().Length == 7)
            {
                _soortId = value;
            }
            else
            {
                throw new ArgumentException(
                    $"Waarneming.SoortId moet 7 getallen lang zijn, " +
                    $"Maar een getal van {value} is gegeven.");
            }
        }
    }

    public Waarneming
        (int id, int fotoId, int waarnemerId, int locatieId, int soortId,
        string omschrijving, string datum, string tijd)
    {
        Id = id;
        Omschrijving = omschrijving;
        Datum = datum;
        Tijd = tijd;
        WaarnemerId = waarnemerId;
        FotoId = fotoId;
        LocatieId = locatieId;
        SoortId = soortId;
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
                Waarnemer ID: {WaarnemerId}
                Foto ID: {FotoId}
                Locatie ID: {LocatieId}
                Soort ID: {SoortId}
                """;
    }
}
