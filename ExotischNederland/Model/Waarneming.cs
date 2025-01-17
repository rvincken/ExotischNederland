namespace ExotischNederland.Model;

internal class Waarneming
{
    private int _id;
    private int _aantal;
    private string _omschrijving;
    private string _toelichting;
    private string _datum;
    private string _tijd;
    private char _geslacht;
    private char _manierDelen;
    private bool _zekerheid;
    private Gebruiker _waarnemer;
    private Foto _afbeelding;
    private Locatie _waarnemingLocatie;
    private Soort _waarnemingSoort;

    public int Id
    {
        get { return _id; }
        private set 
        {
            if (value.ToString().Length <= 7)
            {
                _id = value;
            }
            else
            {
                throw new ArgumentException(
                    $"{this}.Id kan niet langer zijn dan 7 getallen, " +
                    $"Maar een getal van {value} is gegeven.");
            }
        }
    }
    public int Aantal
    {
        get { return _aantal; }
        private set
        {
            if (value.ToString().Length <= 7)
            {
                _aantal = value;
            }
            else
            {
                throw new ArgumentException(
                    $"{this}.Aantal kan niet langer zijn dan 7 getallen, " +
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
                    $"{this}.Omschrijving kan niet langer zijn dan 200 karakters, " +
                    $"Maar een string van:\n{value}\n... is gegeven.");
            }
        }
    }
    public string Toelichting
    {
        get { return _toelichting; }
        private set
        {
            if (value.Length <= 200)
            {
                _toelichting = value;
            }
            else
            {
                throw new ArgumentException(
                    $"{this}.Toelichting kan niet langer zijn dan 200 karakters, " +
                    $"Maar een string van:\n{value}\n... is gegeven.");
            }
        }
    }
    public string Datum
    {
        get { return _datum; }
        private set { _datum = value; }
    }
    public string Tijd
    {
        get { return _tijd; }
        private set { _tijd = value; }
    }
    public char Geslacht
    {
        get { return _geslacht; }
        private set
        {
            value = Char.ToLower(value);
            if (value == 'm' || value == 'f')
            {
                _geslacht = value;
            }
            else
            {
                throw new ArgumentException(
                    $"{this}.Geslacht kan alleen 'm'/'f' zijn, " +
                    $"maar een waarde van {value} is gegeven.");
            }
        }
    }
    public char ManierDelen
    {
        get { return _manierDelen; }
        private set
        {
            value = Char.ToLower(value);
            if (value == 'n' || value == 'v' || value == 'o')
            {
                _geslacht = value;
            }
            else
            {
                throw new ArgumentException(
                    $"{this}.ManierDelen kan alleen 'n'/'v'/'o' zijn, " +
                    $"maar een waarde van {value} is gegeven.");
            }
        }
    }
    public bool Zekerheid
    {
        get { return _zekerheid; }
        private set { _zekerheid = value; }
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
        Aantal = aantal;
        Omschrijving = omschrijving;
        Toelichting = toelichting;
        Datum = datum;
        Tijd = tijd;
        Geslacht = geslacht;
        ManierDelen = manierDelen;
        Zekerheid = zekerheid;
        Waarnemer = waarnemer;
        Afbeelding = afbeelding;
        WaarnemingLocatie = waarnemingLocatie;
        WaarnemingSoort = waarnemingSoort;
    }

    public override string ToString()
    {
        // Verbeter later:
        // hoe waarnemer, afbeelding, waarnemingLocatie en waarnemingSoort getoond worden
        string manierVanDelen;
        switch (ManierDelen)
        {
            case 'n':
                manierVanDelen = "Niet delen";
                break;
            case 'v':
                manierVanDelen = "Vertrouwde personen";
                break;
            case 'o':
                manierVanDelen = "Open";
                break;
            default:
                throw new FormatException(
                    $"{this}.ToString() - Ongeldige karakter in _manierDelen: {_manierDelen}");
        }

        return $"""
                ID: {Id}
                Aantal: {Aantal}
                Omschrijving: {Omschrijving}
                Toelichting: {Toelichting}
                Datum: {Datum}
                Tijd: {Tijd}
                Manier van delen: {manierVanDelen}
                Geslacht: {(Geslacht == 'm' ? "Male" : "Female")}
                Zekerheid: {(Zekerheid ? "Zeker" : "Onzeker")}
                Afbeelding toegevoegd: {(Afbeelding != null ? "Ja" : "Nee")}
                """;
    }
}
