using Microsoft.VisualBasic;

namespace ExotischNederland.Model;

// implementeer:
// id, locatienaam, provincie, breedtegraad, lengtegraad

internal class Locatie
{
    private int _id;
    private string _locatienaam;
    private string _provincie;
    private double _breedtegraad;
    private double _lengtegraad;

    public int Id
    {
        get { return _id; }
    }

    public string Locatienaam
    {
        get { return _locatienaam; }
    }

    public string Provincie
    {
        get { return _provincie; }
    }

    public double Breedtegraad
    {
        get { return _breedtegraad; }
    }

    public double Lengtegraad
    {
        get { return _lengtegraad; }
    }

    public Locatie(int id, string locatienaam, string provincie, double breedtegraad, double lengtegraad)
    {
        _id = id;
        _locatienaam = locatienaam;

        if (IsValidProvincie(provincie))
        {
            _provincie = provincie;
        }
        else
        {
            throw new ArgumentException("Provincie is niet geldig.");
        }
        _breedtegraad = breedtegraad;
        _lengtegraad = lengtegraad;
    }

    public void WijzigNaam(string locatienaam)
    {
        if (IsValidName(locatienaam))
        {
            _locatienaam = locatienaam;
        }
        else
        {
            throw new ArgumentException("Locatienaam mag geen getallen bevatten.");
        }
    }

    public void WijzigProvincie(string provincie)
    {
        if (IsValidProvincie(provincie))
        {
            _provincie = provincie;
        }
        else
        {
            throw new ArgumentException("Provincie is niet geldig.");
        }
    }

    private bool IsValidProvincie(string provincie)
    {
        List<String> provincieNamen = new List<String>
        {
            "Drenthe",
            "Flevoland",
            "Friesland",
            "Gelderland",
            "Groningen",
            "Limburg",
            "Noord-Brabant",
            "Noord-Holland",
            "Overijssel",
            "Utrecht",
            "Zeeland",
            "Zuid-Holland"
        };

        return provincieNamen.Contains(provincie);
    }

    private bool IsValidName(string locatienaam)
    {
        List<char> getallenKarakakters = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        foreach (char c in locatienaam)
        {
            if (getallenKarakakters.Contains(c))
            {
                return false;
            }
        }
        return true;
    }

    public override string ToString()
    {
        return $""""
                Id: {Id}
                Locatienaam: {Locatienaam}
                Provincie: {Provincie}
                Breedtegraad: {Breedtegraad}
                Lengtegraad: {Lengtegraad}
                """";
    }
}