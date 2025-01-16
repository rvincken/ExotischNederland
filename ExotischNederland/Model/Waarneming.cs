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
    }
    public int Aantal
    {
        get { return _aantal; }
    }
    public string Omschrijving
    {
        get { return _omschrijving; }
    }
    public string Toelichting
    {
        get { return _toelichting; }
    }
    public string Datum
    {
        get { return _datum; }
    }
    public string Tijd
    {
        get { return _tijd; }
    }
    public char Geslacht
    {
        get { return _geslacht; }
    }
    public char ManierDelen
    {
        get { return _manierDelen; }
    }
    public bool Zekerheid
    {
        get { return _zekerheid; }
    }
    public Gebruiker Waarnemer
    {
        get { return _waarnemer; }
    }
    public Foto Afbeelding
    {
        get { return _afbeelding; }
    }
    public Locatie WaarnemingLocatie
    {
        get { return _waarnemingLocatie; }
    }
    public Soort WaarnemingSoort
    {
        get { return _waarnemingSoort; }
    }

    public Waarneming
        (int id, int aantal, string omschrijving, string toelichting,
        string datum, string tijd, char geslacht, char manierDelen,
        bool zekerheid, Gebruiker waarnemer, Foto afbeelding,
        Locatie waarnemingLocatie, Soort waarnemingSoort)
    {
        _id = id;
        _aantal = aantal;
        _omschrijving = omschrijving;
        _toelichting = toelichting;
        _datum = datum;
        _tijd = tijd;
        _geslacht = geslacht;
        _manierDelen = manierDelen;
        _zekerheid = zekerheid;
        _waarnemer = waarnemer;
        _afbeelding = afbeelding;
        _waarnemingLocatie = waarnemingLocatie;
        _waarnemingSoort = waarnemingSoort;
    }

    public override string ToString()
    {
        // Verbeter later: hoe manierDelen getoond wordt.
        // hoe waarnemer, afbeelding, waarnemingLocatie en
        // waarnemingSoort getoond worden
        return $"""
                ID: {_id}
                Aantal: {_aantal}
                Omschrijving: {_omschrijving}
                Toelichting: {_toelichting}
                Datum: {_datum}
                Tijd: {_tijd}
                Geslacht: {(_geslacht == 'M' ? "Male" : "Female")}
                Manier van delen: {_manierDelen}
                Zekerheid: {(_zekerheid ? "Zeker" : "Onzeker")}
                Afbeelding toegevoegd: {(_afbeelding != null ? "Ja" : "Nee")}
                """;
    }
}
