using Microsoft.VisualBasic;

namespace ExotischNederland.Model;

internal class Locatie
{
    private int _id;
    private string _locatienaam;
    private string _provincie;

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

    public Locatie(int id, string locatienaam, string provincie)
    {
        if (IsValidName(locatienaam))
        {
            _locatienaam = locatienaam;
        }
        else
        {
            throw new ArgumentException("Locatienaam bevat getallen of is langer dan 50 karakters.");
        }

        _id = id;

        if (IsValidProvincie(provincie))
        {
            _provincie = provincie;
        }
        else
        {
            throw new ArgumentException("Provincie is niet geldig.");
        }
    }

    private void WijzigNaam(string locatienaam)
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

    private void WijzigProvincie(string provincie)
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
            "drenthe",
            "flevoland",
            "friesland",
            "gelderland",
            "groningen",
            "limburg",
            "noord-brabant",
            "noord-holland",
            "overijssel",
            "utrecht",
            "zeeland",
            "zuid-holland"
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

        if (locatienaam.Length > 50)
        {
            return false;
        }

        return true;
    }

    public override string ToString()
    {
        return $""""
                Id: {Id}
                Locatienaam: {Locatienaam}
                Provincie: {Provincie}
                """";
    }
}